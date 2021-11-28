### Задача 1
* а) Пусть `k` - количество записей, у которых `salary = 200`. Тогда `t_idx(k) = t_rnd * k = 28 * k`.
* б) Время на полное сканирование: `t_scan = t_seq * N = 0.28 * 10^8 = 28 * 10^6`. Тогда можно найти такое `k'`, что `t_idx(k') < t_scan`: `28 * k' < 28 * 10^6 => k' < 10^6`

Ответ: 
* а) `28 * k`, где `k` = кол-во записей с `salary = 200`;
* б) `k' < 10^6`

### Задача 2

--

### Задача 3
```sql
SELECT sum(o.Volume)FROM Customer c, Order o
WHERE c.Cid = o.Customer AND c.Name = “Alex”; 
```
* а) 
```
sum(
  project[Order.Volume](
    select[Customer.Name = 'Alex'](
      join[Customer.Cid = Order.Customer](Customer, Order)
    )
  )
)
```
