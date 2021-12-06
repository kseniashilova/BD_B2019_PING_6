package com.example.databasefiller.repository

import com.example.databasefiller.Datasource
import com.example.databasefiller.models.Player
import org.springframework.stereotype.Component


const val PLAYER_TABLE_NAME = "players"

@Component
class PlayerRepository(
    private val datasource: Datasource
) {

    init {
        createTable()
    }

    fun insertPlayer(player: Player): Int {
        var updated: Int

        val connection = datasource.getConnection()
        val preparedStatement = connection.prepareStatement(
            """INSERT INTO $PLAYER_TABLE_NAME VALUES (?, ?, ?, ?) ON CONFLICT DO NOTHING""".trimMargin()
        )

        preparedStatement.use { statement ->
            statement.setString(1, player.name)
            statement.setString(2, player.playerId)
            statement.setString(3, player.country.countryId)
            statement.setDate(4, player.birthday)

            updated = statement.executeUpdate()
        }

        return updated
    }

    private fun createTable() {
        val connection = datasource.getConnection()
        val statement = connection.createStatement()
        statement.use { statement ->
            statement.execute(
                """CREATE TABLE IF NOT EXISTS $PLAYER_TABLE_NAME(
                    | name CHAR(40),
                    | player_id CHAR(10) UNIQUE,
                    | country_id CHAR(3),
                    | birthdate DATE,
                    | FOREIGN KEY (country_id) REFERENCES $COUNTRY_TABLE_NAME(country_id)
                    |);""".trimMargin()
            )
        }
    }
}
