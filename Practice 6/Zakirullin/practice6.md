``` sql
SELECT EXTRACT(year FROM birthdate) birthyear, count(DISTINCT players.player_id) num_players, count(DISTINCT r.event_id) num_golds FROM players INNER JOIN results r on players.player_id = r.player_id INNER JOIN events e on r.event_id = e.event_id INNER JOIN olympics o on o.olympic_id = e.olympic_id WHERE o.year = '2004' AND r.medal = 'GOLD' GROUP BY birthyear;
```

Ничья = у нескольких игроков одинаковый счёт (не обязательно на первом месте).

``` sql
SELECT e.event_id AS event_id, e.name AS name FROM ((SELECT event_id FROM results GROUP BY event_id, result HAVING count(*) > 1) INTERSECT (SELECT event_id FROM results WHERE medal = 'GOLD' GROUP BY event_id HAVING count(*) > 1)) condition LEFT JOIN events e on e.event_id = condition.event_id WHERE is_team_event = 0;
```

``` sql
SELECT DISTINCT players.name palyer_name, olympic_id FROM players INNER JOIN results r on players.player_id = r.player_id INNER JOIN events e on e.event_id = r.event_id;
```

``` sql
SELECT c.name country_name FROM players INNER JOIN countries c on c.country_id = players.country_id GROUP BY c.country_id, c.name ORDER BY sum(CASE WHEN players.name SIMILAR TO '[AEIOU]%' THEN 1 ELSE 0 END)::float / count(*) DESC LIMIT 1;
```

``` sql
SELECT c.country_id country_id, name FROM (
	SELECT country_id, count(medal) medals FROM (
		SELECT e.event_id event_id, medal, country_id from results r INNER JOIN events e on e.event_id = r.event_id INNER JOIN players p on p.player_id = r.player_id WHERE is_team_event = 1 AND olympic_id = 'SYD2000' GROUP BY e.event_id, medal, country_id
	) cr /* country results */ GROUP BY country_id
) cm /* country medals */ INNER JOIN countries c on cm.country_id = c.country_id ORDER BY medals::float / population LIMIT 5;
```
