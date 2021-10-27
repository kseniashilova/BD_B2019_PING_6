## Задание 1

Для Олимпийских игр 2004 года сгенерируйте список (год рождения, количество игроков,
количество золотых медалей), содержащий годы, в которые родились игроки, количество игроков, 
родившихся в каждый из этих лет, которые выиграли по крайней мере одну золотую медаль, и количество золотых медалей, завоеванных игроками, родившимися в этом году.

## Задание 2

Перечислите все индивидуальные (не групповые) соревнования, в которых была ничья в счете, и два или более игрока выиграли золотую медаль.

```sql
SELECT e.name, e.event_id
FROM results r
         JOIN events e ON e.event_id = r.event_id
WHERE e.is_team_event = 0
  AND r.medal = 'GOLD'
GROUP BY e.name, e.event_id
HAVING count(*) >= 2;
```

## Задание 3

Найдите всех игроков, которые выиграли хотя бы одну медаль (GOLD, SILVER и BRONZE) на одной Олимпиаде. (player-name, olympic-id).

```sql
SELECT p.name, o.olympic_id
FROM players p
         JOIN results r ON p.player_id = r.player_id
         JOIN events e ON r.event_id = e.event_id
         JOIN olympics o ON e.olympic_id = o.olympic_id;
```

## Задание 4

В какой стране был наибольший процент игроков (из перечисленных в наборе данных), чьи имена начинались с гласной?

```sql
WITH country_vowels (country_id, vowels) AS (
    SELECT c.country_id, count(*)
    FROM players p
             JOIN countries c ON p.country_id = c.country_id
    WHERE left(p.name, 1) IN ('A', 'E', 'I', 'O', 'U')
    GROUP BY c.country_id
),
     country(country_id, total) AS (
         SELECT c.country_id, count(*)
         FROM players p
                  JOIN countries c ON p.country_id = c.country_id
         GROUP BY c.country_id
     )

SELECT country_vowels.country_id, cast(vowels AS DECIMAL) / total AS share
FROM country_vowels
         JOIN country ON country_vowels.country_id = country.country_id
ORDER BY share DESC
LIMIT 1;
```

## Задание 5

Для Олимпийских игр 2000 года найдите 5 стран с минимальным соотношением количества групповых медалей к численности населения.
