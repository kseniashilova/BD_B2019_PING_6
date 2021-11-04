## Задание 
Данные:  
Teams{country, team_id, population} -- сборные стран;  
Athletes{name, athlete_id, country, birthday, gender} -- спортсмены; 
Events{event_id, name, place, olympiad_id, type, time} -- соревнования;  
Achievements{event_id, athlete_id, medal} -- достижения спортсменов.   

1. Для Олимпийских игр 2004 года сгенерируйте список (год рождения, количество игроков, количество золотых медалей), содержащий годы, в которые родились игроки, количество игроков, родившихся в каждый из этих лет, которые выиграли по крайней мере одну золотую медаль, и количество золотых медалей, завоеванных игроками, родившимися в этом году.  
```
SELECT EXTRACT(YEAR FROM a.birthday) AS year,
	COUNT(*) AS athletes_number,
	SUM(b.gold) as number_gold
FROM Athletes a
	INNER JOIN  (SELECT athlete_id, COUNT(*) AS gold
				FROM Achievements
				WHERE medal = 'GOLD'
				GROUP BY athlete_id) b
	ON a.athlete_id = b.athlete_id
GROUP BY EXTRACT(YEAR FROM a.birthday)
```

2. Перечислите все индивидуальные (не групповые) соревнования, в которых была ничья в счете, и два или более игрока выиграли золотую медаль.  
```
SELECT events.name
FROM Events events 
	 RIGHT OUTER JOIN (SELECT event_id,
						COUNT(*) as number_medals
					FROM Achievements
					WHERE medal = 'GOLD'
					GROUP BY event_id
					HAVING COUNT(*) > 1) tie
	ON events.event_id = tie.event_id
WHERE type = 0
```
3. Найдите всех игроков, которые выиграли хотя бы одну медаль (GOLD, SILVER и BRONZE) на одной Олимпиаде. (player-name, olympic-id).  
```
SELECT name, olympiad_id
FROM Athletes t1 INNER JOIN
(SELECT athlete_id,
		b.olympiad_id,
		COUNT(medal) AS number_medals
FROM Achievements a INNER JOIN 
				(SELECT event_id, olympiad_id
				 FROM Events) b ON a.event_id = b.event_id
GROUP BY athlete_id, b.olympiad_id
HAVING COUNT(medal) >= 1) t2
ON t1.athlete_id = t2.athlete_id
```
4.  В какой стране был наибольший процент игроков (из перечисленных в наборе данных), чьи имена начинались с гласной?  
```
SELECT teams.country
FROM
	((SELECT country, 
			COUNT(*) AS spec_names
	FROM Athletes
	WHERE name SIMILAR TO '(A|E|I|O|U|Y)%'
	GROUP BY country) t1
	INNER JOIN
	(SELECT country,
			COUNT(*) AS number_athletes
	FROM Athletes
	GROUP BY country) t2
	ON t1.country = t2.country) LEFT OUTER JOIN Teams teams
		ON teams.team_id = t1.country
WHERE CAST(t1.spec_names AS REAL)/ CAST(t2.number_athletes AS REAL) = 
		(SELECT MAX(CAST(t1.spec_names AS REAL) / CAST(t2.number_athletes AS REAL)) 
                 FROM
	              (SELECT country, 
			COUNT(*) AS spec_names
	                FROM Athletes
	                WHERE name SIMILAR TO '(A|E|I|O|U|Y)%'
	                GROUP BY country) t1
	              INNER JOIN
	              (SELECT country,
			COUNT(*) AS number_athletes
	               FROM Athletes
	               GROUP BY country) t2
	               ON t1.country = t2.country)
```
5. Для Олимпийских игр 2000 года найдите 5 стран с минимальным соотношением количества групповых медалей к численности населения.  
```
SELECT teams.country
FROM
	(SELECT t2.country,
	 		COUNT(*) AS number_medals
FROM
	(Achievements ach
		LEFT JOIN (SELECT event_id, olympiad_id
				   FROM Events
				   WHERE type = 1 AND olympiad_id = (
				   		SELECT olymp.olympiad_id
				   		FROM Olympiads olymp
				   		WHERE year = 2000)) ev
		ON ach.event_id = ev.event_id) t1
	 	RIGHT JOIN (SELECT athlete_id, country
				   FROM Athletes) t2
		ON t1.athlete_id = t2.athlete_id
GROUP BY t2.country) t3
	 INNER JOIN Teams teams
	 ON t3.country = teams.team_id
ORDER BY CAST(t3.number_medals AS REAL) / CAST( teams.population AS REAL) ASC
FETCH FIRST 5 ROWS ONLY
```
