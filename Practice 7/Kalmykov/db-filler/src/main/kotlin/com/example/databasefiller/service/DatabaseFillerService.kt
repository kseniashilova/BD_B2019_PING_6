package com.example.databasefiller.service

import com.example.databasefiller.models.Olympic
import com.example.databasefiller.models.Player
import com.example.databasefiller.models.Response
import com.example.databasefiller.models.Result
import com.example.databasefiller.models.event.Event
import com.example.databasefiller.repository.*
import com.example.databasefiller.service.generators.DataGeneratorService
import org.springframework.stereotype.Service
import javax.transaction.Transactional

@Service
class DatabaseFillerService(
    private val dataGeneratorService: DataGeneratorService,
    private val countryRepository: CountryRepository,
    private val playerRepository: PlayerRepository,
    private val olympicRepository: OlympicRepository,
    private val eventRepository: EventRepository,
    private val resultRepository: ResultRepository
) {

    @Transactional
    fun insertCountries(amount: Int): Response {
        var countriesCount = 0

        repeat(amount) {
            val country = dataGeneratorService.nextCountry()
            countriesCount += countryRepository.insertCountry(country)
        }

        return Response(newCountries = countriesCount)
    }

    @Transactional
    fun insertPlayers(amount: Int): Response {
        var updatedResponse = Response()

        repeat(amount) {
            val player = dataGeneratorService.nextPlayer()
            updatedResponse += insertPlayer(player)
        }

        return updatedResponse
    }

    @Transactional
    fun insertOlympics(amount: Int): Response {
        var updatedResponse = Response()

        repeat(amount) {
            val olympic = dataGeneratorService.nextOlympic()
            updatedResponse += insertOlympic(olympic)
        }

        return updatedResponse
    }

    @Transactional
    fun insertEvents(amount: Int): Response {
        var updatedResponse = Response()

        repeat(amount) {
            val event = dataGeneratorService.nextEvent()
            updatedResponse += insertEvent(event)
        }

        return updatedResponse
    }

    @Transactional
    fun insertResults(amount: Int): Response {
        var updatedResponse = Response()

        repeat(amount) {
            val result = dataGeneratorService.nextResult()
            updatedResponse += insertResult(result)
        }

        return updatedResponse
    }

    private fun insertResult(result: Result): Response {
        val event = result.event
        val player = result.player
        val eventResponse = insertEvent(event)
        val playerResponse = insertPlayer(player)

        val resultsCount = resultRepository.insertResult(result)
        val resultResponse = Response(newResults = resultsCount)

        return eventResponse + playerResponse + resultResponse
    }

    private fun insertOlympic(olympic: Olympic): Response {
        val country = olympic.country
        val countriesCount = countryRepository.insertCountry(country)
        val olympicsCount = olympicRepository.insertOlympic(olympic)

        return Response(newCountries = countriesCount, newOlympics = olympicsCount)
    }

    private fun insertEvent(event: Event): Response {
        val olympic = event.olympic
        val olympicResponse = insertOlympic(olympic)
        val eventsCount = eventRepository.insertEvent(event)
        val eventResponse = Response(newEvents = eventsCount)

        return olympicResponse + eventResponse
    }

    private fun insertPlayer(player: Player): Response {
        val country = player.country
        val countiesCount = countryRepository.insertCountry(country)
        val playersCount = playerRepository.insertPlayer(player)

        return Response(newPlayers = playersCount, newCountries = countiesCount)
    }
}