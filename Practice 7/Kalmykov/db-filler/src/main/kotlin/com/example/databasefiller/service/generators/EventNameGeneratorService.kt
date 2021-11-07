package com.example.databasefiller.service.generators

import com.example.databasefiller.models.event.EventNameWithType
import org.springframework.stereotype.Service
import kotlin.random.Random

private const val SWIMMING = "SWI"
private const val ATHLETIC = "ATH"

@Service
class EventNameGeneratorService {

    private val disciplines = mapOf(
        "Butterfly" to SWIMMING,
        "Freestyle" to SWIMMING,
        "Backstroke" to SWIMMING,
        "Individual Medley" to SWIMMING,
        "Hurdles" to ATHLETIC,
        "Breaststroke" to SWIMMING
    )

    private val eventNames = mapOf(
        "Triple Jump" to ATHLETIC,
        "Shot Put" to ATHLETIC,
        "Pole Vault" to ATHLETIC,
        "Marathon" to ATHLETIC,
        "Long Jump" to ATHLETIC,
        "Javelin Throw" to ATHLETIC,
        "High Jump" to ATHLETIC,
        "Heptathlon" to ATHLETIC,
        "Hammer Throw" to ATHLETIC,
        "Discus Throw" to ATHLETIC,
        "Decathlon" to ATHLETIC,
        "50km Walk" to ATHLETIC,
        "4x400m Relay" to ATHLETIC,
        "4x200m Freestyle Relay" to SWIMMING,
        "4x100m Relay" to ATHLETIC,
        "4x100m Medley Relay" to SWIMMING,
        "4x100m Freestyle Relay" to SWIMMING,
        "3000m Steeplechase" to ATHLETIC,
        "20km Walk" to ATHLETIC,
    )

    fun nextEventNameWithType(): EventNameWithType {
        val men = "Men"
        val women = "Women"

        val distances = IntArray(60) { idx -> (idx + 1) * 50 }
        val mappedDisciplines = disciplines.mapKeys {
            "${distances.random()} ${it.key} ${listOf(men, women).random()}"
        }

        val mappedEventNames = eventNames.mapKeys {
            "${it.key} ${listOf(men, women).random()}"
        }

        val selectedMap = if (Random.nextBoolean()) {
            mappedDisciplines
        } else {
            mappedEventNames
        }

        val eventName = selectedMap.keys.random()
        val eventType = selectedMap[eventName]!!

        return EventNameWithType(eventName, eventType)
    }
}