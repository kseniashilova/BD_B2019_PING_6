## Задание 5

### Задача 1

<b> 1) Показать все названия книг вместе с именами издателей. </b>

```sql
SELECT Title, PubName FROM Book;
```

<b> 2) В какой книге наибольшее количество страниц? </b>

```sql
SELECT * FROM Book
    WHERE PagesNum =  (select MAX(PagesNum) from Book); 
```

<b> 3) Какие авторы написали более 5 книг? </b>

```sql
SELECT Author 
FROM Book 
GROUP BY Author 
HAVING count(*) > 5;
```

<b> 4) В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг? </b>

```sql
SELECT * FROM Book
    WHERE PagesNum > (SELECT AVG(PagesNum) * 2 FROM BOOK);
```

<b> 5) Какие категории содержат подкатегории? </b>

```sql
SELECT DISTINCT c1.CategoryName FROM Category c1
    WHERE EXISTS (SELECT * FROM Category c2 
        WHERE c1.CategoryName = c2.ParentCat);
```

<b> 6) У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг? </b>

```sql
SELECT Author, count(*) AS CountOfBooks 
    FROM Book GROUP BY Author ORDER BY
        CountOfBooks DESC LIMIT 1;
```

<b> 7) Какие читатели забронировали все книги (не копии), написанные Марком Твеном? </b>

```sql
SELECT r.ID FROM Reader r 
    JOIN Borrowing bor ON r.ID = bor.ReaderNr 
        JOIN Book b ON bor.ISBN = b.ISBN
WHERE b.Author = 'Марк Твен'
    GROUP BY r.ID HAVING COUNT(*) = (
	    SELECT COUNT(*) FROM Book b 
            WHERE b.Author = 'Марк Твен');
```

<b> 8) Какие книги имеют более одной копии? </b>

```sql
SELECT b.* from Book b 
    INNER JOIN Copy c 
        GROUP BY b.ISBN 
        HAVING count(*) > 1;
```
<b> 9) ТОП 10 самых старых книг </b>

```sql
SELECT * FROM Book b
  ORDER BY b.PubYear
  LIMIT 10;
```

<b> 10) Перечислите все категории в категории “Спорт” (с любым уровнем вложености). </b>

```sql
WITH RECURSIVE current_category AS
(
  SELECT * FROM Category
    WHERE ParentCat = 'Спорт'
        UNION ALL
  SELECT subcategory.* FROM Category
    JOIN current_category
      ON ParentCat = current_category.CategoryName
)
SELECT DISTINCT CategoryName FROM current_category;
```

### Задача 2

<b> а) Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4. <b>

```sql
INSERT INTO Borrowing (ReaderNr, ISBN, CopyNumber, ReturnDate)
    SELECT ID, "123456", 4, Null FROM Reader r
        WHERE r.LastName = "Петров" AND r.FirstName = "Василий";
```

<b> б) Удалить все книги, год публикации которых превышает 2000 год. <b>

```sql

DELETE FROM Book
WHERE PubYear > 2000;

DELETE FROM BookCat
WHERE ISBN IN (SELECT ISBN
                FROM Book
                    WHERE PubYear > 2000) ;

DELETE FROM Borrowing
WHERE ISBN IN (SELECT ISBN 
                FROM Book
                    WHERE PubYear > 2000) ;

DELETE FROM Copies
WHERE ISBN IN (SELECT ISBN 
                FROM Book
                    WHERE PubYear > 2000) ;

```

<b> в) Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам). <b>

```sql
UPDATE Borrowing b, BookCat bc 
    SET b.ReturnDate = b.ReturnDate + day(30)
        WHERE b.ReturnDate >= '01.01.2016' 
            AND b.ISBN = bc.ISBN
            AND bc.CategoryName = 'Databases';
```

### Задача 3

<b> 1) <b>

```sql
SELECT s.Name, s.MatrNr FROM Student s 
  WHERE NOT EXISTS ( 
    SELECT * FROM Check c WHERE c.MatrNr = s.MatrNr AND c.Note >= 4.0 ) ; 
```

Данный запрос выберет имена и номера студентов, у которых нет оценки >= 4.

<b> 2) <b>

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

Данный запрос выберет имя, номер и сумму кредитов у профессоров (у профессора 0 кредитов если у него нет лекций).

<b> 3) <b>

```sql
SELECT s.Name, p.Note
  FROM Student s, Lecture lec, Check c
  WHERE s.MatrNr = c.MatrNr AND lec.LectNr = c.LectNr AND c.Note >= 4 
    AND c.Note >= ALL ( 
      SELECT c1.Note FROM Check c1 WHERE c1.MatrNr = c.MatrNr ) 
```

Данный запрос выберет имена и оценки студентов, у которых максимальная оценка >= 4.
