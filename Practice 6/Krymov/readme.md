## П. 1

Для Олимпийских игр 2004 года сгенерируйте список (год рождения, количество игроков,
количество золотых медалей), содержащий годы, в которые родились игроки, количество игроков,
родившихся в каждый из этих лет, которые выиграли по крайней мере одну золотую медаль, и количество золотых медалей, завоеванных игроками, родившимися в этом году.

```sql
SELECT
    EXTRACT(YEAR FROM players.birthdate) as birthdate_year,
    COUNT(DISTINCT players.player_id) as players_count,
    COUNT(*) as gold_medals_count
FROM players
         JOIN results on players.PLAYER_ID = results.PLAYER_ID
         JOIN events on events.EVENT_ID = results.EVENT_ID
         JOIN olympics on olympics.OLYMPIC_ID = events.OLYMPIC_ID
WHERE olympics.year = 2004
  AND results.medal = 'GOLD'
GROUP BY birthdate_year;
```

## П. 2

Перечислите все индивидуальные (не групповые) соревнования, в которых была ничья в счете, и два или более игрока выиграли золотую медаль.

```sql
SELECT
    events.name as event_name,
    events.event_id as event_id
FROM results
         JOIN events ON events.event_id = results.event_id
WHERE results.medal = 'GOLD'
  AND events.is_team_event = 0
GROUP BY
    events.name,
    events.event_id
HAVING
    count(*) >= 2;
```

## П. 3

Найдите всех игроков, которые выиграли хотя бы одну медаль (GOLD, SILVER и BRONZE) на одной Олимпиаде. (player-name, olympic-id).

```sql
SELECT
    players.name as player_name,
    olympics.olympic_id as olympic_id
FROM players
    JOIN results ON players.player_id = results.player_id
    JOIN events ON results.event_id = events.event_id
    JOIN olympics ON events.olympic_id = olympics.olympic_id;
```

## П. 4

В какой стране был наибольший процент игроков (из перечисленных в наборе данных), чьи имена начинались с гласной?

```sql
SELECT countries.name
FROM countries
ORDER BY (
             (SELECT count(*) FROM players
              WHERE players.country_id = countries.country_id
              AND left(players.name, 1) IN ('A', 'I', 'E', 'O', 'U'))

             /

             (SELECT count(*) FROM players
              WHERE players.country_id = countries.country_id)
        ) DESC
LIMIT 1
```

## П. 5

Для Олимпийских игр 2000 года найдите 5 стран с минимальным соотношением количества групповых медалей к численности населения.

```sql
SELECT
    countries.name as country_name,
    (1.0 * count(countries.name) / countries.population) as pdiv
FROM countries
         JOIN players on countries.country_id = players.country_id
         JOIN results on players.player_id = results.player_id
         JOIN events on events.event_id = results.event_id AND events.is_team_event = 1
         JOIN olympics on olympics.year = 2000 AND olympics.olympic_id = events.olympic_id
GROUP BY
    country_name, countries.population
ORDER BY pdiv ASC
    LIMIT 5
```