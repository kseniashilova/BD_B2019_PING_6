package com.example.databasefiller.models

data class Response(
    var newPlayers: Int = 0,
    var newCountries: Int = 0,
    var newResults: Int = 0,
    var newOlympics: Int = 0,
    var newEvents: Int = 0
) {

    operator fun plus(other: Response) = Response(
        this.newPlayers + other.newPlayers,
        this.newCountries + other.newCountries,
        this.newResults + other.newResults,
        this.newOlympics + other.newOlympics,
        this.newEvents + other.newEvents
    )
}