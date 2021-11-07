package com.example.databasefiller.repository

import com.example.databasefiller.Datasource
import com.example.databasefiller.models.Olympic
import org.springframework.stereotype.Component


const val OLYMPIC_TABLE_NAME = "olympics"

@Component
class OlympicRepository(
    private val datasource: Datasource
) {

    init {
        createTable()
    }

    fun insertOlympic(olympic: Olympic): Int {
        var updated: Int

        val connection = datasource.getConnection()
        val preparedStatement = connection.prepareStatement(
            "INSERT INTO $OLYMPIC_TABLE_NAME VALUES (?, ?, ?, ?, ?, ?) ON CONFLICT DO NOTHING"
        )

        preparedStatement.use { statement ->
            statement.setString(1, olympic.olympicId)
            statement.setString(2, olympic.country.countryId)
            statement.setString(3, olympic.city)
            statement.setInt(4, olympic.year)
            statement.setDate(5, olympic.startDate)
            statement.setDate(6, olympic.endDate)

            updated = statement.executeUpdate()
        }

        return updated
    }


    private fun createTable() {
        val connection = datasource.getConnection()
        val statement = connection.createStatement()
        statement.use { statement ->
            statement.execute(
                """CREATE TABLE IF NOT EXISTS $OLYMPIC_TABLE_NAME(
                    | olympic_id CHAR(7) UNIQUE,
                    | country_id CHAR(3),
                    | city CHAR(50),
                    | year INTEGER,
                    | startdate DATE,
                    | enddate DATE,
                    | FOREIGN KEY (country_id) REFERENCES $COUNTRY_TABLE_NAME(country_id)
                    |);""".trimMargin()
            )
        }
    }
}
