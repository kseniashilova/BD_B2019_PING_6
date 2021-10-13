## Задание 5

### Задача 1

Реляционная схема:

* Reader( <ins>ID</ins>, LastName, FirstName, Address, BirthDate)  <br>
* Book ( <ins>ISBN</ins>, Title, Author, PagesNum, PubYear, PubName)  <br>
* Publisher ( <ins>PubName</ins>, PubAddress)  <br>
* Category ( <ins>CategoryName</ins>, ParentCat)  <br>
* Copy ( <ins>ISBN, CopyNumber</ins>, ShelfPosition)  <br>
* Borrowing ( <ins>ReaderNr, ISBN, CopyNumber</ins>, ReturnDate)  <br>
* BookCat ( <ins>ISBN, CategoryName</ins> )

Запросы:

* Показать все названия книг вместе с именами издателей.

```sql
SELECT books.title,
       publishers.name
FROM books
         JOIN publishers ON books.title = publishers.name;
```

* В какой книге наибольшее количество страниц?

```postgresql
SELECT isbn, title
FROM books
ORDER BY page_count DESC
LIMIT 1;
```

* Какие авторы написали более 5 книг?

```postgresql
SELECT author
FROM books
GROUP BY author
HAVING COUNT(*) > 5;
```

* В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?

```postgresql
SELECT isbn, title
FROM books
WHERE page_count > (SELECT AVG(page_count) FROM books);
```

* Какие категории содержат подкатегории?

```postgresql
SELECT parent.name
FROM categories AS parent
         JOIN categories AS child ON parent.name = child.name;
```

* У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?

```postgresql
SELECT author, COUNT(*) AS books_count
FROM books
GROUP BY author
ORDER BY books_count DESC
LIMIT 1;
```

* Какие читатели забронировали все книги (не копии), написанные "Марком Твеном"?

```postgresql
SELECT bookings.reader_number
FROM bookings
         JOIN books ON books.isbn = bookings.isbn
WHERE books.author = 'Марк Твен'
GROUP BY bookings.reader_number
HAVING COUNT(*) = (SELECT COUNT(*)
                   FROM books
                   WHERE author = 'Марк Твен');
```

* Какие книги имеют более одной копии?

```postgresql
SELECT isbn
FROM copies
GROUP BY isbn
HAVING COUNT(*) > 1;
```

* ТОП 10 самых старых книг

```postgresql
SELECT isbn
FROM books
ORDER BY year
LIMIT 10;
```

* Перечислите все категории в категории “Спорт” (с любым уровнем вложености).

```postgresql
WITH RECURSIVE sport_categories (category_name) AS (
    SELECT name
    FROM categories
    WHERE name = 'Спорт'
    UNION ALL
    SELECT children.name
    FROM categories AS children
             JOIN sport_categories ON children.parent_name = sport_categories.category_name
)
SELECT category_name
FROM sport_categories;
```

### Задача 2

* Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.

```postgresql
INSERT INTO bookings(reader_number, isbn, copy_number, return_date)
SELECT number, 123456, 4, '20211012'
FROM readers
WHERE first_name = 'Василий'
  AND last_name = 'Петров'
```

* Удалить все книги, год публикации которых превышает 2000 год.

```postgresql
DELETE
FROM books
WHERE "year" > 2000
```

* Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на
  30 дней дольше (предположим, что в SQL можно добавлять числа к датам).

```postgresql
UPDATE bookings
SET return_date = return_date + interval '30 days'
WHERE return_date >= '20160101'
  AND EXISTS(SELECT 1
             FROM book_categories
             WHERE book_categories.isbn = bookings.isbn
               AND category_name = 'Базы данных')
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
SELECT s.Name, s.MatrNr
FROM Student s
WHERE NOT EXISTS(
        SELECT * FROM Check c WHERE c.MatrNr = s.MatrNr AND c.Note >= 4.0); 
```

Все студенты, которые получили за все работы оценку ниже 4.0.

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

Для каждого существующего профессора найти сумму кредитов проводимых профессором лекций.

3.

```sql
SELECT s.Name, p.Note
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

Получить всех студентов, наивысший балл которых за работы не менее четырёх (и получить соответствующую наивысшую оценку)
.

Непонятно, зачем тут лекции.

