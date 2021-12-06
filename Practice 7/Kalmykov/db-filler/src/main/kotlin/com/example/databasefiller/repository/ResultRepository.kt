package com.example.databasefiller.repository

import com.example.databasefiller.Datasource
import com.example.databasefiller.models.Result
import org.springframework.stereotype.Repository

const val RESULT_TABLE_NAME = "results"

@Repository
class ResultRepository(
    private val datasource: Datasource
) {

    init {
        createTable()
    }

    fun insertResult(result: Result): Int {
        var updated: Int

        val connection = datasource.getConnection()
        val preparedStatement = connection.prepareStatement(
            """INSERT INTO $RESULT_TABLE_NAME VALUES (?, ?, ?, ?)
                """.trimIndent()
        )

        preparedStatement.use { statement ->
            statement.setString(1, result.event.eventId)
            statement.setString(2, result.player.playerId)
            statement.setString(3, result.medal)
            statement.setFloat(4, result.result)

            updated = preparedStatement.executeUpdate()
        }

        return updated
    }

    private fun createTable() {
        val connection = datasource.getConnection()
        val statement = connection.createStatement()
        statement.use { statement ->
            statement.execute(
                """CREATE TABLE IF NOT EXISTS $RESULT_TABLE_NAME(
                    | event_id CHAR(7),
                    | player_id CHAR(10),
                    | medal CHAR(7),
                    | result FLOAT,
                    | FOREIGN KEY (event_id) REFERENCES $EVENT_TABLE_NAME(event_id),
                    | FOREIGN KEY (player_id) REFERENCES $PLAYER_TABLE_NAME(player_id)
                    |);""".trimMargin()
            )
        }
    }
}