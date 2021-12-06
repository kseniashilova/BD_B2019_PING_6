package com.example.databasefiller.models

import com.example.databasefiller.models.country.Country
import java.sql.Date

const val MIN_BIRTHDAY_YEAR = 1980
const val MAX_BIRTHDAY_YEAR = 2002

data class Player(
    val name: String,
    val playerId: String,
    val country: Country,
    val birthday: Date
)
