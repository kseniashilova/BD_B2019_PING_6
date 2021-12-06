package com.example.databasefiller.repository

import com.example.databasefiller.Datasource
import com.example.databasefiller.models.country.Country
import org.springframework.stereotype.Component


const val COUNTRY_TABLE_NAME = "countries"

@Component
class CountryRepository(
    private val datasource: Datasource
) {

    init {
        createTable()
    }

    fun insertCountry(country: Country): Int {
        var updated: Int

        val connection = datasource.getConnection()
        val preparedStatement = connection.prepareStatement(
            "INSERT INTO $COUNTRY_TABLE_NAME VALUES (?, ?, ?, ?)  ON CONFLICT DO NOTHING"
        )

        preparedStatement.use { statement ->
            statement.setString(1, country.name)
            statement.setString(2, country.countryId)
            statement.setInt(3, country.area)
            statement.setInt(4, country.population)

            updated = statement.executeUpdate()
        }

        return updated
    }


    private fun createTable() {
        val connection = datasource.getConnection()
        val statement = connection.createStatement()
        statement.use { statement ->
            statement.execute(
                """CREATE TABLE IF NOT EXISTS $COUNTRY_TABLE_NAME(
                    | name CHAR(40),
                    | country_id CHAR(3) UNIQUE,
                    | area_sqkm INTEGER,
                    | population INTEGER
                    |);""".trimMargin()
            )
        }
    }
}
