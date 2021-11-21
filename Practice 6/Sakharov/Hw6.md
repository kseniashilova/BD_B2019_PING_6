### Задание 6 Сахаров Никита БПИ196

* Для Олимпийских игр 2004 года сгенерируйте список (год рождения, количество игроков, количество золотых медалей),
  содержащий годы, в которые родились игроки, количество игроков, родившихся в каждый из этих лет, которые выиграли по
  крайней мере одну золотую медаль, и количество золотых медалей, завоеванных игроками, родившимися в этом году.

```postgresql
SELECT EXTRACT(year FROM players.birthdate) AS bth_year,
       COUNT(DISTINCT players.player_id)    AS players_cnt,
       COUNT(results.event_id)              AS medals_cnt
FROM results 
         JOIN events ON results.event_id = events.event_id
         JOIN olympics ON events.olympic_id = olympics.olympic_id
         JOIN players ON results.player_id = players.player_id
WHERE olympics.year = 2004
  AND results.medal = 'GOLD'
GROUP BY EXTRACT(year FROM players.birthdate);
```

* Перечислите все индивидуальные (не групповые) соревнования, в которых была ничья в счете, и два или более игрока
  выиграли золотую медаль.

```postgresql
SELECT e.event_id
FROM events 
         JOIN events  ON e.event_id = results.event_id
WHERE events.is_team_event = 0
  AND results.medal = 'GOLD'
GROUP BY events.event_id
HAVING COUNT(*) > 1;
```

* Найдите всех игроков, которые выиграли хотя бы одну медаль (GOLD, SILVER и BRONZE) на одной Олимпиаде. (player-name,
  olympic-id).

```postgresql
SELECT DISTINCT players.name, events.olympic_id
FROM players
         JOIN results ON results.player_id = players.player_id
         JOIN events ON events.event_id = results.event_id
WHERE results.medal IN ('GOLD', 'SILVER', 'BRONZE');
```

* В какой стране был наибольший процент игроков (из перечисленных в наборе данных), чьи имена начинались с гласной?

```postgresql
SELECT vowels_in_country.country_id, vowels * 1.0 / countries as percent
FROM(   SELECT players.country_id, COUNT(*) AS vowels
        FROM players
        WHERE LEFT (players.name, 1) IN ('A', 'E', 'I', 'O', 'U')
        GROUP BY players.country_id
    ) AS vowels_in_country
JOIN(   SELECT players.country_id, COUNT(*) AS countries
        FROM players
        GROUP BY players.country_id
    ) AS players_in_country
ON vowels_in_country.country_id = players_in_country.country_id
ORDER BY percent DESC LIMIT 1;
```
* Для Олимпийских игр 2000 года найдите 5 стран с минимальным соотношением количества групповых медалей к численности 
населения.
```postgresql
SELECT group_medals.country_id, 1.0 * group_medals.count / countries.population as percent
FROM(
    SELECT pl.country_id AS country_id, count(*) AS count
    FROM results res
    JOIN events ev ON res.event_id = ev.event_id
    JOIN olympics ol ON ev.olympic_id = ol.olympic_id
    JOIN players pl ON res.player_id = pl.player_id
    WHERE ol.year = 2000 AND ev.is_team_event = 1
    GROUP BY pl.country_id) AS group_medals
JOIN countries ON group_medals.country_id = countries.country_id
ORDER BY percent
LIMIT 5
```
