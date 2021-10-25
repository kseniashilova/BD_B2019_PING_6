## Задание 5 Сахаров Никита БПИ196

### Задача 1

Возьмите реляционную схему для библиотеки сделаную в задании 3.1:

* Reader( <ins>ID</ins>, LastName, FirstName, Address, BirthDate)  <br>
* Book ( <ins>ISBN</ins>, Title, Author, PagesNum, PubYear, PubName)  <br>
* Publisher ( <ins>PubName</ins>, PubAdress)  <br>
* Category ( <ins>CategoryName</ins>, ParentCat)  <br>
* Copy ( <ins>ISBN, CopyNumber</ins>,, ShelfPosition)  <br>

* Borrowing ( <ins>ReaderNr, ISBN, CopyNumber</ins>, ReturnDate)  <br>
* BookCat ( <ins>ISBN, CategoryName</ins> )

Напишите SQL-запросы:

1) Показать все названия книг вместе с именами издателей.

```sql
    SELECT Book.Title, Book.PubName
    FROM Book;
```

2) В какой книге наибольшее количество страниц?

```sql
    SELECT Book.ISBN
    FROM Book
    WHERE Book.PagesNum == MAX(BOOK.PagesNum); 
```

3) Какие авторы написали более 5 книг?

```sql
    SELECT Author, COUNT(Author)
    FROM Book
    GROUP BY Author
    HAVING COUNT(Author) > 5;
```

4) В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?

```sql
    SELECT *
    FROM Book
    WHERE Book.PagesNum > 2 * AVG(Book.PagesNum); 
```

5) Какие категории содержат подкатегории?

```sql
    SELECT DISTINCT C1.CategoryName
    FROM Category AS C1
             INNER JOIN Category AS C2 ON C1.CategoryName = C2.ParentCat;
```

6) У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?

```sql
    SELECT Author
    FROM (
             SELECT Author, COUNT(Author)
             FROM Book
             GROUP BY Author)
    WHERE COUNT(Author) == MAX(COUNT(Author));
```

7) Какие читатели забронировали все книги (не копии), написанные 'Марком Твеном'?

```sql
    SELECT reader.ID, reader.LastName, reader.FirstName, reader.Address, reader.BirthDate
    FROM (
             SELECT reader.ID, reader.LastName, reader.FirstName, reader.Address, reader.BirthDate, COUNT(*) as count
             FROM Reader reader
                 JOIN Borrowing bor
             ON bor.ReaderNR = reader.ID
                 JOIN Book ON Book.ISBN = bor.ISBN
             WHERE Author = 'Марк Твен'
             GROUP BY reader.ID)
    WHERE count = COUNT(SELECT Book.ISBN FROM Book WHERE Book.Author = 'Марк Твен');
```

8) Какие книги имеют более одной копии?

```sql
    SELECT copy.ISBN, COUNT(*) as count
    FROM Copy copy
    GROUP BY copy.ISBN
    HAVING count > 1;
```

9) ТОП 10 самых старых книг

```sql
    SELECT *
    FROM Book
    ORDER BY Book.PubYear LIMIT 10;
```

10) Перечислите все категории в категории “Спорт” (с любым уровнем вложености).

```Skip```

### Задача 2

Напишите SQL-запросы для следующих действий:

1) Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.

```sql
    INSERT INTO Borrowing (ReaderNr, ISBN, CopyNumber, ReturnDate)
    VALUES ((SELECT DISTINCT FROM Reader WHERE Reader.FirstName == 'Василеем' AND Reader.LastName == 'Петровым'),
        123456, 4, NULL)
```

2) Удалить все книги, год публикации которых превышает 2000 год.

```sql
    DELETE
    FROM Book
    WHERE Bool.PubYear > 2000;
```

3) Измените дату возврата для всех книг категории 'Базы данных', начиная с 01.01.2016, чтобы они были в заимствовании на
   30 дней дольше (предположим, что в SQL можно добавлять числа к датам).

```sql
    UPDATE Borrowing
        JOIN BookCat bookcat
    on bookcat.ISBN == Borrowing.ISBN
    WHERE bookcat.CatName == 'Базы данных' AND Borrowing.ReturnDate > 01.01.2016
    Set Borrowing.ReturnDate = Borrowing.ReturnDate + 30;
``` 

## Задача 3

Рассмотрим следующую реляционную схему:

* Student( MatrNr, Name, Semester )
* Check( MatrNr, LectNr, ProfNr, Note )
* Lecture( LectNr, Title, Credit, ProfNr )
* Professor( ProfNr, Name, Room )

Опишите на русском языке результаты следующих запросов:

1.

```sql
SELECT s.Name, s.MatrNr
FROM Student s
WHERE NOT EXISTS(
        SELECT * FROM Check c WHERE c.MatrNr = s.MatrNr AND c.Note >= 4.0);
```

Имена и MatrNr студентов, у которых не было ни одной проверочной с оценкой больше либо равной 4.

2.

```sql
(SELECT p.ProfNr, p.Name, sum(lec.Credit)
 FROM Professor p,
      Lecture lec
 WHERE p.ProfNr = lec.ProfNr
 GROUP BY p.ProfNr, p.Name)
UNION
(SELECT p.ProfNr, p.Name, 0
 FROM Professor p
 WHERE NOT EXISTS(
         SELECT * FROM Lecture lec WHERE lec.ProfNr = p.ProfNr)); 
```

Список суммарных кредитов по всем лекциям каждого профессора(имя и номер), при чем если у профессора не было лекций то
сумма 0.

3.

```sql
SELECT s.Name, c.Note
FROM Student s,
     Lecture lec,
     Check c
WHERE s.MatrNr = c.MatrNr
  AND lec.LectNr = c.LectNr
  AND c.Note >= 4
  AND c.Note >= ALL (
    SELECT c1.Note
    FROM Check c1
    WHERE c1.MatrNr = c.MatrNr) 
```

Имя и оценка студентов, у которых лучшая оценка среди всех их проверочных больше 4.