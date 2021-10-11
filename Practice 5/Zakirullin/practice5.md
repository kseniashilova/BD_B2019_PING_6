## Task 1

``` sql
SELECT Name, PubName FROM BookPublication;
```

``` sql
SELECT ISBN, Name FROM BookPublication p WHERE p.NumberOfPages = (SELECT MAX(NumberOfPages) FROM BookPublication);
```

``` sql
SELECT Author FROM BookPublication GROUP BY Author HAVING COUNT(ISBN) > 5;
```

``` sql
SELECT ISBN, Name FROM BookPublication p WHERE p.NumberOfPages > 2 * (SELECT AVG(NumberOfPages) FROM BookPublication);
```

``` sql
SELECT CatName FROM Category WHERE CatName IN (SELECT ParentName FROM Category);
```

``` sql
-- TODO check if it's possible to refer to entity aliases in this way
--      may be `WITH ... AS` is the correct syntax to use
SELECT Author FROM (SELECT Author, COUNT(ISBN) AS cnt FROM BookPublication GROUP BY Author) a WHERE cnt = (SELECT MAX(cnt) FROM a);
```

``` sql
SELECT r.ReaderID, r.LastName, r.FirstName FROM Reader r JOIN (SELECT DISTINCT ReaderID, ISBN FROM Capture) c ON r.ReaderID = c.ReaderID WHERE c.ISBN IN (SELECT ISBN FROM BookPublication WHERE Author = 'Марк Твен') HAVING COUNT(c.ISBN) = (SELECT COUNT(ISBN) FROM BookPublication WHERE Author = 'Марк Твен');
```

``` sql
SELECT i.ISBN AS ISBN, Name FROM BookPublication p JOIN BookInstance i ON p.ISBN = i.ISBN GROUP BY i.ISBN HAVING COUNT(InstanceId) > 1;
```

``` sql
SELECT TOP 10 ISBN, Name FROM BookPublication ORDER BY Year ASC;
```

``` sql
WITH Subcategories (Ancestor, Descender) AS (
	SELECT ParentName, CatName FROM Category
	UNION ALL SELECT s.Ancestor, c.CatName FROM Subcategories s JOIN Category c ON s.Descender = c.ParentName
) SELECT Descender FROM Subcategories WHERE Ancestor = 'Спорт';
```
