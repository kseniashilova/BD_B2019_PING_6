package com.example.databasefiller.models.country


const val MIN_AREA = 1
const val MAX_AREA = 17_125_000

const val MIN_POPULATION = 836
const val MAX_POPULATION = 1_449_670_556

data class Country(
    val name: String,
    val countryId: String,
    val area: Int,
    val population: Int
)
