``` sql
SELECT EXTRACT(year FROM birthdate) birthyear, count(DISTINCT players.player_id) num_players, count(DISTINCT r.event_id) num_golds FROM players INNER JOIN results r on players.player_id = r.player_id INNER JOIN events e on r.event_id = e.event_id INNER JOIN olympics o on o.olympic_id = e.olympic_id WHERE o.year = '2004' AND r.medal = 'GOLD' GROUP BY birthyear;
```

Ничья = у нескольких игроков одинаковый счёт (не обязательно на первом месте).

``` sql
SELECT e.event_id AS event_id, e.name AS name FROM ((SELECT event_id FROM results GROUP BY event_id, result HAVING count(*) > 1) INTERSECT (SELECT event_id FROM results WHERE medal = 'GOLD' GROUP BY event_id HAVING count(*) > 1)) condition LEFT JOIN events e on e.event_id = condition.event_id WHERE is_team_event = 0;
```
