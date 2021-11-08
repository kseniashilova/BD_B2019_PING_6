# Task 1
```sql
SELECT Extract(YEAR from Players.birthdate) AS birth_year, 
    COUNT(DISTINCT Players.player_id) AS players_count, 
    COUNT(Results.player_id) AS medals_count
FROM Results
    JOIN Players ON Results.player_id = Players.player_id
    JOIN Events ON Results.event_id = Events.event_id
    JOIN Olympics ON Events.olympic_id = Olympics.olympic_id
WHERE Results.medal = 'GOLD' AND Olympics.year = '2004'
GROUP BY Extract(YEAR from Players.birthdate);
```
# Task 2
```sql
SELECT 
    Events.name
FROM
    Events
WHERE
    Events.event_id IN 
        (
        SELECT Events.event_id
        FROM Results JOIN Events ON Events.event_id = Results.event_id
        WHERE Events.is_team_event = 0 AND Results.medal = 'GOLD'
        GROUP BY Events.event_id HAVING COUNT(Results.player_id) > 1
        );
```

# Task 3
```sql
SELECT Players.name, Olympics.olympic_id
FROM Olympics
JOIN Events ON Olympics.olympic_id = Events.olympic_id
JOIN Results ON Events.event_id = Results.event_id
JOIN Players ON Results.player_id = Players.player_id;
```
# Task 4
```sql
SELECT Countries.name
FROM Countries
WHERE Countries.country_id IN (
SELECT CountryIds.country_id
FROM(
    SELECT VowelsPlayers.country_id, 
    CAST(VowelsPlayers.players_count AS DECIMAL)/AllPlayers.players_count
        AS vowels_percent FROM(
                            SELECT Players.country_id, COUNT(*) players_count
                            FROM Players
                            WHERE LEFT(Players.name, 1) IN ('A', 'E', 'I', 'O', 'U', 'Y')
                            GROUP BY Players.country_id
                            ) AS VowelsPlayers
        JOIN (
            SELECT Players.country_id, COUNT(*) AS players_count 
            FROM Players
            GROUP BY Players.country_id
            ) AS AllPlayers ON VowelsPlayers.country_id = AllPlayers.country_id
        ORDER BY vowels_percent DESC 
        LIMIT 1
    ) AS CountryIds
);
```
# Task 5
```sql
SELECT NewCountries.name
FROM (
SELECT Countries.country_id, Countries.name
FROM Olympics
JOIN Events ON Events.olympic_id = Olympics.olympic_id
JOIN Results ON Events.event_id = Results.event_id
JOIN Players ON Players.player_id = Results.player_id
JOIN Countries ON Countries.country_id = Players.country_id
WHERE Olympics.year = 2000 AND Events.is_team_event = 1
GROUP BY Countries.country_id, Countries.name, Countries.population
ORDER BY CAST(COUNT(Results.medal) AS decimal) / Countries.population
LIMIT 5) AS NewCountries;
```