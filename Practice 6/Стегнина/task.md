* Для Олимпийских игр 2004 года сгенерируйте список (год рождения, количество игроков, количество золотых медалей), содержащий годы, в которые родились игроки, количество игроков, родившихся в каждый из этих лет, которые выиграли по крайней мере одну золотую медаль, и количество золотых медалей, завоеванных игроками, родившимися в этом году.
```sql
SELECT extract(YEAR FROM p.birthdate) as year_of_birth, COUNT(DISTINCT p.player_id) as num_of_players, COUNT(*) as num_of_gold
FROM players p
JOIN results on p.PLAYER_ID = results.PLAYER_ID AND results.medal = 'GOLD'
JOIN events on events.EVENT_ID = results.EVENT_ID
JOIN olympics on olympics.OLYMPIC_ID = events.OLYMPIC_ID AND olympics.year = 2004
GROUP BY year_of_birth
```

* Перечислите все индивидуальные (не групповые) соревнования, в которых была ничья в счете, и два или более игрока выиграли золотую медаль.
```sql
SELECT e.name, e.olympic_id as olymp
FROM events e
JOIN results r on e.event_id = r.event_id AND r.medal = 'GOLD'
WHERE e.is_team_event = 0
GROUP BY e.name, olymp
HAVING count(*) > 1
```

* Найдите всех игроков, которые выиграли хотя бы одну медаль (GOLD, SILVER и
BRONZE) на одной Олимпиаде. (player-name, olympic-id).
```sql
SELECT DISTINCT p.name, o.olympic_id
FROM players p
JOIN results r on p.player_id = r.player_id
JOIN events e on r.event_id = e.event_id
JOIN olympics o on e.olympic_id = o.olympic_id
```

* В какой стране был наибольший процент игроков (из перечисленных в наборе данных), чьи имена начинались с гласной?
```sql
SELECT c.name
FROM countries c
ORDER BY (
    SELECT count(*)
    FROM players p
    WHERE p.country_id = c.country_id AND
    left(p.name, 1) IN ('A', 'E', 'I', 'O', 'U')) /
         (
    SELECT count(*)
    FROM players p
    WHERE p.country_id = c.country_id
    ) DESC
LIMIT 1
```

* Для Олимпийских игр 2000 года найдите 5 стран с минимальным соотношением количества групповых медалей к численности населения.
```sql
SELECT c.name, (count(c.name) * 1.0 / c.population) as perc
    FROM countries c
    JOIN players p on c.country_id = p.country_id
    JOIN results r on p.player_id = r.player_id
    JOIN events e on e.event_id = r.event_id AND e.is_team_event = 1
    JOIN olympics o on o.olympic_id = e.olympic_id AND o.year = 2000
GROUP BY c.name, c.population
ORDER BY perc ASC
LIMIT 5
```
