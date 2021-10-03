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

## Task 2

### а)

Под прямым рейсом понимаем все поезда, у которых две данные станции — смежные (т. е. \* - A - B - \*).

`ist` нужно, чтобы не учитывать несколько станций в одном и том же городе по пути.

``` sql
(SELECT c.TrainNr AS TrainNr FROM connected c, Station dst, Station ast WHERE c.DepartureStation = dst.Name AND c.ArrivalStation = ast.Name AND dst.CityName = 'Москва' AND ast.CityName = 'Тверь') MINUS (SELECT c1.TrainNr AS TrainNr FROM connected c1, connected c2, Station dst, Station ist, Station ast WHERE c1.DepartureStation = dst.Name AND c1.ArrivalStation = ist.Name AND c2.ArrivalStation = ast.Name AND c1.ArrivalStation = c2.DepartureStation AND c1.TrainNr = c2.TrainNr AND dst.CityName = 'Москва' AND NOT ist.CityName = dst.CityName AND NOT ist.CityName = ast.CityName AND ast.CityName = 'Тверь');
```

### б)

Предполагается, что многосегментность строгая (для нестрогой достаточного одного соединения).

``` sql
SELECT c1.TrainNr AS TrainNr FROM connected c1, connected c2, Station dst, Station ast WHERE c1.DepartureStation = dst.Name AND c2.ArrivalStation = ast.Name AND c1.ArrivalStation = c2.DepartureStation AND c1.TrainNr = c2.TrainNr AND dst.CityName = 'Москва' AND ast.CityName = 'Санкт-Петербург' AND DAY(c1.Departure) = DAY(c2.Arrival);
```

### в)

Пункт а станет проще: останется только выражение слева от вычитания.

Пункт б нельзя будет решить, не имея возможности создавать цикл/рекурсию.

## Task 3

Сущности L, R, поля, по которому join — C.

Π\_(¬RC)(σ\_(C = RC)(L × ρ\_(RC ← C)(R))) ∪ Π\_(¬RC)(σ\_(C = RC)(L × ρ\_(RC ← C)(Π\_(C)(L) ∖ Π\_(C)(R)))) ∪ Π\_(¬LC)(σ\_(C = LC)(R × ρ\_(LC ← C)(Π\_(C)(R) ∖ Π\_(C)(L))))
