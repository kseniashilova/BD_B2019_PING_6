## Задание 5

### Задача 1  

Напишите SQL-запросы:

* Показать все названия книг вместе с именами издателей.
```sql
SELECT title, publisher_name from books; 
```
* В какой книге наибольшее количество страниц?
```sql
SELECT * FROM books
WHERE page_count =
      (SELECT max(page_count) FROM books)
```
* Какие авторы написали более 5 книг?
```sql
SELECT author FROM books
GROUP BY author
HAVING count(*) > 5
```
* В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?
```sql
SELECT title FROM books
WHERE page_count > (SELECT 2 * avg(page_count) FROM books)
```
* Какие категории содержат подкатегории?
```sql
SELECT Distinct c1.name FROM categories c1
JOIN categories c2 on c1.name = c2.parent_name
```
* У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?
```sql
SELECT       author
    FROM     books
    GROUP BY author
    ORDER BY COUNT(*) DESC
    LIMIT    1;
```
* Какие читатели забронировали   все книги (не копии), написанные "Марком Твеном"?
```sql
SELECT first_name, last_name FROM
    (SELECT r.first_name, r.last_name, count(*) as count
FROM readers r
JOIN bookings b on r.number = b.reader_number
JOIN books b2 on b.isbn = b2.isbn
WHERE author = 'Марк Твен'
GROUP BY r.number) as fnlnc
WHERE count = (SELECT count(*)
    FROM books
    WHERE  author = 'Марк Твен')
```
* Какие книги имеют более одной копии? 
```sql
 SELECT * FROM books b
JOIN (
    SELECT copies.isbn, count(*) as count
    FROM copies
    GROUP BY copies.isbn) as copies_n
ON copies_n.isbn = b.isbn and count > 1
```
* ТОП 10 самых старых книг
```sql
SELECT * FROM books
ORDER BY year
LIMIT 10
```
* Перечислите все категории в категории “Спорт” (с любым уровнем вложености).
```sql
WITH RECURSIVE cat AS
                   (
                       SELECT name
                       FROM categories
                       WHERE categories.parent_name = 'Sport'
                       UNION ALL
                       SELECT c.name
                       FROM categories c
                       JOIN cat ON cat.name = c.parent_name
                   )
SELECT DISTINCT name FROM cat
```

### Задача 2

Напишите SQL-запросы для следующих действий:

* Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.
```sql
INSERT INTO bookings (reader_number, copy_number, isbn, return_date)
SELECT number, 4, 123456, '2021-10-12' FROM readers
WHERE first_name = 'Василий' AND
      last_name = 'Петров'
```
* Удалить все книги, год публикации которых превышает 2000 год.
```sql
Delete from bookings
Where exists (
Select 1 from books
where books.isbn = bookings.isbn
and books.year > 2000);
Delete from copies
Where exists (
Select 1 from books
where books.isbn = copies.isbn
and books.year > 2000);
Delete from books
where books.year > 2000
```
* Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).
```sql
UPDATE bookings
SET return_date = return_date + 30
WHERE return_date >= date('2016-01-01') AND
      exists (SELECT 1 FROM book_categories
          WHERE isbn = bookings.isbn and
                category_name = 'Базы данных')
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

Будут выбраны имена и номера (MatrNr) студентов, у которых нет ни одной оценки большей или равной 4.

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

Будут выведены номера и имена профессоров и сумма кредитов лекций, которые ведет каждый профессор. Для профессоров, не ведущих лекций, будет выведено 0.

3.
```sql
SELECT s.Name, p.Note
  FROM Student s, Lecture lec, Check c
  WHERE s.MatrNr = c.MatrNr AND lec.LectNr = c.LectNr AND c.Note >= 4 
    AND c.Note >= ALL ( 
      SELECT c1.Note FROM Check c1 WHERE c1.MatrNr = c.MatrNr ) 
```

 Будут выбраны имена студентов и все их наибольшие оценки, которые >=4.
