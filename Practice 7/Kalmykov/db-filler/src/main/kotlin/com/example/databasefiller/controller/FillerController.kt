package com.example.databasefiller.controller

import com.example.databasefiller.service.DatabaseFillerService
import org.springframework.web.bind.annotation.GetMapping
import org.springframework.web.bind.annotation.RequestParam
import org.springframework.web.bind.annotation.RestController

@RestController
class FillerController(
    private val databaseFillerService: DatabaseFillerService
) {

    @GetMapping("/countries")
    fun insertCountries(@RequestParam amount: Int) =
        databaseFillerService.insertCountries(amount)

    @GetMapping("/players")
    fun insertPlayers(@RequestParam amount: Int) =
        databaseFillerService.insertPlayers(amount)

    @GetMapping("/olympics")
    fun insertOlympics(@RequestParam amount: Int) =
        databaseFillerService.insertOlympics(amount)

    @GetMapping("/events")
    fun insertEvents(@RequestParam amount: Int) =
        databaseFillerService.insertEvents(amount)

    @GetMapping("/results")
    fun insertResults(@RequestParam amount: Int) =
        databaseFillerService.insertResults(amount)
}