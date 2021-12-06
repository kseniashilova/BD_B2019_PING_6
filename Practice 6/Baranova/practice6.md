## Задание 6

Напишие SQL запросы

### Задание 1
*Для Олимпийских игр 2004 года сгенерируйте список (год рождения, количество игроков, количество золотых медалей), содержащий годы, в которые родились игроки, количество игроков, родившихся в каждый из этих лет, которые выиграли по крайней мере одну золотую медаль, и количество золотых медалей, завоеванных игроками, родившимися в этом году.*

```sql
SELECT EXTRACT(year FROM players.birthdate) birth_year,
       count(DISTINCT players.player_id) n_players,
       count(*) n_medals
FROM players
JOIN results on players.player_id = results.player_id
JOIN events on results.event_id = events.event_id
JOIN olympics on olympics.olympic_id = events.olympic_id
WHERE results.medal = 'GOLD' AND olympics.year = 2004
GROUP BY EXTRACT(year FROM players.birthdate);
```

### Задание 2
*Перечислите все индивидуальные (не групповые) соревнования, в которых была ничья в счете, и два или более игрока выиграли золотую медаль.*

```sql
SELECT events.event_id, events.name FROM events
JOIN results ON events.event_id = results.event_id
WHERE events.is_team_event = 0
AND results.medal = 'GOLD'
GROUP BY events.event_id, events.name
HAVING count(*) >= 2;
```

### Задание 3
*Найдите всех игроков, которые выиграли хотя бы одну медаль (GOLD, SILVER и BRONZE) на одной Олимпиаде. (player-name, olympic-id).*

```sql
SELECT players.name, olympics.olympic_id FROM players
    JOIN results ON players.player_id = results.player_id
    JOIN events ON results.event_id = events.event_id
    JOIN olympics ON events.olympic_id = olympics.olympic_id;
```

### Задание 4
*В какой стране был наибольший процент игроков (из перечисленных в наборе данных), чьи имена начинались с гласной?*

Если достаточно найти одну страну:

```sql
SELECT country
FROM (
    SELECT countries.name country,
           CAST(count(*) AS DECIMAL) / total.total_count vowel_names_percentage
    FROM players
        JOIN countries on countries.country_id = players.country_id
        JOIN (
            SELECT countries.country_id, count(*) total_count
            FROM players
                JOIN countries ON countries.country_id = players.country_id
            GROUP BY countries.country_id
            ) total ON total.country_id = countries.country_id
    WHERE players.name ~* '^[aeiou].*'
    GROUP BY countries.name, total.total_count
    ) AS percentage
ORDER BY vowel_names_percentage DESC
LIMIT 1;
```

Если нужно найти все такие страны (в предложенном наборе у 4х стран имена всех спортсменов начинаются с гласной):

```sql
WITH percentage(country, vowel_names_percentage) AS (
    SELECT countries.name, CAST(count(*) AS DECIMAL) / total.total_count
    FROM players
        JOIN countries on countries.country_id = players.country_id
        JOIN (
            SELECT countries.country_id, count(*) total_count
            FROM players
                JOIN countries ON countries.country_id = players.country_id
            GROUP BY countries.country_id
            ) total ON total.country_id = countries.country_id
    WHERE players.name ~* '^[aeiou].*'
    GROUP BY countries.name, total.total_count
)

SELECT country FROM percentage
WHERE vowel_names_percentage = (
    SELECT max(vowel_names_percentage) FROM percentage
    );
```

### Задание 5
*Для Олимпийских игр 2000 года найдите 5 стран с минимальным соотношением количества групповых медалей к численности населения.*

```sql
WITH MedalsCount(country, count) AS (
    SELECT countries.name, count(*) FROM events
    JOIN results on events.event_id = results.event_id
    JOIN olympics on events.olympic_id = olympics.olympic_id
    JOIN players on players.player_id = results.player_id
    JOIN countries on countries.country_id = players.country_id
    WHERE events.is_team_event = 1 AND olympics.year = 2000
    GROUP BY countries.name
)

SELECT countries.name
FROM countries
JOIN MedalsCount ON MedalsCount.country = countries.name
ORDER BY CAST(MedalsCount.count AS DECIMAL) / countries.population
LIMIT 5;
```
