### Задание 6

Я взял датасет по ссылке, так что таблицы те же.

* Для Олимпийских игр 2004 года сгенерируйте список (год рождения, количество игроков, количество золотых медалей),
  содержащий годы, в которые родились игроки, количество игроков, родившихся в каждый из этих лет, которые выиграли по
  крайней мере одну золотую медаль, и количество золотых медалей, завоеванных игроками, родившимися в этом году.

```postgresql
SELECT EXTRACT(year FROM p.birthdate) AS birth_year,
       COUNT(DISTINCT p.player_id)    AS winners_count,
       COUNT(r.event_id)              AS gold_medals_count
FROM results AS r
         JOIN events AS e ON r.event_id = e.event_id
         JOIN olympics AS o ON e.olympic_id = o.olympic_id
         JOIN players AS p ON r.player_id = p.player_id
WHERE o.year = 2004
  AND r.medal = 'GOLD'
GROUP BY EXTRACT(year FROM p.birthdate);
```

* Перечислите все индивидуальные (не групповые) соревнования, в которых была ничья в счете, и два или более игрока
  выиграли золотую медаль.

```postgresql
SELECT e.event_id
FROM events AS e
         JOIN results AS r ON e.event_id = r.event_id
WHERE e.is_team_event = 0
  AND r.medal = 'GOLD'
GROUP BY e.event_id
HAVING COUNT(*) > 1;
```

* Найдите всех игроков, которые выиграли хотя бы одну медаль (GOLD, SILVER и BRONZE) на одной Олимпиаде. (player-name,
  olympic-id).

Если трактовать буквально, то получается подозрительно просто

```postgresql
SELECT DISTINCT p.name,
                e.olympic_id
FROM players AS p
         JOIN results AS r ON p.player_id = r.player_id
         JOIN events AS e ON r.event_id = e.event_id
         JOIN olympics o on e.olympic_id = o.olympic_id;
```

Быть может, имелось в виду найти всех игроков, которые на одной олимпиаде получили по каждой медали? Тогда выходит так

```postgresql
SELECT DISTINCT p.name,
                e.olympic_id
FROM players AS p
         JOIN results AS r ON p.player_id = r.player_id
         JOIN events AS e ON r.event_id = e.event_id
         JOIN olympics o on e.olympic_id = o.olympic_id
GROUP BY p.name, e.olympic_id
HAVING COUNT(DISTINCT r.medal) = 3;
```

* В какой стране был наибольший процент игроков (из перечисленных в наборе данных), чьи имена начинались с гласной?

```postgresql
SELECT DISTINCT c.name,
                AVG((SUBSTR(p.name, 1, 1) IN ('A', 'E', 'I', 'O', 'U', 'Y'))::int)
                OVER (PARTITION BY c.name) * 100 AS percentage
FROM players AS p
         JOIN countries AS c ON p.country_id = c.country_id
ORDER BY percentage DESC
LIMIT 1;
```

Пояснение: предикат `SUBSTR(p.name, 1, 1) IN ('A', 'E', 'I', 'O', 'U', 'Y')` определяет, что имя начинается с гласной.
Каст результата (bool) к целочисленному типу даёт в результате 0 или 1. AVG складывает эти нули и единицы и делит на
общее число значений, так мы получаем процент.

* Для Олимпийских игр 2000 года найдите 5 стран с минимальным соотношением количества групповых медалей к численности
  населения.

```postgresql
SELECT c.name,
       COUNT(r.medal)                         AS medals_count,
       c.population,
       COUNT(r.medal)::decimal / c.population AS medals_per_population
FROM results AS r
         JOIN events AS e on e.event_id = r.event_id
         JOIN olympics AS o on o.olympic_id = e.olympic_id
         JOIN players AS p ON p.player_id = r.player_id
         JOIN countries AS c on c.country_id = p.country_id
WHERE e.is_team_event = 1
  AND o.year = 2000
GROUP BY c.name, c.population
ORDER BY medals_per_population
LIMIT 5;
```
