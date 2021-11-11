Для гененрирования было использован Kotlin с подлючением Java Faker(https://github.com/DiUS/java-faker)

Запусть можно по адрессу .\createDB\build\distributions\createDB-1.0-SNAPSHOT\bin\createDB.bat

Будет сгенирированный файл с командами, которые нужно запустить для генерации бд, пример есть в .\createDB\build\distributions\createDB-1.0-SNAPSHOT\bin\

Можно указать колличество стран, олимпиад, участников и событий, колличество результатов определяется из колличества событий

Теперь к гененрации

Сначала просто удаляем старые таблицы и добовляе их по-новой, тут просто в файл пишутся захардкоженные команды например код для добовления команд для удаления таблиц

```kotlin
private fun dropTables(bufferedWriter: BufferedWriter) {
        bufferedWriter.write("drop table Results;\n")
        bufferedWriter.write("drop table Players;\n")
        bufferedWriter.write("drop table Events;\n")
        bufferedWriter.write("drop table Olympics;\n")
        bufferedWriter.write("drop table Countries;\n")
        bufferedWriter.newLine()
    }
```
Для странны для имени изпользовался faker.country().name() для популяции и размера територии faker.number().randomNumber()

```kotlin
private fun createCountries(bufferedWriter: BufferedWriter) {
    for (countryId in 1..countCountries) {
        val name = faker.country().name()
        val areasSQKM = faker.number().randomNumber()
        val population = faker.number().randomNumber()
        bufferedWriter.write("insert into Countries values('$name', '$countryId', $areasSQKM, $population);\n")
    }
    bufferedWriter.newLine()
}
```
Для олимпиады сначала гененрируется год, который потом приводится к году кратному 4. Id это просто число от 1 до колличества олимпиад. Город получается с помошью  faker.country().capital(). Дата начала олимпиадв выбирается случайно с помощью faker.date().between и дата берется между началом и концом выбранного года. Дата конца одимпиады это дата между датой начала олимпиады и датой конца года
```kotlin
    private fun createOlympics(bufferedWriter: BufferedWriter) {
        for (olympicId in 1..countOlympics) {
            var year = faker.number().numberBetween(1980, 2021)
            year -= year % 4
            val countryId = faker.number().numberBetween(1, countCountries + 1)
            val city = faker.country().capital()
            val startYear = Date(year - 1900, 0, 1)
            val endYear = Date(year - 1900, 11, 31)
            val startDate = faker.date().between(startYear, endYear)
            val endDate = faker.date().between(startDate, endYear)
            val startDateString = SimpleDateFormat("dd-MM-yyyy").format(startDate)
            val endDateString = SimpleDateFormat("dd-MM-yyyy").format(endDate)
            bufferedWriter.write("insert into Olympics values('$olympicId', '$countryId', '$city', $year," +
                    " to_date('$startDateString', 'dd-mm-yyyy'), to_date('$endDateString', 'dd-mm-yyyy'));\n")
        }
        bufferedWriter.newLine()
    }
```

Для генерации участников для именни используетя faker.name().name(), но с заменой \`(апостров) на пробелы так как \`(апостров) не всегда поддерживается. Для дня рождения использовались faker.date().birthday()

```kotlin
private fun createPlayers(bufferedWriter: BufferedWriter) {
    for (playerId in 1..countPlayers) {
        val countryId = faker.number().numberBetween(1, countCountries + 1)
        val name = faker.name().name().replace('`',' ')
        val birthdate = faker.date().birthday()
        val birthdateString = SimpleDateFormat("dd-MM-yyyy").format(birthdate)
        bufferedWriter.write("insert into Players values('$name', '$playerId', '$countryId', " +
                "to_date('$birthdateString', 'dd-mm-yyyy'));\n")
    }
    bufferedWriter.newLine()
}
```
Для генерации событий для названия используется faker.esports().event(), для типа faker.esports().league(). Колличество игроков определяется faker.number().numberBetween, если это одиночное событие то оно становится -1. Единицы измерения просто выбираются между секундами, метрами и очками. Дальше для каждого события генерируются все призовые места. Резултат это просто не нудевое число faker.number().randomDigitNotZero(). Так же есть 1% шанс нечьи для каждой медали.

```kotlin
private fun createEventsAndResults(bufferedWriter: BufferedWriter) {
    for (eventId in 1..countEvents) {
        val olympicId = faker.number().numberBetween(1, countOlympics + 1)
        val name = faker.esports().event()
        val eventType = faker.esports().league()
        val isTeamEvent = faker.number().numberBetween(0, 2)
        val numPlayersInTeam = faker.number().numberBetween(3, 12) * isTeamEvent - 1
        val resultNotedIn = RESULT_NOTED_IN_LIST[faker.number().numberBetween(0, 3)]
        bufferedWriter.write("insert into Events values('$eventId', '$name', '$eventType', '$olympicId', " +
                "$isTeamEvent, $numPlayersInTeam, '$resultNotedIn');\n")

        for (medalId in 0..2) {
            val medal = MEDALS_LIST[medalId]
            val result = faker.number().randomDigitNotZero()
            // 1 из 100 ничья
            for (i in 0..(faker.number().numberBetween(1, 101) / 100)) {
                for (currentPlayer in 1..abs(numPlayersInTeam)) {
                    val playersId = faker.number().numberBetween(1, countPlayers + 1)
                    bufferedWriter.write(
                        "insert into Results values('$eventId', '$playersId', '$medal', $result);\n"
                    )
                }
            }
        }

        bufferedWriter.newLine()
    }
}

companion object {

    val RESULT_NOTED_IN_LIST = listOf("seconds", "points", "meters")
    val MEDALS_LIST = listOf("GOLD", "SILVER", "BRONZE")

}
```