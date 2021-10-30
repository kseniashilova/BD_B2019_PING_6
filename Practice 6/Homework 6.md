## Задание 5

### Запрос 1

<b> Для Олимпийских игр 2004 года сгенерируйте список (год рождения, количество игроков, количество золотых медалей), содержащий годы, в которые родились игроки, количество игроков, родившихся в каждый из этих лет, которые выиграли по крайней мере одну золотую медаль, и количество золотых медалей, завоеванных игроками, родившимися в этом году. </b>

```sql
SELECT EXTRACT(YEAR FROM Players.birthdate), COUNT(DISTINCT Players.player_id), COUNT(Results.medal)
FROM players
    JOIN Results r ON Players.player_id = r.player_id
    JOIN Events e ON Results.event_id = e.event_id
    JOIN Olympics o ON Events.olympic_id = o.olympic_id
WHERE Results.medal = 'GOLD' AND Olympics.year = 2004
GROUP BY EXTRACT(YEAR FROM Players.birthdate)
```

### Запрос 2

<b> Перечислите все индивидуальные (не групповые) соревнования, в которых была ничья в счете, и два или более игрока выиграли золотую медаль. </b>

```sql
SELECT Events.event_id, Events.name 
  FROM Events
JOIN Results
  ON Events.event_id = Results.event_id
WHERE Events.is_team_event = 0 
  AND Results.medal = 'GOLD'
GROUP BY Events.event_id, Events.name 
  HAVING COUNT(*) > 1;
```

### Запрос 3

<b> Найдите всех игроков, которые выиграли хотя бы одну медаль (GOLD, SILVER и BRONZE) на одной Олимпиаде. (player-name, olympic-id). </b>

```sql
SELECT Players.name, Players.olympic_id
FROM Players
  JOIN Results ON Players.player_id = Results.player_id
  JOIN Events ON Results.event_id = Events.event_id
  JOIN Olympics ON Events.olympic_id = Olympics.olympic_id;
```

### Запрос 4

<b> В какой стране был наибольший процент игроков (из перечисленных в наборе данных), чьи имена начинались с гласной? </b>

```sql
SELECT leading_vowels.country_id, cast(vowels as decimal)/total FROM (
  SELECT Players.country_id, COUNT(*) as vowels FROM Players
  WHERE LEFT(Players.name, 1) IN ('A', 'E', 'I', 'O', 'U')
  GROUP BY Players.country_id
) AS leading_vowels
JOIN (
  SELECT Players.country_id, count(*) AS total FROM Players
  GROUP BY Players.country_id
) AS countries ON leading_vowels.country_id = countries.country_id
ORDER BY 2 DESC LIMIT 1;
```

### Запрос 5

<b> Для Олимпийских игр 2000 года найдите 5 стран с минимальным соотношением количества групповых медалей к численности населения. </b>

```sql
SELECT country.country_id
FROM Olympics
  JOIN Events 
    ON Events.olympic_id = Olympics.olympic_id
  JOIN Countries 
    ON Сountries.country_id = Players.country_id
  JOIN Players 
    ON Players.player_id = Results.player_id
  JOIN Results 
    ON Events.event_id = Results.event_id
WHERE year = 2000
  AND is_team_event = 1
GROUP BY country.country_id, country.population
ORDER BY CAST(COUNT(Results.medal) AS decimal) / country.population
LIMIT 5;
```