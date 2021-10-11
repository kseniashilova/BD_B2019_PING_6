## Task 1

``` sql
SELECT title, publisher_name FROM books;
```

``` sql
SELECT ISBN, title FROM books p WHERE p.page_count = (SELECT MAX(page_count) FROM books);
```

``` sql
SELECT Author FROM books GROUP BY Author HAVING COUNT(ISBN) > 5;
```

``` sql
SELECT ISBN, title FROM books p WHERE p.page_count > 2 * (SELECT AVG(page_count) FROM books);
```

``` sql
SELECT name FROM categories WHERE name IN (SELECT parent_name FROM categories);
```

``` sql
WITH Authors AS (SELECT Author, COUNT(ISBN) AS cnt FROM books GROUP BY Author)
SELECT Author FROM Authors WHERE cnt = (SELECT MAX(cnt) FROM Authors);
```

``` sql
SELECT r.number, r.last_name, r.first_name FROM readers r JOIN (SELECT DISTINCT reader_number, ISBN FROM bookings) c ON r.number = c.reader_number WHERE c.ISBN IN (SELECT ISBN FROM books WHERE Author = 'Марк Твен') GROUP BY r.number, r.last_name, r.first_name HAVING COUNT(c.ISBN) = (SELECT COUNT(ISBN) FROM books WHERE Author = 'Марк Твен');
```

``` sql
SELECT i.ISBN AS ISBN, title FROM books p JOIN copies i ON p.ISBN = i.ISBN GROUP BY i.ISBN, title HAVING COUNT(number) > 1;
```

``` sql
SELECT ISBN, title FROM books ORDER BY Year ASC LIMIT 10;
```

``` sql
WITH RECURSIVE Subcategories (Ancestor, Descender) AS (
	SELECT parent_name, name FROM categories
	UNION ALL SELECT s.Ancestor, c.name FROM Subcategories s JOIN categories c ON s.Descender = c.parent_name
) SELECT Descender FROM Subcategories WHERE Ancestor = 'Спорт';
```

## Task 2

``` sql
INSERT INTO bookings(reader_number, ISBN, copy_number, return_date) SELECT r.number, 123456, 4, NOW() FROM readers r WHERE r.last_name = 'Петров' AND r.first_name = 'Василий';
```

``` sql
DELETE FROM books WHERE year > 2000;
```

``` sql
UPDATE bookings b SET return_date = return_date + INTERVAL '30 day' FROM book_categories c WHERE b.ISBN = c.ISBN AND c.category_name = 'Базы данных' AND b.return_date >= TIMESTAMP '2016-01-01';
```
