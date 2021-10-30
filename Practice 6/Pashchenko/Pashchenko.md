# **Задача 1**
Для Олимпийских игр 2004 года сгенерируйте список (год рождения, количество игроков, количество золотых медалей), содержащий годы, в которые родились игроки, количество игроков, родившихся в каждый из этих лет, которые выиграли по крайней мере одну золотую медаль, и количество золотых медалей, завоеванных игроками, родившимися в этом году. 

SELECT
EXTRACT(YEAR FROM players.birthdate) as birth\_year,
COUNT(DISTINCT players.player\_id) as players\_count,
COUNT(results.medal) as gold\_medals\_count
FROM olympics
JOIN events ON olympics.olympic\_id = events.olympic\_id
JOIN results ON events.event\_id = results.event\_id
JOIN players ON results.player\_id = players.player\_id
WHERE results.medal = 'GOLD' AND year = 2004
GROUP BY birth\_year;
# **Задача 2**
Перечислите все индивидуальные (не групповые) соревнования, в которых была ничья в счете, и два или более игрока выиграли золотую медаль. 

SELECT events.name
FROM events
JOIN results ON events.event\_id = results.event\_id
WHERE results.medal = 'GOLD' AND is\_team\_event = 0
GROUP BY results.result, events.name
HAVING COUNT(results.medal) > 1;

# **Задача 3**
Найдите всех игроков, которые выиграли хотя бы одну медаль (GOLD, SILVER и BRONZE) на одной Олимпиаде. (player-name, olympic-id). 

SELECT DISTINCT players.name, events.olympic\_id
FROM players
`         `JOIN results ON players.player\_id = results.player\_id
`         `JOIN events ON results.event\_id = events.event\_id;

# **Задача 4**
В какой стране был наибольший процент игроков (из перечисленных в наборе данных), чьи имена начинались с гласной?
# **Задача 5**
` `Для Олимпийских игр 2000 года найдите 5 стран с минимальным соотношением количества групповых медалей к численности населения.

SELECT countries.country\_id, COUNT(results.medal) \* 1.0 / population as medal\_percentage
FROM olympics
`         `JOIN events ON olympics.olympic\_id = events.olympic\_id
`         `JOIN results ON events.event\_id = results.event\_id
`         `JOIN players ON results.player\_id = players.player\_id
`         `JOIN countries ON players.country\_id = countries.country\_id
WHERE is\_team\_event = 1 AND year = 2000
GROUP BY countries.country\_id, population
ORDER BY medal\_percentage LIMIT 5;
