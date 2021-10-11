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
-- TODO check if it's possible to refer to entity aliases in this way
--      may be `WITH ... AS` is the correct syntax to use
SELECT Author FROM (SELECT Author, COUNT(ISBN) AS cnt FROM books GROUP BY Author) a WHERE cnt = (SELECT MAX(cnt) FROM a);
```

``` sql
SELECT r.number, r.last_name, r.first_name FROM readers r JOIN (SELECT DISTINCT reader_number, ISBN FROM bookings) c ON r.number = c.number WHERE c.ISBN IN (SELECT ISBN FROM books WHERE Author = 'Марк Твен') HAVING COUNT(c.ISBN) = (SELECT COUNT(ISBN) FROM books WHERE Author = 'Марк Твен');
```

``` sql
SELECT i.ISBN AS ISBN, title FROM books p JOIN copies i ON p.ISBN = i.ISBN GROUP BY i.ISBN HAVING COUNT(number) > 1;
```

``` sql
SELECT TOP 10 ISBN, title FROM books ORDER BY Year ASC;
```

``` sql
WITH Subcategories (Ancestor, Descender) AS (
	SELECT parent_name, name FROM categories
	UNION ALL SELECT s.Ancestor, c.name FROM Subcategories s JOIN categories c ON s.Descender = c.parent_name
) SELECT Descender FROM Subcategories WHERE Ancestor = 'Спорт';
```
