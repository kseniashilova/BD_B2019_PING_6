# Домашнее задание №6

1. Cписок (год рождения, количество игроков, количество золотых медалей), содержащий годы, в которые родились игроки,
   количество игроков, родившихся в каждый из этих лет, которые выиграли по крайней мере одну золотую медаль, и
   количество золотых медалей, завоеванных игроками, родившимися в этом году.

```sql
SELECT EXTRACT(YEAR FROM players.birthdate) as yob,
       COUNT(DISTINCT players.player_id)    as players,
       COUNT(results.medal)                 as medals
FROM olympics
         JOIN events ON events.olympic_id = olympics.olympic_id
         JOIN results ON events.event_id = results.event_id
         JOIN players ON players.player_id = results.player_id
WHERE year = 2004 AND results.medal = 'GOLD'
GROUP BY 1;
```

2. Все индивидуальные (не групповые) соревнования, в которых была ничья в счете, и два или более игрока выиграли золотую
   медаль.

```sql
SELECT events.event_id, events.name
FROM events
         JOIN results ON events.event_id = results.event_id
WHERE is_team_event = 0
  AND results.medal = 'GOLD'
GROUP BY events.event_id, events.name
HAVING COUNT(results.medal) >= 2;
```

3. Все игроки, которые выиграли хотя бы одну медаль (GOLD, SILVER и BRONZE) на одной Олимпиаде. (player-name,
   olympic-id).

```sql
SELECT DISTINCT players.name, events.olympic_id
FROM players
         JOIN results ON results.player_id = players.player_id
         JOIN events ON events.event_id = results.event_id
WHERE results.medal IN ('GOLD', 'SILVER', 'BRONZE');
```

4. В какой стране был наибольший процент игроков (из перечисленных в наборе данных), чьи имена начинались с гласной?

```sql
SELECT c1.country_id, vowels * 1.0 / countries
FROM (
         SELECT players.country_id, COUNT(*) AS vowels
         FROM players
         WHERE LEFT (players.name, 1) IN ('A', 'E', 'I', 'O', 'U')
         GROUP BY players.country_id
     ) AS c1
         JOIN (
    SELECT players.country_id, COUNT(*) AS countries
    FROM players
    GROUP BY players.country_id
) AS c2 ON c1.country_id = c2.country_id
ORDER BY 2 DESC LIMIT 1;
```

5. Для Олимпийских игр 2000 года найдите 5 стран с минимальным соотношением количества групповых медалей к численности
   населения.

```sql
SELECT countries.country_id, COUNT(results.medal) * 1.0 / population
FROM olympics
         JOIN events ON events.olympic_id = olympics.olympic_id
         JOIN results ON events.event_id = results.event_id
         JOIN players ON players.player_id = results.player_id
         JOIN countries ON countries.country_id = players.country_id
WHERE year = 2000 AND is_team_event = 1
GROUP BY countries.country_id, population
ORDER BY 2 LIMIT 5;
```