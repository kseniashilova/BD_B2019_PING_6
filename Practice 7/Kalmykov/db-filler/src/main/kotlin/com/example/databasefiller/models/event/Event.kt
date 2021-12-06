package com.example.databasefiller.models.event

import com.example.databasefiller.models.Olympic

const val MAX_PLAYERS_IN_TEAM = 7
const val MIN_PLAYERS_IN_TEAM = 4

data class Event(
    var eventId: String,
    val name: String,
    val eventType: String,
    val olympic: Olympic,
    val isTeamEvent: Int,
    val playersNumberInTeam: Int,
    val resultNotedIn: String
)
