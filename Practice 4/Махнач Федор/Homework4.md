## Задание 4

### Задача 1

Reader( Id, LastName, FirstName, Address, BirthDate ) <br>
Book ( ISBN, Title, Author, PagesNum, Year, PublisherName )  <br>
Publisher ( Name, Address )  <br>
Category ( Name, ParentCategoryName )  <br>
Copy ( ISBN, CopyNumber, ShelfPosition )  <br>
Borrowing ( ReaderId, ISBN, CopyNumber, ReturnDate )  <br>
BookCategory ( ISBN, CategoryName ) <br>

<b> а) Какие фамилии читателей в Москве? </b>

```sql
SELECT LastName
FROM Reader
WHERE 'Москва' IN Address;
```

<b> б) Какие книги (author, title) брал Иван Иванов? </b>

```sql
SELECT Book.Author,
       Book.Title
FROM Borrowing
         JOIN Book ON Borrowing.ISBN = Book.ISBN
         JOIN Reader ON Borrowing.ReaderNr = Reader.Id
WHERE Reader.FirstName = 'Иван'
  AND Reader.LastName = 'Иванов';
```

P.S. Данный запрос вернёт несколько одинаковых записей в случае, если Иван Иванов брал одну и ту же книгу несколько раз.
Для того, чтобы всегда получать уникальные значения следует добавить `DISTINCT`.

<b> в) Какие книги (ISBN) из категории "Горы" не относятся к категории "Путешествия"? Подкатегории не обязательно принимать во внимание! </b>

```sql
SELECT ISBN
FROM BookCategory
WHERE CategoryName = 'Горы'
    EXCEPT
SELECT ISBN
FROM BookCategory
WHERE CategoryName = 'Путешествия';
```

<b> г) Какие читатели (LastName, FirstName) вернули копию книги? </b>

```sql
SELECT DISTINCT Reader.LastName,
                Reader.FirstName
FROM Borrowing
         JOIN Reader ON Reader.Id = Borrowing.ReaderId;
```

<b> д) Какие читатели (LastName, FirstName) брали хотя бы одну книгу (не копию), которую брал также Иван Иванов (не включайте Ивана Иванова в результат)? </b>

```sql
WITH IvanIvanovBooks (ISBN)
         AS
         (
             SELECT DISTINCT Borrowing.ISBN
             FROM Borrowing
                      JOIN Reader ON Reader.Id = Borrowing.ReaderId
             WHERE Reader.FirstName = 'Иван'
               AND Reader.LastName = 'Иванов'
         )
SELECT DISTINCT Reader.LastName,
                Reader.FirstName
FROM Borrowing
         JOIN Reader ON Reader.Id = Borrowing.ReaderId
         JOIN IvanIvanovBooks AS IIB ON IIB.ISBN = Borrowing.ISBN
WHERE NOT (Reader.FirstName = 'Иван' AND Reader.LastName = 'Иванов');
```

### Задача 2

City ( Name, Region ) <br>
Station ( Name, #Tracks, CityName, Region ) <br>
Train ( TrainNr, Length, StartStationName, EndStationName ) <br>
Connection ( FromStation, ToStation, TrainNr, Departure, Arrival) <br>

_Я сначала на SQL писал, потому что я не могу сразу думать реляционной алгеброй._

__а) Найдите все прямые рейсы из Москвы в Тверь.__

Так как существует транзитивное замыкание, считаем, что коннекшн прямой только в том случае, если нет других коннекшенов для этого начального города для этого поезда.
```sql
SELECT C.TrainNr,
       C.Departure,
       C.Arrival
FROM Train AS T
         JOIN Connection AS C ON Connection.TrainNr = TrainNr
WHERE C.FromStation = 'Москва'
  AND C.ToStation = 'Тверь'
  AND NOT EXISTS(
        -- Проверяем, есть ли коннекшн из Москвы НЕ в Тверь для этого поезда.
        -- В этом случае считаем, что рейс не прямой.
        SELECT 1
        FROM Connection C2
        WHERE C2.TrainNr = C.TrainNr
          AND C2.FromStation = 'Москва'
          AND C2.ToStation <> 'Тверь'
    ) 
```

__б) Найдите все многосегментные маршруты, имеющие точно однодневный трансфер из Москвы в Санкт-Петербург (первое
отправление и прибытие в конечную точку должны быть в одну и ту же дату). Вы можете применить функцию DAY () к атрибутам
Departure и Arrival, чтобы определить дату.__

В SQL я выразил это так:

```sql
SELECT C1.TrainNr,
       C1.Departure,
       C2.Arrival
FROM Connection AS C1
         JOIN Connection AS C2 ON C1.TrainNr = C2.TrainNr
WHERE C1.FromStation = 'Москва'
          AND C2.FromStation <> 'Москва'
          AND С2.ToStation = 'Санкт-Петербург'
          AND DAY (C1.Departure) = DAY (C2.Arrival)
```

В реляционной алгебре у меня получилось так

```
project[C1.TrainNr, C1.Departure, C2.Arrival](
    select[
        C1.FromStation = 'Москва'
        AND С2.FromStation <> 'Москва'
        AND C2.ToStation = 'Санкт-Петербург'
        AND DAY (C1.Departure) = DAY (C2.Arrival)   
    ]
    (
        join[C1.TrainNr = C2.TrainNr](
            rename(Connection, C1),
            rename(Connection, C2)
        )
    )
)
```

__в) Что изменится в выражениях для а) и б), если отношение "Connection" не содержит дополнительных кортежей для
транзитивного замыкания, поэтому многосегментный маршрут Москва-> Тверь-> Санкт-Петербург содержит только кортежи
Москва-> Тверь и Тверь-Санкт-Петербург?__

Пункт а) можно будет записать просто:

`select[FromStation = 'Москва' AND ToStation = 'Тверь'] (Connection)`

Пункт б) можно не менять.

### Задача 3

Представьте внешнее объединение (outer join ) в виде выражения реляционной алгебры с использованием только базовых
операций (select, project, cartesian, rename, union, minus)

<ins> Решение </ins>

Пусть

`R(A1, ..., An)`

`S(B1, ..., Bm)`

Определю для удобства обычный inner join:

`join[condition](R, S) = project[A1, ..., An, B1, ..., Bm](select[condition](cartesian(R, S)))`

Тогда

```
outer_join[condition](R, S) = 
    union(
        join[condition](R, S),
        project[A1, ..., An, NULL, ..., NULL](R - project[A1, ..., An](join[condition](R, S)))),
        project[NULL, ..., NULL, B1, ..., Bm](S - project[B1, ..., Bm](join[condition](R, S))))
        )
```

Смысл: внутренний джойн + значения первого отношения, не попавшие во внутренний джойн + значения второго отношения, не
попавшие во внутренний джойн. 
