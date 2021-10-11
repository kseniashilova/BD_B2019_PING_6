## Задача 1
``` sql
SELECT books.Title, books.publisher_name
FROM books;
```
```sql
SELECT *
FROM books
WHERE books.page_count = (
    SELECT MAX(books.page_count)
    FROM books
);
```
```sql
SELECT author, COUNT(author)
FROM books
GROUP BY author
HAVING COUNT(author) > 5;
```
```sql
SELECT *
FROM books
WHERE books.page_count > (
    SELECT 2 * AVG(books.page_count)
    FROM books
);
```
```sql
SELECT DISTINCT c1.name
FROM categories c1
         INNER JOIN categories c2 ON c1.name = c2.parent_name;

select author, count(*) as CountOfBooks
from books
group by author
order by CountOfBooks desc
limit 1;
```
```sql
SELECT r.number, r.last_name, r.first_name, r.address, r.birthday
FROM (SELECT r.number, r.last_name, r.first_name, r.address, r.birthday, COUNT(*) as count
      FROM readers r
               JOIN bookings bor ON bor.reader_number = r.number
               JOIN books book ON book.ISBN = bor.ISBN
      WHERE author = 'Марк Твен'
      GROUP BY r.number) r
WHERE count = (
    SELECT COUNT(*)
    FROM books b
    WHERE b.Author = 'Марк Твен'
);
```
```sql
SELECT copies.ISBN
FROM copies
GROUP BY copies.ISBN
HAVING COUNT(*) > 1;

SELECT *
FROM books
ORDER BY books.year
LIMIT 10;
```
-- не сделал
## Задача 2
```sql
INSERT INTO bookings (reader_number, ISBN, copy_number, return_date)
SELECT number, 123456, 4, NULL
FROM readers
WHERE (first_name = 'Василий' AND last_name = 'Петров');
```
```sql
DELETE
FROM books
WHERE books.year > 2000;
```
```sql
UPDATE bookings
SET return_date = return_date + interval '30' day
WHERE ISBN in (SELECT ISBN
               FROM categories bc
               WHERE bc.name = 'Базы данных')
  AND return_date >= '01.01.2016';
```
## Задача 3
1. Вывести имена и номера студентов, у которых все Note(оценки?) строго ниже 4.0.
2. Вывести номера, имена и суммы кредитов профессоров, и если у профессора нет ни одной лекции, то считать, что сумма кредитов равна нулю.
3. --
