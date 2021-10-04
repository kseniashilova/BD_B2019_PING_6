Задание 4

Задача 1

Возьмите библиотечную систему, схему которой сделали на предыдущем задании Reader( ID, LastName, FirstName, Address, BirthDate) Book ( ISBN, Title, Author, PagesNum, PubYear, PubName) Publisher ( PubName, PubAdress) Category ( CategoryName, ParentCat) Copy ( ISBN, CopyNumber, ShelfPosition) Borrowing ( ReaderNr, ISBN, CopyNumber, ReturnDate) BookCat ( ISBN, CategoryName ) Напишите SQL-запросы (или выражения реляционной алгебры) для следующих вопросов:

а) Какие фамилии читателей в Москве? 

SELECT LastName 

FROM Reader 

WHERE Address

` `LIKE ‘%Москва%’;

б) Какие книги (author, title) брал Иван Иванов? 

SELECT DISTINCT Book.Author, Book.Title 

FROM Borrowing

JOIN Book ON Borrowing.ISBN = Book.ISBN 

JOIN Reader ON Borrowing.ReaderNr = Reader.Id 

WHERE Reader.FirstName = 'Иван' AND Reader.LastName = 'Иванов';

в) Какие книги (ISBN) из категории "Горы" не относятся к категории "Путешествия"? Подкатегории не обязательно принимать во внимание! 

SELECT DISTINCT ISBN 

FROM BookCategory 

WHERE (CategoryName = 'Горы')

AND

(CategoryName NOT LIKE '%Путешествия%');

г) Какие читатели (LastName, FirstName) вернули копию книгу? 

SELECT DISTINCT Reader. FirstName, Reader.LastName 

FROM Reader

JOIN Borrowing 

ON Reader.ID = Borrowing.ReaderNr 

WHERE Borrowing.ReturnDate IS NOT NULL; 

д) Какие читатели (LastName, FirstName) брали хотя бы одну книгу (не копию), которую брал также Иван Иванов (не включайте Ивана Иванова в результат)?

SELECT Reader.LastName, Reader.FirstName 

FROM Reader 

JOIN Borrowing 

ON Reader.ID = Borrowing.ReaderNr

WHERE ISBN IN 

(SELECT ISBN 

FROM Borrowing 

JOIN Reader 

ON Reader.ID = Borrowing.ReaderNr 

WHERE FirstName = 'Иван' AND LastName = 'Иванов') 

EXCEPT

SELECT Reader.LastName, Reader.FirstName 

FROM Borrowing 

JOIN Reader

ON Reader.ID = Borrowing.ReaderNr 

WHERE FirstName = 'Иван' AND LastName = 'Иванов' 

Задача 2

Возьмите схему для Поездов, которую выполняли в предыдущем задании. City ( Name, Region ) Station ( Name, #Tracks, CityName, Region ) Train ( TrainNr, Length, StartStationName, EndStationName ) ( FromStation, ToStation, TrainNr, Departure, Arrival) Предположим, что отношение "Connection" уже содержит транзитивное замыкание. Когда поезд 101 отправляется из Москвы в Санкт-Петербург через Тверь, содержит кортежи для связи Москва->Тверь, Тверь-Санкт-Петербург и Москва->Санкт-Петербург. Сформулируйте следующие запросы в реляционной алгебре: 

а) Найдите все прямые рейсы из Москвы в Тверь.

Изображение находится в данной папке под названием HW\_4\_image.jpg

б) Найдите все многосегментные маршруты, имеющие точно однодневный трансфер из Москвы в Санкт-Петербург (первое отправление и прибытие в конечную точку должны быть в одну и ту же дату). Вы можете применить функцию DAY () к атрибутам Departure и Arrival, чтобы определить дату. 

PROJECT [C1.TrainNr, C1.Departure, C2.Arrival]

(SELECT [ C1.FromStation = 'Москва' 

AND С2.FromStation <> 'Москва' 

AND C2.ToStation = 'Санкт-Петербург' 

AND DAY (C1.Departure) = DAY (C2.Arrival) ]

` `( JOIN[C1.TrainNr = C2.TrainNr]( RENAME(Connection, C1), RENAME (Connection, C2) ) ) ) 

в) Что изменится в выражениях для а) и б), если отношение "Connection" не содержит дополнительных кортежей для транзитивного замыкания, поэтому многосегментный маршрут Москва-> Тверь-> Санкт-Петербург содержит только кортежи Москва-> Тверь и Тверь-Санкт-Петербург? 

а) Можно будет заменить выражением SELECT(FromStation = ‘Москва’ AND ToStation = ‘Тверь’] (Connection)

б) Не изменится

Задача 3

Представьте внешнее объединение (outer join ) в виде выражения реляционной алгебры с использованием только базовых операций (select, project, cartesian, rename, union, minus)

Определим два набора данных:

L (A1, ..., Am)

R (B1, ..., Bn)

Inner join можно представить как inner join плюс значения L, лежащие вне inner join плюс значения R, лежащие вне inner join

Тогда:

outer join (L, R) = 

union (

inner join(L, R),

project[A1, ..., Am, NULL, ..., NULL](L - project[A1, ..., Am](inner\_join(L, R))), 

project[NULL, ..., NULL, B1, ..., Bn](R - project[B1, ..., Bn](inner\_join(L, R)))

)
