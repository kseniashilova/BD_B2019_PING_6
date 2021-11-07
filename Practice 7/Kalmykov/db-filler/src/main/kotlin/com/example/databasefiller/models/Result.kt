package com.example.databasefiller.models

import com.example.databasefiller.models.event.Event

const val MIN_RESULT = 2
const val MAX_RESULT = 250

data class Result(
    val event: Event,
    val player: Player,
    val medal: String,
    val result: Float,
)
