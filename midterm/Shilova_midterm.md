1.
``` sql


create table apartment(
  id number primary key,
  owner_id number,
  number_of_rooms number,
  area number, 
  floor number,
  number_of_bathrooms number,
  country varchar(256),
  city varchar(256),
  street varchar(256),
  house_number number,
  apartment_number number,
  cost_of_day number
);
```

``` sql
create table owner(
  id number primary key,
  name varchar(256)
);
```

``` sql
create table rent(
  host_id number,
  apartment_id number
  start_date date,
  end_date date
  approve_date date,
  reject_date date,
  request_date date,
  cancel_date date, 
  
  foreign key(host_id)
        references host(id)
        on delete cascade,
    
    foreign key(apartment_id)
        references apartment(id)
        on delete cascade,

    primary key(host_id, apartment_id, start_date)
);
```

Task 3.

``` sql
select a.id, a.street, a.house_number, a.apartment_number
from apartment a
join rent r on a.id == r.apartment_id
where city == 'Moscow' and approve_date != NULL and approve_date.year() = now().year()
group by a.id
having count(*) > 5
```

Task 4.

``` sql
select a.id, a.street, a.house_number, a.apartment_number
from apartment a
join rent r on a.id == r.apartment_id
where city == 'Moscow' and approve_date != NULL and approve_date.year() = now().year() 
  and host_id not in (
      select host_id
      from rent
      where cancel_date == NULL
      ) as exept_host_id
group by a.id
having count(*) > 5

```

Task 5.

Тут напишу 2 варианта. Первый - отказывается гость
``` sql
select a.country, a.number_of_rooms, count(*)
from apartment a
join rent r on a.id == r.apartment_id
where r.cancel_date != NULL
group by a.country, a.number_of_rooms
```
Второй - отказывает владелец
``` sql
select a.country, a.number_of_rooms, count(*)
from apartment a
join rent r on a.id == r.apartment_id
where r.reject_date != NULL
group by a.country, a.number_of_rooms
```

Task 6.
``` sql
select a.country, count(*), sum(a.cost_of_day * (r.end_date.day() - r.start_date.day()))
from apartment a
join rent r on a.id == r.apartment_id
where r.reject_date != NULL and (now().day() - r.reject_date.day() < 30)
group by a.country
```


Task 7.
``` sql
select o1.id, o1.name
from owner o1
join  apartment a1 on a1.owner_id == o1.id
join rent r1 on a1.id == r1.apartment_id
where a1.cost_of_day * (r1.end_date.day() - r1.start_date.day()) in
  (
    select  sum(a.cost_of_day * (r.end_date.day() - r.start_date.day())) as sum
    from apartment a
    join rent r on a.id == r.apartment_id
    where r.approve_date != NULL and r.cancel_date == NULL and (now().year() == r.approve_date.year())
    order by sum desc
    limit 3
  )
```

Task 8.
``` sql
select host1.host_id, host2.host_id
from 
(select host_id, id, start_date, end_date
from  apartment a1
join rent r1 on a1.id == r1.apartment_id
where r1.cancel_date == NULL and r1.reject_date == NULL) as host1
union
(select host_id, id, start_date, end_date
from  apartment a2
join rent r2 on a2.id == r2.apartment_id
where r2.cancel_date == NULL and r2.reject_date == NULL
  and r2.start_date.year() == host1.start_date.year() and abs((r2.end_date.day() - r2.start_date.day()) - (host1.end_date.day() - host1.start_date.day()) <= 3) as host2
)
```
