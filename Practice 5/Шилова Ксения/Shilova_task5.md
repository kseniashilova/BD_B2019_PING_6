## Задание 5

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

* Показать все названия книг вместе с именами издателей.

```sql
SELECT b.Title, b.PubName FROM Book b ;
```

* В какой книге наибольшее количество страниц?

```sql
SELECT * FROM Book b 
  WHERE b.PagesNum =  (
  SELECT MAX(b2.PagesNum) FROM Book b2)
```

* Какие авторы написали более 5 книг?

```sql

  SELECT Author, COUNT(Author) FROM Book
    GROUP BY Author
    HAVING COUNT(Author) > 5 ;
```

* В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?

```sql
SELECT * FROM Book b 
  WHERE b.PagesNum > 2 * AVG(b.PagesNum) ; 
```

* Какие категории содержат подкатегории?

```sql
SELECT DISTINCT c1.CategoryName FROM Category c1
  INNER JOIN Category c2 ON c1.CategoryName = c2.ParentCat ;
```

* У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?

```sql
  SELECT Author FROM (
    SELECT Author, COUNT(Author) FROM Book
      GROUP BY Author
    )
   WHERE COUNT(Author) = MAX(COUNT(Author)) ;
```

* Какие читатели забронировали   все книги (не копии), написанные "Марком Твеном"?

```sql
SELECT r.ID, r.LastName, r.FirstName, r.Address, r.BirthDate FROM
  ( SELECT r.ID, r.LastName, r.FirstName, r.Address, r.BirthDate, COUNT(*) as count
   FROM Reader r
      JOIN Borrowing bor ON bor.ReaderNR = r.ID
      JOIN Book book ON book.ISBN = bor.ISBN
    WHERE Author = "Марк Твен"
    GROUP BY r.ID)
WHERE count = COUNT(
        SELECT * FROM Book b
          WHERE b.Author = "Марк Твен"
) ;
```

* Какие книги имеют более одной копии? 

```sql
 SELECT c.ISBN, COUNT(*) as count FROM Copy c
    GROUP BY c.ISBN
    HAVING count > 1 ;
```

* ТОП 10 самых старых книг

```sql
SELECT * FROM Book b
  ORDER BY b.PubYear
  LIMIT 10 ;
```

* Перечислите все категории в категории “Спорт” (с любым уровнем вложености).

```sql

WITH RECURSIVE category AS
 (
   SELECT c.CategoryName, c.ParentCat FROM Category c
   WHERE c.ParentCat = "Спорт"
   UNION ALL
   SELECT category.CategoryName, category.ParentCat
    FROM category, Category c2
    WHERE category.CategoryName = c2.ParentCat
   )
 SELECT * FROM category
```

### Задача 2

Напишите SQL-запросы для следующих действий:

* Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.

```sql
INSERT INTO Borrowing (ReaderNr, ISBN, CopyNumber, ReturnDate)
  SELECT ID, "123456", 4, Null FROM Reader r
  WHERE r.FirstName = "Василий" AND r.LastName = "Петров" ;
```

* Удалить все книги, год публикации которых превышает 2000 год.

```sql

DELETE FROM Copy c
WHERE c.ISBN IN (SELECT ISBN FROM Book b
                    WHERE b.PubYear > 2000) ;
                    
DELETE FROM Borrowing bor
WHERE bor.ISBN IN (SELECT ISBN FROM Book b
                    WHERE b.PubYear > 2000) ;
                    
DELETE FROM BookCat bc
WHERE bc.ISBN IN (SELECT ISBN FROM Book b
                    WHERE b.PubYear > 2000) ;

DELETE FROM Book b
WHERE b.PubYear > 2000 ;
```

* Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).

```sql
UPDATE Borrowing bor
SET bor.ReturnDate = bor.ReturnDate + day(30)
WHERE bor.ISBN in ( SELECT ISBN FROM BookCat bc
                      WHERE bc.CategoryName = "Базы данных" )
      AND bor.ReturnDate >= Date("01.01.2016")
```


### Задача 3

Рассмотрим следующую реляционную схему:

* Student( MatrNr, Name, Semester ) 
* Check( MatrNr, LectNr, ProfNr, Note ) 
* Lecture( LectNr, Title, Credit, ProfNr ) 
* Professor( ProfNr, Name, Room )

Опишите на русском языке результаты следующих запросов:

1.
```sql
SELECT s.Name, s.MatrNr FROM Student s 
  WHERE NOT EXISTS ( 
    SELECT * FROM Check c WHERE c.MatrNr = s.MatrNr AND c.Note >= 4.0 ) ; 
```

Выбрать имя и номер для тех студентов, у которых нет ни одной оценки большей или равной 4.0.

2.
```sql
( SELECT p.ProfNr, p.Name, sum(lec.Credit) 
FROM Professor p, Lecture lec 
WHERE p.ProfNr = lec.ProfNr
GROUP BY p.ProfNr, p.Name)
UNION
( SELECT p.ProfNr, p.Name, 0 
FROM Professor p
WHERE NOT EXISTS ( 
  SELECT * FROM Lecture lec WHERE lec.ProfNr = p.ProfNr )); 
```
Выбрать номер, имя и сумму всех кредитов для профессоров, и считать, что сумма кредитов равна нулю, если у профессора нет ни одной лекции

3.
```sql
SELECT s.Name, p.Note
  FROM Student s, Lecture lec, Check c
  WHERE s.MatrNr = c.MatrNr AND lec.LectNr = c.LectNr AND c.Note >= 4 
    AND c.Note >= ALL ( 
      SELECT c1.Note FROM Check c1 WHERE c1.MatrNr = c.MatrNr ) 
```

Выбрать имена и самые наибольшие оценки для студентов, для которых наибольшая оценка больше или равна 4
