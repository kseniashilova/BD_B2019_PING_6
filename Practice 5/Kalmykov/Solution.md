# Задание 1

### Показать все названия книг вместе с именами издателей. 

```sql
SELECT Title, PubName FROM Book;
```

### В какой книге наибольшее количество страниц? 

```sql
SELECT * FROM Book
WHERE PagesNum = (SELECT max(PagesNum) FROM Book);
```

### Какие авторы написали более 5 книг? 

```sql
SELECT Author FROM Book
GROUP BY Author
HAVING count(*) > 5;
```

### В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг? 

```sql
SELECT * FROM Book
WHERE PagesNum > (SELECT avg(PagesNum) * 2 FROM Book);
```

### Какие категории содержат подкатегории? 

```sql
SELECT DISTINCT a.CategoryName FROM Category a 
INNER JOIN Category b ON a.CategoryName = b.ParentCat;
```

### У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг? 

```sql
SELECT Author FROM Book
WHERE Author = (SELECT Author FROM (
                                    SELECT Author, count(*) FROM Book
                                    GROUP BY Author 
                                    ORDER BY count DESC
                                    LIMIT 1
                                   ) AS AuthorCount
);
```

### Какие читатели забронировали все книги (не копии), написанные "Марком Твеном"?

### Какие книги имеют более одной копии? 

```sql
SELECT * FROM Book
  INNER JOIN (
                SELECT ISBN, count(*) FROM Copy
                GROUP BY ISBN
             ) AS BookNumber
        ON Book.ISBN = BookNumber.ISBN
WHERE count > 1;
```

### ТОП 10 самых старых книг 

```sql
SELECT * FROM Book
ORDER BY PubYear
LIMIT 10;
```

### Перечислите все категории в категории “Спорт” (с любым уровнем вложености).

```sql
WITH RECURSIVE current_category AS
(
  SELECT * FROM Category
  WHERE Category.ParentCat = 'Спорт'
    UNION ALL
  SELECT subcategory.* FROM Category subcategory
    JOIN current_category
      ON subcategory.ParentCat = current_category.CategoryName
)
SELECT DISTINCT CategoryName FROM current_category;
```

# Задание 2

### Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.

```sql
INSERT INTO Borrowing (ReaderNr, ISBN, CopyNumber, ReturnDate)
  SELECT ID, '123456', 4, null FROM Reader
  WHERE FirstName = 'Василий' AND LastName = 'Петров';
```

### Удалить все книги, год публикации которых превышает 2000 год.

```sql
DELETE FROM Borrowing
WHERE ISBN IN (
                    SELECT ISBN FROM Book
                    WHERE PubYear > 2000
              );
              
DELETE FROM Copy
WHERE ISBN IN (
                    SELECT ISBN FROM Book
                    WHERE PubYear > 2000
              );
              
DELETE FROM BookCat
WHERE ISBN IN (
                    SELECT ISBN FROM Book
                    WHERE PubYear > 2000
              );
              
DELETE FROM Book
WHERE PubYear > 2000;
```

### Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).

```sql
UPDATE Borrowing b, BookCat bc
SET ReturnDate = ReturnDate + 30 days
WHERE b.ReturnDate >= to_date('01 01 2016', 'DD MM YYYY') AND b.ISBN = bc.ISBN AND bc.CategoryName = 'Базы Данных';
```

# Задание 3

## 3.1 
```sql
SELECT s.Name, s.MatrNr FROM Student s 
  WHERE NOT EXISTS ( 
    SELECT * FROM Check c WHERE c.MatrNr = s.MatrNr AND c.Note >= 4.0 ); 
```

Выбрать имена и номера зачисления всех студентов, у которых нет ни одной оценки (Note), большей или равной 4.

## 3.2

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

Выбрать всех профессоров с их суммой кредитов, которые они получат за все лекции. Причём, если у профессора нет лекций, то он получит 0 кредитов.
  
## 3.3

```sql
SELECT s.Name, p.Note
  FROM Student s, Lecture lec, Check c
  WHERE s.MatrNr = c.MatrNr AND lec.LectNr = c.LectNr AND c.Note >= 4 
    AND c.Note >= ALL ( 
      SELECT c1.Note FROM Check c1 WHERE c1.MatrNr = c.MatrNr );

```

Выбрать имена студентов (и оценки), которые имеют хотя бы одну оценку >= 4 по лекции, по которой студент имеет наибольшую из всех своих оценок. Причём если есть одинаковые наибольшие оценки >= 4, то все они будут выбраны.
