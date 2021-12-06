package com.example.databasefiller

import org.springframework.beans.factory.annotation.Value
import org.springframework.stereotype.Component
import java.sql.Connection
import java.sql.DriverManager

@Component
class Datasource {

    @Value("\${spring.datasource.url}")
    private lateinit var connectionUrl: String

    @Value("\${spring.datasource.username}")
    private lateinit var username: String

    @Value("\${spring.datasource.password}")
    private lateinit var password: String

    private var connection: Connection? = null

    fun getConnection(): Connection {
        if (connection == null) {
            connection = DriverManager.getConnection(
                connectionUrl, username, password
            )
        }

        return connection!!
    }

    fun close() = connection?.close()
}