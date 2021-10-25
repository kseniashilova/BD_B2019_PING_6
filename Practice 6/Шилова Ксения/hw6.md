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
