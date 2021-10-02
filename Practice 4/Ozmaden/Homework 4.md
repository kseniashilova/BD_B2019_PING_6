## Задание 4

### Задача 1

<b> а) Какие фамилии читателей в Москве? </b>

```sql
SELECT LastName 
FROM Reader 
WHERE Address 
LIKE %Moscow%;
```

<b> б) Какие книги (author, title) брал Иван Иванов? </b>

```sql
SELECT Book.Author, 
        Book.Title 
FROM Book 
        JOIN Reader ON Borrowing.ReaderNr = Reader.ID 
WHERE Reader.LastName = 'Ivanov' AND Reader.FirstName = 'IVAN'
```

<b> в) Какие книги (ISBN) из категории "Горы" не относятся к категории "Путешествия"? Подкатегории не обязательно принимать во внимание! </b>

```sql
SELECT DISTINCT Book.ISBN
FROM BookCategory
WHERE BookCategory.CategoryName = 'Mountains'
EXCEPT SELECT DISTINCT Book.ISBN 
FROM BookCategory
WHERE BookCategory.CategoryName = 'Travel';
```

<b> г) Какие читатели (LastName, FirstName) вернули копию книги? </b>

```sql
SELECT DISTINCT Reader.FirstName, 
    Reader.LastName 
FROM Reader 
        JOIN Borrowing ON Reader.ID = Borrowing.ReaderNr
WHERE Borrowing.ReturnDate IS NOT NULL;
```

<b> д) Какие читатели (LastName, FirstName) брали хотя бы одну книгу (не копию), которую брал также Иван Иванов (не включайте Ивана Иванова в результат)? </b>

```sql
FROM Reader 
        JOIN Borrowing ON Reader.ID == Borrowing.ReaderNr
        WHERE ISBN IN (
            SELECT ISBN 
            FROM Borrowing 
                JOIN Reader ON Reader.ID = Borrowing.ReaderNr 
            WHERE LastName = 'Ivanov' AND FirstName = 'Ivan') 
        EXCEPT SELECT Reader.LastName, Reader.FirstName 

FROM Borrowing 
        JOIN Reader ON Reader.ID == Borrowing.ReaderNr 
WHERE LastName = 'Ivanov' AND FirstName = 'Ivan';
```

### Задача 2

<b> а) Найдите все прямые рейсы из Москвы в Тверь. <b>

```sql
SELECT Tr.TrainNr 
FROM Train AS Tr 
        JOIN Connection AS C ON Tr.TrainNr = C.TrainNr
WHERE C.FromStation = 'Москва' AND C.ToStation = 'Тверь'
```

<b> б) Найдите все многосегментные маршруты, имеющие точно однодневный трансфер из Москвы в Санкт-Петербург. <b>

```sql
SELECT C1.Departure,
       C2.Arrival, 
       C1.TrainNr
FROM Connection AS C1
         JOIN Connection AS C2 ON C1.TrainNr = C2.TrainNr
WHERE DAY (C1.Departure) = DAY (C2.Arrival) 
    AND (С2.ToStation = 'Санкт-Петербург' AND C1.FromStation = 'Москва');
```

<b> в) Что изменится в выражениях для а) и б), если отношение "Connection" не содержит дополнительных кортежей для транзитивного замыкания, поэтому многосегментный маршрут Москва-> Тверь-> Санкт-Петербург содержит только кортежи Москва-> Тверь и Тверь-Санкт-Петербург? <b>

В а) достаточно найти совпадающие номера и конечную станцию, так что в выражении вроде бы ничего менять не надо.

В б) тоже не вижу причин для изменения.

### Задача 3

<b> Представьте внешнее объединение (outer join) в виде выражения реляционной алгебры с использованием только базовых операций (select, project, cartesian, rename, union, minus) <b>

`L(A1, ..., An)`

`R(B1, ..., Bm)`

Outer Join = Inner Join + значения L вне Inner Join + значения R вне Inner Join. 

Тогда:

- Найдём строки из L, для которых нет соответствия из R, и ставим null вместо значений R.

- Найдём строки из R, для которых нет соответствия из L, и ставим null вместо значений L.

```
outer_join(L, R) = 
    union(
        inner_join(L, R),

        project[A1, ..., An, NULL, ..., NULL](L - project[A1, ..., An](inner_join(L, R))),

        project[NULL, ..., NULL, B1, ..., Bm](R - project[B1, ..., Bm](inner_join(L, R)))
    )
```
