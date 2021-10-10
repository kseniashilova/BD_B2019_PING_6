# Домашнее задание 4 Сахаров Никита БПИ196

## Задача 1

+ Reader: ({<ins> ID</ins>, LastName, FirstName, Address, BirthDate}) 
+ Book: ({ <ins>ISBN</ins>, Title, Author, PagesNum, PubYear, PubName})
+ Publisher: ({ <ins>PubName</ins>, PubAdress})
+ Category: ({ <ins>CategoryName</ins>, ParentCat})
+ Copy: ({<ins>ISBN, CopyNumber</ins>, ShelfPosition})
+ Borrowing: ({<ins>ReaderNr</ins>, <ins>ISBN</ins>, </ins>CopyNumber</ins>, ReturnDate})
+ BookCat: ({<ins>ISBN</ins>, <ins>CategoryName</ins>})

Пункты: 
+ a)  Какие фамилии читателей в Москве?
    ```sql
    SELECT Reader.Surname
    FROM Reader
    WHERE Reader.Address = 'Москва';
    ```
+ б) Какие книги (author, title) брал Иван Иванов?
    ```sql 
    SELECT DISTINCT  Book.Author, Book.Title
    FROM Borrowing
        JOIN Book ON Borrowing.ISBN = Book.ISBN
        JOIN Reader ON Borrowing.ReaderNr = Reader.ID
    WHERE Reader.FirstName = 'Иван' AND Reader.LasrName = 'Иванов';
    ```
+ в) Какие книги (ISBN) из категории "Горы" не относятся к категории "Путешествия"?
    ```sql
    SELECT BookCat.ISBN
    FROM BookCat
        WHERE BookCat.CategoryName = 'Горы'
    EXCEPT
    SELECT BookCat.ISBN
    FROM BookCat
        WHERE BookCat.CategoryName = 'Путешествия';
    ```
+  г) Какие читатели (LastName, FirstName) вернули копию книгу?
    ```sql
    SELECT Reader.LastName, Reader.FirstName
    FROM Reader
        JOIN Borrowing ON Reader.ID = Borrowing.ReaderNr
    WHERE Borrowing.ReturnDate IS NOT NULL;
    ```
+ д) Какие читатели (LastName, FirstName) брали хотя бы одну книгу (не копию), которую брал также Иван Иванов (не включайте Ивана Иванова в результат)?
    ```sql
    SELECT DISTINCT Reader.LastName, Reader.FirstName
    FROM Borrowing
        JOIN Reader ON Borrowing.ReaderNr = Reader.ID
    WHERE NOT (Reader.FirstName = 'Иван') AND NOT (Reader.LastName = 'Иванов') AND
        Borrowing.ISBN in
        (SELECT Borrowing.ISBN 
            FROM Borrowing
                JOIN Reader ON Borrowing.ReaderNr = Reader.ID
            WHERE Reader.FirstName = 'Иван' AND Reader.LastName = 'Иванов')
    ```
## Задача 2
+ City: ( {<ins>Name</ins>, Region} )
+ Station: ( {<ins>Name</ins>, #Tracks, CityName, Region} )
+ Train: ( {<ins>TrainNr</ins>, Length, StartStationName, EndStationName} ) 
+ Сonnected:  ( {FromStation, ToStation, TrainNr, Departure, Arrival})

Пункты: 
+ а) Найдите все прямые рейсы из Москвы в Тверь.
    Хотим найти все рейсы что начинаются в Мск и заканчиваются в твери. Но т.к. мы хотим прямые рейсы значит хотим выкинуть все поезда которые стартуют в мск и идут не в Тверь.
    ```sql
    SELECT DISTINCT Сonnected.TrainNr
    FROM Connected
        JOIN Station stFrom on Сonnected.FromStation = stFrom.Name
        JOIN Station stTo on Сonnected.ToStation = stTo.Name
    WHERE stFrom.CityName = 'Москва' AND stTo.CityNam = 'Тверь' ;
    EXCEPT
    SELECT BookCat.ISBN
    FROM Connected
        JOIN Station stFrom on Сonnected.FromStation = stFrom.Name
        JOIN Station stTo on Сonnected.ToStation = stTo.Name
    WHERE stFrom.CityName = 'Москва' AND NOT stTo.CityNam = 'Тверь' ;
    ```
+ б) Найдите все многосегментные маршруты, имеющие точно однодневный трансфер из Москвы в Санкт-Петербург.
    ```sql 
    SELECT C1.TrainNr, C1.Departure, C2.Arrival
    FROM Сonnected AS ConFrom
        JOIN Сonnected AS ConTo ON ConFrom.TrainNr = ConTo.TrainNr
        JOIN Station stFrom on ConFrom.FromStation = stFrom.Name
        JOIN Station stTo on ConTo.ToStation = stTo.Name
    WHERE DAY (ConFrom.Departure) = DAY (ConTo.Arrival) 
        AND stFrom.Name = 'Москва'
        AND stTo.Name = 'Санкт-Петербург';
    ```
+ в) Что изменится в выражениях для а) и б), если отношение "Connection" не содержит дополнительных кортежей для транзитивного замыкания, поэтому многосегментный маршрут Москва-> Тверь-> Санкт-Петербург содержит только кортежи Москва-> Тверь и Тверь-Санкт-Петербург?<br>
Ничего не должно поменяться потому что я написал запросы сразу под оба варианта.

## Задача 3

Представьте внешнее объединение (outer join) в виде выражения реляционной алгебры с использованием только базовых операций (select, project, cartesian, rename, union, minus)

Определим для начала обычный  inner join, который склеивает наши таблички.

```
Inner Join(A, B, Condition) = project[A1, ..., An, B1, ..., Bm](select(cartesian(A, B), Condition))
```
Теперь его надо заполнить записями, которые в него не вошли, но нужны в outer join. Для этого 
Посмотрим как привести такую же табличку где есть записи из A дополеннную null.
Для этого вычтем из A наш inner join и спроцеируем на табличку где на нужных местах NULL

project[A1, ..., An, NULL, ..., NULL](A - project[A1, ..., An](Inner Join(A, B, Condition)))


В итооге получаем такое варажение: 
```
Outer Join(A, B, Condition) = 
    union(
        Inner Join(A, B, Condition),
        project[A1, ..., An, NULL, ..., NULL](A - project[A1, ..., An](Inner Join(A, B, Condition))),
        project[NULL, ..., NULL, B1, ..., Bm](B - project[B1, ..., Bm](Inner Join(A, B, Condition)))
    )
```