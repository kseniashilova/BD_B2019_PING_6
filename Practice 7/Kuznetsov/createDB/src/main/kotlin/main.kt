import com.github.javafaker.Faker
import java.io.BufferedWriter
import java.io.File
import java.text.SimpleDateFormat
import java.util.*
import kotlin.math.abs

class OlympicsDBFakeCreator(
    private val countCountries: Int,
    private val countOlympics: Int,
    private val countPlayers: Int,
    private val countEvents: Int,
) {

    private val faker = Faker()

    fun toFile(path: String) {
        File(path).bufferedWriter().use { bufferedWriter: BufferedWriter ->
            dropTables(bufferedWriter)
            createTables(bufferedWriter)
            createCountries(bufferedWriter)
            createOlympics(bufferedWriter)
            createPlayers(bufferedWriter)
            createEventsAndResults(bufferedWriter)
        }
    }

    private fun dropTables(bufferedWriter: BufferedWriter) {
        bufferedWriter.write("drop table Results;\n")
        bufferedWriter.write("drop table Players;\n")
        bufferedWriter.write("drop table Events;\n")
        bufferedWriter.write("drop table Olympics;\n")
        bufferedWriter.write("drop table Countries;\n")
        bufferedWriter.newLine()
    }

    private fun createTables(bufferedWriter: BufferedWriter) {
        bufferedWriter.write("create table Countries (\n")
        bufferedWriter.write("    name char(40),\n")
        bufferedWriter.write("    country_id char(3) unique,\n")
        bufferedWriter.write("    area_sqkm integer,\n")
        bufferedWriter.write("    population integer\n")
        bufferedWriter.write(");\n")
        bufferedWriter.newLine()
        bufferedWriter.write("create table Olympics (\n")
        bufferedWriter.write("    olympic_id char(7) unique,\n")
        bufferedWriter.write("    country_id char(3),\n")
        bufferedWriter.write("    city char(50),\n")
        bufferedWriter.write("    year integer,\n")
        bufferedWriter.write("    startdate date,\n")
        bufferedWriter.write("    enddate date,\n")
        bufferedWriter.write("    foreign key (country_id) references Countries(country_id)\n")
        bufferedWriter.write(");\n")
        bufferedWriter.newLine()
        bufferedWriter.write("create table Players (\n")
        bufferedWriter.write("    name char(40),\n")
        bufferedWriter.write("    player_id char(10) unique,\n")
        bufferedWriter.write("    country_id char(3),\n")
        bufferedWriter.write("    birthdate date,\n")
        bufferedWriter.write("    foreign key (country_id) references Countries(country_id)\n")
        bufferedWriter.write(");\n")
        bufferedWriter.newLine()
        bufferedWriter.write("create table Events (\n")
        bufferedWriter.write("    event_id char(7) unique,\n")
        bufferedWriter.write("    name char(40),\n")
        bufferedWriter.write("    eventtype char(20),\n")
        bufferedWriter.write("    olympic_id char(7),\n")
        bufferedWriter.write("    is_team_event integer check (is_team_event in (0, 1)),\n")
        bufferedWriter.write("    num_players_in_team integer,\n")
        bufferedWriter.write("    result_noted_in char(100),\n")
        bufferedWriter.write("    foreign key (olympic_id) references Olympics(olympic_id)\n")
        bufferedWriter.write(");\n")
        bufferedWriter.newLine()
        bufferedWriter.write("create table Results (\n")
        bufferedWriter.write("    event_id char(7),\n")
        bufferedWriter.write("    player_id char(10),\n")
        bufferedWriter.write("    medal char(7),\n")
        bufferedWriter.write("    result float,\n")
        bufferedWriter.write("    foreign key (event_id) references Events(event_id),\n")
        bufferedWriter.write("    foreign key (player_id) references players(player_id)\n")
        bufferedWriter.write(");\n")
        bufferedWriter.newLine()
    }

    private fun createCountries(bufferedWriter: BufferedWriter) {
        for (countryId in 1..countCountries) {
            val name = faker.country().name()
            val areasSQKM = faker.number().randomNumber()
            val population = faker.number().randomNumber()
            bufferedWriter.write("insert into Countries values('$name', '$countryId', $areasSQKM, $population);\n")
        }
        bufferedWriter.newLine()
    }

    private fun createOlympics(bufferedWriter: BufferedWriter) {
        for (olympicId in 1..countOlympics) {
            var year = faker.number().numberBetween(1980, 2021)
            year -= year % 4
            val countryId = faker.number().numberBetween(1, countCountries + 1)
            val city = faker.country().capital()
            val startYear = Date(year - 1900, 0, 1)
            val endYear = Date(year - 1900, 11, 31)
            val startDate = faker.date().between(startYear, endYear)
            val endDate = faker.date().between(startDate, endYear)
            val startDateString = SimpleDateFormat("dd-MM-yyyy").format(startDate)
            val endDateString = SimpleDateFormat("dd-MM-yyyy").format(endDate)
            bufferedWriter.write("insert into Olympics values('$olympicId', '$countryId', '$city', $year," +
                    " to_date('$startDateString', 'dd-mm-yyyy'), to_date('$endDateString', 'dd-mm-yyyy'));\n")
        }
        bufferedWriter.newLine()
    }

    private fun createPlayers(bufferedWriter: BufferedWriter) {
        for (playerId in 1..countPlayers) {
            val countryId = faker.number().numberBetween(1, countCountries + 1)
            val name = faker.name().name().replace('`',' ')
            val birthdate = faker.date().birthday()
            val birthdateString = SimpleDateFormat("dd-MM-yyyy").format(birthdate)
            bufferedWriter.write("insert into Players values('$name', '$playerId', '$countryId', " +
                    "to_date('$birthdateString', 'dd-mm-yyyy'));\n")
        }
        bufferedWriter.newLine()
    }

    private fun createEventsAndResults(bufferedWriter: BufferedWriter) {
        for (eventId in 1..countEvents) {
            val olympicId = faker.number().numberBetween(1, countOlympics + 1)
            val name = faker.esports().event()
            val eventType = faker.esports().league()
            val isTeamEvent = faker.number().numberBetween(0, 2)
            val numPlayersInTeam = faker.number().numberBetween(3, 12) * isTeamEvent - 1
            val resultNotedIn = RESULT_NOTED_IN_LIST[faker.number().numberBetween(0, 3)]
            bufferedWriter.write("insert into Events values('$eventId', '$name', '$eventType', '$olympicId', " +
                    "$isTeamEvent, $numPlayersInTeam, '$resultNotedIn');\n")

            for (medalId in 0..2) {
                val medal = MEDALS_LIST[medalId]
                val result = faker.number().randomDigitNotZero()
                // 1 из 100 ничья
                for (i in 0..(faker.number().numberBetween(1, 101) / 100)) {
                    for (currentPlayer in 1..abs(numPlayersInTeam)) {
                        val playersId = faker.number().numberBetween(1, countPlayers + 1)
                        bufferedWriter.write(
                            "insert into Results values('$eventId', '$playersId', '$medal', $result);\n"
                        )
                    }
                }
            }

            bufferedWriter.newLine()
        }
    }

    companion object {

        val RESULT_NOTED_IN_LIST = listOf("seconds", "points", "meters")
        val MEDALS_LIST = listOf("GOLD", "SILVER", "BRONZE")

    }
}

fun main(args: Array<String>) {
    println("Write path to file")
    val path = readLine() ?: ""
    println("Write count countries")
    val countCountries = readLine()?.toInt() ?: 0
    println("Write count olympics")
    val countOlympics = readLine()?.toInt() ?: 0
    println("Write count players")
    val countPlayers = readLine()?.toInt() ?: 0
    println("Write count events")
    val countEvents = readLine()?.toInt() ?: 0
    OlympicsDBFakeCreator(
        countCountries = countCountries,
        countOlympics = countOlympics,
        countPlayers = countPlayers,
        countEvents = countEvents
    ).toFile(path)
}