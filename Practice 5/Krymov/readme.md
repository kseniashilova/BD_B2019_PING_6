### 1

* Показать все названия книг вместе с именами издателей.

```sql
SELECT Title, PubName FROM Book;
```

* В какой книге наибольшее количество страниц?

```sql
SELECT Title FROM Book WHERE PagesNum = MAX(PagesNum); 
```

* Какие авторы написали более 5 книг?

```sql
  SELECT Author, COUNT(Author) FROM Book 
  GROUP BY Author HAVING COUNT(Author) > 5;
```

* В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?

```sql
SELECT * FROM Book WHERE PagesNum > (2 * AVG(PagesNum)); 
```

* Какие категории содержат подкатегории?

```sql
SELECT DISTINCT a.CategoryName FROM Category a
  INNER JOIN Category b ON a.CategoryName = b.ParentCat;
```

* У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?


* Какие читатели забронировали   все книги (не копии), написанные "Марком Твеном"?


* Какие книги имеют более одной копии?

```sql
 SELECT ISBN, COUNT(*) as cnt FROM Copy GROUP BY ISBN HAVING cnt > 1;
```

* ТОП 10 самых старых книг

```sql
SELECT * FROM Book ORDER BY PubYear LIMIT 10;
```

* Перечислите все категории в категории “Спорт” (с любым уровнем вложености).


### 2

* Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.

```sql
INSERT INTO Borrowing (ReaderNr, ISBN, CopyNumber, ReturnDate)
  SELECT ID, "123456", 4, null FROM Reader
  WHERE FirstName = "Василий" AND LastName = "Петров";
```

* Удалить все книги, год публикации которых превышает 2000 год.

```sql
DELETE FROM Copy
WHERE ISBN IN (SELECT ISBN FROM Book WHERE PubYear > 2000);
                    
DELETE FROM Borrowing
WHERE ISBN IN (SELECT ISBN FROM Book WHERE PubYear > 2000);
                    
DELETE FROM BookCat
WHERE ISBN IN (SELECT ISBN FROM Book WHERE PubYear > 2000);

DELETE FROM Book WHERE PubYear > 2000;
```

* Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).

```sql
UPDATE Borrowing
SET ReturnDate = ReturnDate + day(30)
WHERE ReturnDate >= Date("01.01.2016")
    AND ISBN in 
        (SELECT ISBN FROM BookCat WHERE CategoryName = "Базы данных");
```


### 3

1.
```sql
SELECT s.Name, s.MatrNr FROM Student s 
  WHERE NOT EXISTS ( 
    SELECT * FROM Check c WHERE c.MatrNr = s.MatrNr AND c.Note >= 4.0 ) ; 
```

Вернуть имя студента и номер, если нет ни одной оценки больше или равно 4

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

Вернуть имя, номер и сумма всех кредитов для профессоров (для дефолтного значения, если лекций нет, принят ноль)

3.
```sql
SELECT s.Name, p.Note
  FROM Student s, Lecture lec, Check c
  WHERE s.MatrNr = c.MatrNr AND lec.LectNr = c.LectNr AND c.Note >= 4 
    AND c.Note >= ALL ( 
      SELECT c1.Note FROM Check c1 WHERE c1.MatrNr = c.MatrNr ) 
```

Вернуть имена и наибольшие оценки студентов у которых наибольший балл выше или равен 4