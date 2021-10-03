# Task 1
## а
    SELECT LastName FROM Reader WHERE Address LIKE '%Москва %'
## б
    SELECT 
    Book.Author, Book.Title 
    FROM 
    Book, Borrowing, Reader 
    WHERE 
    Book.ISBN = Borrowing.ISBN 
        AND Borrowing.ReaderNr = Reader.Id 
        AND Reader.LastName = 'Иванов' 
        AND Reader.FirstName = 'Иван'

## в

    SELECT ISBN FROM BookCat WHERE CategoryName = 'Горы'
    EXCEPT
    SELECT ISBN FROM BookCat WHERE CategoryName = 'Путешествия'

## г

    SELECT 
    Reader.LastName, Reader.FirstName 
    FROM 
    Reader 
    JOIN 
    Borrowing ON Borrowing.ReaderNr = Reader.Id 
    WHERE 
    Borrowing.ReturnDate NOT null

## д

    SELECT 
    Reader.LastName, Reader.FirstName 
    FROM
    Reader, Borrowing
    WHERE
    Borrowing.ReaderNr = Reader.Id 
        AND Reader.LastName <> 'Иванов' 
        AND Reader.FirstName <> 'Иван' 
        AND Borrowing.ISBN IN (
            SELECT 
            Borrowing.ISBN
            FROM
            Reader, Borrowing
            WHERE
            Borrowing.ReaderNr = Reader.Id 
                AND Reader.LastName = 'Иванов' 
                AND Reader.FirstName = 'Иван'
        )
# Task 2
## а
$\sigma _{FromStation='Москва' \wedge ToStation='Тверь'}(Connection)$
## б
$\sigma _{FromStation='Москва' \wedge ToStation='Санкт-Петербург'\wedge DAY(Departure)=DAY(Arrival) \wedge TrainNb \in \prod _{TrainNb}(\sigma _{FromStation<>'Москва' \vee ToStation<>'Санкт-Петербург'}(Connection)) }(Connection)$

Здесь $\prod _{TrainNb}(\sigma _{FromStation<>'Москва' \vee ToStation<>'Санкт-Петербург'}(Connection))$ Это номера всех поездов, у которых часть маршрута не начиналась в Москве или не кончалось в Питере, и если рассматривать при этом только соединения Москвы с Питером, получим только многосегментные маршруты. 
## в
Нужно будет искуственно сделать транзитивного замыкания для этого вместо Connection брать
$\prod _{StartConnection.FromStation, EndConnection.ToStation, StartConnection.TrainNr, StartConnection.Departure, EndConnection.Arrival}($

$\rho _{StartConnection}(Connection)$

$A_{StartConnection.Arrival<=EndConnection.Departure \wedge StartConnection.TrainNr=EndConnection.TrainNr}$

$\rho _{EndConnection}(Connection)$

$)$

# Task 3
$R(A_1,..., A_m, B_1,..., B_k)$

$S(B_1,..., B_k, C_1,..., C_n)$

Сначало ввседем обычный join чтобы не усложнять запись:

$R \, A \, S = \prod _{A_1,..., A_m, B_1,..., B_k, C_1,..., C_n}(\sigma _{R.B1=S.B1 \wedge ... \wedge R.B_k = S.B_k}(R\times S))$

Теперь outer join:

$R \, B \, S = R \, A \, S \cup \prod _{A_1,..., A_m, B_1,..., B_k, null, ... , null}(R - \prod _{A_1,..., A_m, B_1,..., B_k}(R \, A \, S)) \cup \prod _{null, ... , null, B_1,..., B_k, C_1,..., C_n}(S - \prod _{B_1,..., B_k, C_1,..., C_n}(R \, A \, S)) $