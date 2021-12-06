package com.example.databasefiller.service.generators

import com.example.databasefiller.models.*
import com.example.databasefiller.models.country.*
import com.example.databasefiller.models.event.Event
import com.example.databasefiller.models.event.MAX_PLAYERS_IN_TEAM
import com.example.databasefiller.models.event.MIN_PLAYERS_IN_TEAM
import com.github.javafaker.Faker
import org.springframework.stereotype.Service
import java.sql.Date
import kotlin.random.Random

private fun Boolean.toInt() = if (this) 1 else 0

@Service
class DataGeneratorService(
    private val eventNameGeneratorService: EventNameGeneratorService
) {

    private val faker = Faker()

    fun nextCountry(): Country {
        val name = faker.country().name().take(40)
        val countryId = nextCountryId()
        val area = (MIN_AREA..MAX_AREA).random()
        val population = (MIN_POPULATION..MAX_POPULATION).random()

        return Country(name, countryId, area, population)
    }

    fun nextPlayer(): Player {
        val firstName = faker.name().firstName()
        val lastName = faker.name().lastName()
        val name = "$firstName $lastName"
        val playerId = "${lastName.take(5)}${firstName.take(3)}01".uppercase()
        val birthday = nextBirthday()!!
        val country = nextCountry()

        return Player(name, playerId, country, birthday)
    }

    fun nextEvent(): Event {
        val eventId = "E-1"
        val eventNameWithType = eventNameGeneratorService.nextEventNameWithType()
        val name = eventNameWithType.name
        val eventType = eventNameWithType.type
        val olympic = nextOlympic()
        val isTeamEvent = Random.nextBoolean()
        val playersNumberInTeam = if (!isTeamEvent) {
            -1
        } else {
            (MIN_PLAYERS_IN_TEAM..MAX_PLAYERS_IN_TEAM).random()
        }
        val resultNotedIn = listOf("seconds, meters").random()

        return Event(eventId, name, eventType, olympic, isTeamEvent.toInt(), playersNumberInTeam, resultNotedIn)
    }

    fun nextOlympic(): Olympic {
        val countryWithCity = nextCountryWithCity()
        val country = countryWithCity.country
        val city = countryWithCity.city
        val year = (MIN_YEAR..MAX_YEAR).random()
        val olympicId = "${city.take(3).uppercase()}$year"
        val startDate = nextDateWithYear(year)!!
        if (startDate.date >= 17) {
            startDate.date -= 3
        }

        val endMonth = startDate.month + 2
        val endDay = startDate.day + (4..10).random()
        val endDate = Date.valueOf("$year-$endMonth-$endDay")

        return Olympic(olympicId, country, city, year, startDate, endDate)
    }

    fun nextResult(): Result {
        val event = nextEvent()
        val player = nextPlayer()
        val medal = listOf("GOLD", "SILVER", "BRONZE").random()
        val result = ((MIN_RESULT * 100)..(MAX_RESULT * 100)).random() / 100.0

        return Result(event, player, medal, result.toFloat())
    }

    private fun nextDateWithYear(year: Int): Date? {
        val month = (1..10).random()
        val day = (1..15).random()

        return Date.valueOf("$year-$month-$day")
    }

    private fun nextBirthday(): Date? {
        val year = (MIN_BIRTHDAY_YEAR..MAX_BIRTHDAY_YEAR).random()
        val month = (1..11).random()
        val day = (1..15).random()

        return Date.valueOf("$year-$month-$day")
    }

    private fun nextCountryWithCity(): CountryWithCity {
        val country = faker.country()
        val name = country.name().take(40)
        val city = country.capital().take(50)
        val countryId = nextCountryId()
        val area = (MIN_AREA..MAX_AREA).random()
        val population = (MIN_POPULATION..MAX_POPULATION).random()

        return CountryWithCity(Country(name, countryId, area, population), city)
    }

    private fun nextCountryId() = faker.country().countryCode3().uppercase()
}