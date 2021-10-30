# Home Work 4
## Astashkina Maria BPI196

###Task 1 
+ Reader( ID, LastName, FirstName, Address, BirthDate) 
+ Book ( ISBN, Title, Author, PagesNum, PubYear, PubName) 
+ Publisher ( PubName, PubAdress) 
+ Category ( CategoryName, ParentCat)
+ Copy ( ISBN, CopyNumber, ShelfPosition)
+ Borrowing ( ReaderNr, ISBN, CopyNumber, ReturnDate) 
+ BookCat ( ISBN, CategoryName )

####а) Last name of readers in Moscow:
~~~~sql
SELECT LastName FROM Reader 
    WHERE "Moscow" IN Address
~~~~
####b) Books(Author, Title) borrowed by Ivan Ivanov:
~~~~sql
SELECT DISTINCT Book.Author, Book.Title FROM Borrowing 
    JOIN Reader ON Borrowing.ReaderNr = Reader.Id
    JOIN Copy ON Borrowing.CopyNumber = Copy.CopyNumber
    JOIN Book ON Copy.ISBN = Book.ISBN
    WHERE Reader.FirstName = "Ivan" AND Reader.LastName = "Ivanov"
~~~~
####c) Books(ISBN) from "Mountains" category are not from minor category "Travelling":
~~~~sql
SELECT ISBN FROM BookCat 
    WHERE Category.CategoryName = "Mountains" AND WHERE NOT Category.CategoryName = "Travelling"
~~~~
_ANOTHER SOLUTION_
~~~~sql
SELECT ISBN FROM BookCat
    WHERE Category.CategoryName = "Mountains" EXCEPT WHERE Category.CategoryName = "Travelling"
~~~~
####d) Readers(LastName, FirstName) who returned books:
~~~~sql
SELECT Reader.FirstName, Reader.LastName FROM Reader
    JOIN Borrowing ON Borrowing.ReaderNr = Reader.ID
    WHERE NOT Borrowing.ReturnDate = null
~~~~
####e) Readers (LastName, FirstName) who took at least one book (not a copy),which Ivan Ivanov also took (do not include Ivan Ivanov in the result)?
~~~~sql
SELECT Reader.FirstName, Reader.LastName FROM Borrowing
    JOIN Reader ON Borrowing.ReaderNr = Reader.ID
    JOIN Copy ON Borrowing.CopyNumber = Copy.CopyNumber
    WHERE NOT Reader.FirstName = "Ivan" AND NOT Reader.LastName = "Ivanov" AND Copy.ISBN IN 
        (SELECT DISTINCT Copy.ISBN FROM Borrowing
            JOIN Reader ON Borrowing.ReaderId = Reader.ReaderId
            JOIN Copy ON Borrowing.CopyNumber = Copy.CopyNumber
            WHERE Reader.FirstName = "Ivan" AND Reader.LastName = "Ivanov")
~~~~


###Task 2
+ City (Name, Region) 
+ Station (Name, #Tracks, CityName, Region) 
+ Train (TrainNr, Length, StartStationName, EndStationName) 
+ Connection (FromStation, ToStation, TrainNr, Departure, Arrival)

####a) All direct trips from Moscow to Tver: 
`П<trainNr>(S<City.Name1 = "Moscow" AND City.Name = "Tver">(
П<trainNr, City.Name1, CityName as City.Name2>(
S<Connection.Station2 = Station.Name>(
П<TrainNr, Station1, Station2, City.Name as City.Name1>(
S<Connection.Station1 = Station.Name>(
Connection JOIN Station)) 
JOIN Station))))`
####b) All multi-segment routes that have exactly a one-day transfer from Moscow to St. Petersburg:

`AllRoutes = (S<City.NameFrom = "Moscow" AND City.NameTo = "St. Petersburg">(
П<Station.Name = Connection.ToStation, TrainNr, Departure, Arrival,City.NameFrom, City.Name as City.NameTo>(
S<Station.Name = Connection.ToStation>(
П<FromStationToStationTrainNr, Departure, Arrival, City.Name as City.NameFrom>(
S<Station.Name = Connection.FromStation>(Station JOIN Connection)) JOIN Station))))`

`OneDayTransfer = П<TrainNr>(S<Day(Arrival) = Day(Departure)>(AllRoutes))`

`OneDayTransferMultiSegment = П<TrainNr>(S<Day(Arrival) = Day(Departure)>(AllRoutes - 
S<Count>(S<TrainNr = AllRoutes.TrainNr **  (Train JOIN AllRoutes) = Count(Train)>))AllRoutes`
####c) 
1) We had to JOIN all Connection and search for trains that start from Moscow AND goes to Tver.
2) We search for DAY() <for trains from Moscow> = DAY()<for trains to St. Petersburg>. The multi-segment condition doesn't change.  

###Task 3
Think of an outer join as a relational algebra expression using only basic operations 
(select, project, cartesian, rename, union, minus).

We will JOIN A and B. Not common attributes: AttributesA and AttributesB. Common: CommonAttributes.

_OUTER JOIN_: ((A - П<CommonAttributes, AttributesA> (INNER JOIN)) JOIN (null, null, null, ....)) UNION INNER JOIN 
UNION ((null, null, null, ....) JOIN (B - П<CommonAttributes, AttributesB>(INNER JOIN)))
