# Задание 5

## Задача 1

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
SELECT Title, PubName FROM Book; 
```

* В какой книге наибольшее количество страниц?

```sql
SELECT * FROM Book 
WHERE PagesNum = (SELECT MAX(PagesNum) FROM Book);
```

* Какие авторы написали более 5 книг?

```sql
SELECT Author FROM Book 
GROUP BY Author 
HAVING COUNT(*) > 5;
```

* В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?

```sql
SELECT * FROM Book 
WHERE PagesNum > (SELECT 2 * AVG(PagesNum) FROM Book);
```

* Какие категории содержат подкатегории?

```sql
SELECT DISTINCT ParentCat FROM Category
WHERE ParentCat IS NOT NULL;
```

* У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?

```sql
SELECT Author FROM Book 
GROUP BY Author 
HAVING COUNT(*) = (
    SELECT MAX(NumOfBooks) FROM (
        SELECT COUNT(*) AS NumOfBooks 
        FROM Book 
        GROUP BY Author
    ) 
);
```

* Какие читатели забронировали   все книги (не копии), написанные "Марком Твеном"?

```sql
WITH DistinctBorrows (ReaderNr, ReaderLastName, ReaderFirstName, ISBN) AS (
    SELECT DISTINCT Reader.ID, 
                    Reader.LastName, 
                    Reader.FirstName, 
                    Borrowing.ISBN 
    FROM Borrowing 
    JOIN Reader ON Borrowing.ReaderNr = Reader.ID
) 
SELECT DistinctBorrows.ReaderLastName, DistinctBorrows.ReaderFirstName 
FROM DistinctBorrows 
JOIN Book ON DistinctBorrows.ISBN = Book.ISBN 
WHERE Book.Author = "Марк Твен" 
GROUP BY DistinctBorrows.ReaderNr 
HAVING COUNT(*) = (
    SELECT COUNT(ISBN) FROM Book 
    WHERE Author = "Марк Твен"
);
```

* Какие книги имеют более одной копии? 

```sql
SELECT * FROM Book
WHERE ISBN IN (
    SELECT ISBN FROM Copy
    GROUP BY ISBN
    HAVING COUNT(*) > 1
);
```

* ТОП 10 самых старых книг

```sql
SELECT * FROM Book 
ORDER BY PubYear 
LIMIT 10;
```

* Перечислите все категории в категории “Спорт” (с любым уровнем вложености).

```sql
WITH RECURSIVE SportSubcategory AS (
    SELECT * FROM Category 
    WHERE ParentCat = "Спорт" 
        UNION ALL 
    SELECT NextChild.* FROM Category As NextChild 
    JOIN SportSubcategory ON NextChild.ParentCat = SportSubcategory.CategoryName 
) 
SELECT DISTINCT CategoryName FROM SportSubcategory;
```

## Задача 2

Напишите SQL-запросы для следующих действий:

* Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.

```sql
INSERT INTO Borrowing (ReaderNr, ISBN, CopyNumber, ReturnDate) 
SELECT ID, 123456, 4, NULL FROM Reader 
WHERE FirstName = "Василий" AND LastName = "Петров" 
LIMIT 1;
```

* Удалить все книги, год публикации которых превышает 2000 год.

```sql 
SELECT ISBN INTO ISBN2Delete FROM Book WHERE PubYear > 2000; 
DELETE FROM Borrowing WHERE ISBN IN ISBN2Delete; 
DELETE FROM BookCat WHERE ISBN IN ISBN2Delete; 
DELETE FROM Copy WHERE ISBN IN ISBN2Delete; 
DELETE FROM Book WHERE PubYear > 2000; 
DROP TABLE ISBN2Delete;
```

* Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).

```sql 
UPDATE Borrowing 
SET ReturnDate = ReturnDate + 30 
WHERE ReturnDate >= to_date('01.01.2016', 'dd.mm.yyyy') 
AND ISBN in ( 
    SELECT ISBN FROM BookCat 
    WHERE CategoryName = "Базы данных" 
);
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
SELECT s.Name, s.MatrNr FROM Student s 
  WHERE NOT EXISTS ( 
    SELECT * FROM Check c WHERE c.MatrNr = s.MatrNr AND c.Note >= 4.0 ) ; 
```

Найти имена и идентификаторы всех таких студентов, которые не получали оценки выше или равной 4.0 ни по какому из предметов.

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

Найти информацию о профессорах и суммарном количестве кредитов по лекциям, которые они ведут (сумма кредитов = 0, если профессор не ведет лекций).

3.
```sql
SELECT s.Name, p.Note
  FROM Student s, Lecture lec, Check c
  WHERE s.MatrNr = c.MatrNr AND lec.LectNr = c.LectNr AND c.Note >= 4 
    AND c.Note >= ALL ( 
      SELECT c1.Note FROM Check c1 WHERE c1.MatrNr = c.MatrNr ) 
```

Выбрать имена студентов и их оценки по предмету, за который они получили свою наивысшую оценку среди всех остальных предметов, и при этом эта оценка 4 или выше.
