1. Для Олимпийских игр 2004 года сгенерируйте список (год рождения, количество игроков, количество золотых медалей), содержащий годы, в которые родились игроки, количество игроков, родившихся в каждый из этих лет, которые выиграли по крайней мере одну золотую медаль, и количество золотых медалей, завоеванных игроками, родившимися в этом году.
```sql
select b_year1, count_players, count_medals
    from
    
         (
  (select date_part('year', birthdate) as b_year1, count(*) as count_medals
  from
    (select birthdate, medal, r.player_id
    from (players
    join results r on players.player_id = r.player_id
    join events e on r.event_id = e.event_id
    join olympics o on e.olympic_id = o.olympic_id)
    where year = 2004) as t1
  group by date_part('year', birthdate), medal
  having medal = 'GOLD') as t_medal

join

  (select date_part('year', birthdate) as b_year2, count(*) as count_players
  from
    (select distinct birthdate, r.player_id
    from (players
    join results r on players.player_id = r.player_id
    join events e on r.event_id = e.event_id
    join olympics o on e.olympic_id = o.olympic_id)
    where year = 2004 and medal = 'GOLD') as t2
    group by date_part('year', birthdate)) as t_pl

on b_year1 = b_year2 )
as t
```

Так как запрос получился большой, то объясню, как я делала:
1) верхний селект - год рождения и число золотых медалей спортсменов этого года рождения на играх в 2004 году. 
Тут группирую по медалям, чтобы посчитать именно медали, учитываются все спортсмены (и если они заработали 2 и больше медалей, то учитываются несколько раз)
2) нижний селект - год рождения и количество спортменов этого года рождения, у которых хотя бы 1 медаль.
Тут я беру distinct, чтобы повторяющиеся строки (если более 1 золотой медали) не учитывались.
3) делаю джойн, чтобы объединить в одну таблицу (по году рождения)

Этот запрос работает, прикладываю скрин его работы (то есть медалей за определенный год рождения больше. чем спортсменов, потому что спортсмен может заработать 2 и более медалей)
![table](https://github.com/kseniashilova/BD_B2019_PING_6/blob/main/Practice%206/Шилова%20Ксения/1.PNG)



2. Перечислите все индивидуальные (не групповые) соревнования, в которых была ничья в счете, и два или более игрока выиграли золотую медаль.
```sql
select e.name
from events e join results r on e.event_id = r.event_id
where is_team_event = 0
group by e.name, r.result, r.medal
having count(*) > 1 and medal = 'GOLD'
```

Здесь сначала берем все соревнования, которые не являются командными, затем группируем по медалям и резлультатам, и получаем, что если медаль золотая, то элементов в группе должно быть больше одного, так как в этом случае ничья


3. Найдите всех игроков, которые выиграли хотя бы одну медаль (GOLD, SILVER и BRONZE) на одной Олимпиаде. (player-name, olympic-id).
```sql
select p.name, o.olympic_id
from players p
join results r on p.player_id = r.player_id
join events e on r.event_id = e.event_id
join olympics o on e.olympic_id = o.olympic_id
```

4. В какой стране был наибольший процент игроков (из перечисленных в наборе данных), чьи имена начинались с гласной?
```sql
select t_vowel.country_id, 1.0 * count_vowel/count_country as percentage
    from
(
(select c.country_id, count(*) as count_vowel
from players p
join countries c on p.country_id = c.country_id
where   SUBSTRING(p.name,1,1) IN ('A','E','I','O','U')
group by c.country_id) as t_vowel

join

(select c.country_id, count(*) as count_country
from players p
join countries c on p.country_id = c.country_id
group by c.country_id) as t_country

on t_vowel.country_id = t_country.country_id)

order by percentage desc
limit 1

```

То есть, сначала берем и находим количество игроков, у которых имя начинается с гласной (t_vowel), затем, находим общее количество игроков по всем странам, затем делим первое количество на второе и получается долю игроков, у которых имя начинается с гласной. 
Чтобы взять наибольшую долю вместе с id страны, нужно выстроить строки в порядке убывания процента и взять первую.


5. Для Олимпийских игр 2000 года найдите 5 стран с минимальным соотношением количества групповых медалей к численности населения.
```sql
select t1.country_id, 1.0 * t1.count / t2.population as percentage
from
(select p.country_id as country_id, count(*) as count
from results r
join events e on r.event_id = e.event_id
join olympics o on e.olympic_id = o.olympic_id
join players p on r.player_id = p.player_id
where o.year = 2000 and e.is_team_event = 1
group by p.country_id) as t1

join

(select c.country_id as country_id, c.population as population
from countries c) as t2

on t1.country_id = t2.country_id

order by percentage
limit 5
```

Все аналогично предыдущей задаче. t1 - таблица с количеством групповых медалей за олимпиаду 2000 года, по всем странам, t2 - таблица с количеством населения для каждой страны, затем берем и находим отношение этих количеств, сортируем и берем 5 минимальных.
