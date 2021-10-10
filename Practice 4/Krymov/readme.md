### 1

<b> а) </b>

```sql
SELECT LastName 
FROM Reader 
WHERE Address 
LIKE %Москва%;
```

<b> б) </b>

```sql
SELECT Book.Author, Book.Title 
FROM Book 
        JOIN Reader ON Borrowing.ReaderNr = Reader.ID 
WHERE (Reader.LastName = 'Иванов' AND Reader.FirstName = 'Иван')
```

<b> в) </b>

```sql
SELECT DISTINCT Book.ISBN
FROM BookCategory
WHERE BookCategory.CategoryName = 'Горы'

EXCEPT SELECT DISTINCT Book.ISBN 
FROM BookCategory
WHERE BookCategory.CategoryName = 'Путешествия';
```

<b> г) </b>

```sql
SELECT DISTINCT Reader.FirstName, Reader.LastName 
FROM Reader 
        JOIN Borrowing ON Reader.ID = Borrowing.ReaderNr
WHERE Borrowing.ReturnDate IS NOT NULL;
```

<b> д) </b>

```sql
FROM Reader 
        JOIN Borrowing ON Reader.ID == Borrowing.ReaderNr
        WHERE ISBN IN
            (SELECT ISBN FROM Borrowing 
                JOIN Reader ON Reader.ID = Borrowing.ReaderNr 
            WHERE LastName = 'Иванов' AND FirstName = 'Иван') 
        EXCEPT SELECT Reader.LastName, Reader.FirstName 
FROM Borrowing 
        JOIN Reader ON Reader.ID == Borrowing.ReaderNr 
WHERE (LastName = 'Иванов' AND FirstName = 'Иван');
```

### 2

<b> а) <b>

```sql
SELECT Tr.TrainNr 
FROM Train AS Tr 
        JOIN Connection AS Conn ON Tr.TrainNr = Conn.TrainNr
WHERE Conn.FromStation = 'Москва' AND Conn.ToStation = 'Тверь'
```

<b> б) <b>

```sql
SELECT Conn1.Departure,
       Conn1.TrainNr,
       Conn2.Arrival
FROM Connection AS Conn1
         JOIN Connection AS Conn2 ON Conn1.TrainNr = Conn2.TrainNr
WHERE (DAY (Conn1.Departure) = DAY (Conn2.Arrival) AND
    (Сonn2.ToStation = 'Санкт-Петербург' AND Conn1.FromStation = 'Москва'));
```


### 3

```
outer_join(A, B) = 
    union(
        inner_join(A, B, Condition),
        project[A1, ..., An, NULL, ..., NULL](A - project[A1, ..., An](inner_join(A, B))),
        project[NULL, ..., NULL, B1, ..., Bm](B - project[B1, ..., Bm](inner_join(A, B)))
    )
```