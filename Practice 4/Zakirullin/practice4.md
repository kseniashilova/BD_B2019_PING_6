## Task 1

### а)

``` sql
SELECT r.LastName AS LastName FROM Reader r WHERE r.Address LIKE '% Moscow, %';
```

### б)

``` sql
SELECT b.Author AS author, b.Name AS title FROM BookPublication b, Capture c, Reader r WHERE r.FirstName = 'Иван' AND r.LastName = 'Иванов' AND c.ReaderID = r.ReaderID AND c.ISBN = b.ISBN;
```

### в)

``` sql
(SELECT c.ISBN AS ISBN FROM categorizes c WHERE c.CatName = 'Горы') MINUS
(SELECT c.ISBN AS ISBN FROM categorizes c WHERE c.CatName = 'Путешествия');
```

### г)

``` sql
SELECT r.LastName AS LastName, r.FirstName AS FirstName FROM Reader r, Capture c WHERE r.ReaderID = c.ReaderID AND c.ReturnDate < CURRENT_DATE();
```

### д)

``` sql
SELECT DISTINCT r.LastName AS LastName, r.FirstName AS FirstName FROM Reader r, Capture c, (SELECT c.ISBN as ISBN FROM Capture c, Reader r WHERE r.FirstName = 'Иван' AND r.LastName = 'Иванов' AND c.ReaderID = r.ReaderID) o WHERE c.ReaderID = r.ReaderID AND c.ISBN = o.ISBN AND NOT (r.FirstName = 'Иван' AND r.LastName = 'Иванов');
```
