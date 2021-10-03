# Домашнее задание 4
## Задача 1
Reader( ID, LastName, FirstName, Address, BirthDate) </br>
Book ( ISBN, Title, Author, PagesNum, PubYear, PubName) </br>
Publisher ( PubName, PubAdress) </br>
Category ( CategoryName, ParentCat) Copy ( ISBN, CopyNumber, ShelfPosition) </br>
Borrowing ( ReaderNr, ISBN, CopyNumber, ReturnDate) </br>
BookCat ( ISBN, CategoryName ) </br>
а) 
```sql
SELECT LastName
FROM Reader
WHERE Address = 'Москва';
```
б) 
```sql
SELECT Book.Author, Book.Title 
FROM Book 
    JOIN Book ON Borrowing.ISBN  = Book.ISBN
    JOIN Reader ON Borrowing.ReaderNr = Reader.ID
WHERE Reader.FirstName = 'Иван' AND Reader.LastName = 'Иванов';
```
в) 
```sql
SELECT Book.ISBN 
FROM BookCat
WHERE BookCat.CategoryName = 'Горы' 
    EXCEPT 
SELECT Book.ISBN 
FROM BookCat 
WHERE BookCat.CategoryName = 'Путешествия';
```
г) 
```sql
SELECT Reader.FirstName, Reader.LastName 
FROM Reader 
    JOIN Borrowing ON Reader.ID = Borrowing.ReaderNr 
WHERE Borrowing.ReturnDate IS NOT NULL;
```
д) 
```sql
SELECT Reader.LastName, Reader.FirstName 
FROM Reader 
    JOIN Borrowing ON Reader.ID == Borrowing.ReaderNr 
WHERE ISBN IN (SELECT ISBN FROM Borrowing JOIN Reader ON Reader.ID == Borrowing.ReaderNr WHERE FirstName = 'Иван' AND LastName = 'Иванов') 
    EXCEPT 
SELECT Reader.LastName, Reader.FirstName 
FROM Borrowing JOIN Reader ON Reader.ID == Borrowing.ReaderNr 
WHERE FirstName = 'Иван' AND LastName = 'Иванов';
```
## Задача 2 
City ( Name, Region ) </br>
Station ( Name, #Tracks, CityName, Region ) </br>
Train ( TrainNr, Length, StartStationName, EndStationName ) </br>
Connection ( FromStation, ToStation, TrainNr, Departure, Arrival) </br>
а) 
```sql
SELECT  TrainNr FROM Connection
    JOIN Station sfr ON sfr.Name = Connection.FromStation
    JOIN Station sto ON sto.Name = Connection.ToStation
WHERE s1.CityName = 'Москва' AND s2.CityName = 'Тверь'
    EXCEPT
    -- если есть Connection из Москвы не в Тверь для этого поезда или 
    -- в Тверь не из Москвы, тогда рейс не прямой.
SELECT TrainNr FROM Connection
    JOIN Station sfr ON sfr.Name = Connection.FromStation   
    JOIN Station sto ON sto.Name = Connection.ToStation
WHERE s1.CityName != 'Москва' OR s2.CityName != 'Тверь' 
```
б)
```sql
SELECT C1.TrainNr, C1.Departure, C2.Arrival
FROM Connection AS C1
    JOIN Connection AS C2 ON C1.TrainNr = C2.TrainNr
WHERE DAY (C1.Departure) = DAY (C2.Arrival) 
    AND C1.FromStation = 'Москва'
    AND С2.ToStation = 'Санкт-Петербург';
```
в) 
- Можно будет из всех connection-ов взять те записи, в которых FromStation = Москва и ToStation = Тверь (то есть просто из моего запроса убрать except)
- ничего не изменится
## Задача 3
Пусть есть $L(A_1, ..., A_n)$ и $R(B_1, ..., B_m)$

Outer Join можно выразить через inner join L и R + значения L, не попавшие inner join L, R + значения R, не попавшие в inner join L, R.

Надо определить inner join:
```
inner_join[condition](L, R) = project[A1, ..., An, B1, ..., Bm](select[condition](cartesian(L, R)))
```
```
outer_join(L, R) = union(
        Inner Join(L, R, Condition),
        project[A1, ..., An, NULL, ..., NULL](L - project[A1, ..., An](inner_join(L, R))),
        project[NULL, ..., NULL, B1, ..., Bm](R - project[B1, ..., Bm](inner_join(L, R)))
    )
```
