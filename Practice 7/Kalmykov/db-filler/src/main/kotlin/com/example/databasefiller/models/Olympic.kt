package com.example.databasefiller.models

import com.example.databasefiller.models.country.Country
import java.sql.Date

const val MIN_YEAR = 1980
const val MAX_YEAR = 2010

data class Olympic(
    val olympicId: String,
    val country: Country,
    val city: String,
    val year: Int,
    val startDate: Date,
    val endDate: Date
)
