package com.example.databasefiller.repository

import com.example.databasefiller.Datasource
import com.example.databasefiller.models.event.Event
import org.springframework.stereotype.Component
import java.util.concurrent.atomic.AtomicInteger


const val EVENT_TABLE_NAME = "events"

@Component
class EventRepository(
    private val datasource: Datasource
) {

    private val counter = AtomicInteger(1)

    init {
        createTable()
    }

    fun insertEvent(event: Event): Int {
        var updated: Int

        val connection = datasource.getConnection()
        val preparedStatement = connection.prepareStatement(
            "INSERT INTO $EVENT_TABLE_NAME VALUES (?, ?, ?, ?, ?, ?, ?) ON CONFLICT DO NOTHING"
        )

        event.eventId = "E${counter.getAndIncrement()}"

        preparedStatement.use { statement ->
            statement.setString(1, event.eventId)
            statement.setString(2, event.name)
            statement.setString(3, event.eventType)
            statement.setString(4, event.olympic.olympicId)
            statement.setInt(5, event.isTeamEvent)
            statement.setInt(6, event.playersNumberInTeam)
            statement.setString(7, event.resultNotedIn)

            updated = statement.executeUpdate()
        }

        return updated
    }


    private fun createTable() {
        val connection = datasource.getConnection()
        val statement = connection.createStatement()
        statement.use { statement ->
            statement.execute(
                """CREATE TABLE IF NOT EXISTS $EVENT_TABLE_NAME(
                    | event_id CHAR(7) UNIQUE,
                    | name CHAR(40),
                    | eventtype CHAR(20),
                    | olympic_id CHAR(7),
                    | is_team_event INTEGER CHECK (is_team_event IN (0, 1)),
                    | num_players_in_team INTEGER,
                    | result_noted_in CHAR(100),
                    | FOREIGN KEY (olympic_id) REFERENCES $OLYMPIC_TABLE_NAME(olympic_id)
                    |);""".trimMargin()
            )
        }
    }
}
