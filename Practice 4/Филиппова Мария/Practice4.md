## Задача 1
a) Какие фамилии читателей в Москве?

'''
   SELECT LastName  
   FROM Reader  
   WHERE Address = 'Москва'  
'''

б) Какие книги брал Иван Иванов?
  
'''
   SELECT book.Title, book.Author  
   FROM Book book, Reader r, Borrowing b  
   WHERE book.ISBN = b.ISBN AND b.ReaderNr =   
   (SELECT r.ID   
    FROM Reader r  
    WHERE r.FirstName = "Иван" AND r.LastName = "Иванов")
'''

в) Какие книги (ISBN) в категории "Горы" не отосятся к категории "Путешествия"?  
'''
   SELECT ISBN  
   FROM BookCat  
   WHERE CategoryName = "Горы"  
   MINUS   
   (SELECT ISBN  
   FROM BookCat  
   WHERE CategoryName = "Путешествия")  
'''

г) Какие читатели вернули копию книги?
'''
   SELECT r.LastName, r.FirstName 
   FROM Borrowing b  
   LEFT JOIN Reader r ON b.ReaderNr = r.ID 
   WHERE b.CopyNumber != NULL AND b.ReturnDate != NULL  
'''

д) Какие читатели брали хотя бы одну книгу (не копию), которую брал также Иван Иванов?
'''
   SELECT r.LastName, r.FirstName   
   FROM (SELECT DISTINCT b.ISBN, b.ReaderNr 
         FROM Borrowing b LEFT JOIN Reader r ON b.ReaderNr = r.ID  
         WHERE r.LastName = 'Иванов' AND r.FirstName = 'Иван' AND b.CopyNumber IS NULL) tmp 
   LEFT JOIN Borrowing b ON tmp.ISBN != b.ISBN AND tmp.ReaderNr = b.ReaderNr  
   LEFT JOIN Reader r ON b.ReaderNr = r.ID
'''

## Задача 2
а) Все прямые рейсы из Москвы в Тверь
'''
   SELECT *  
   FROM Connection  
   WHERE FromStation = "Москва" AND ToStation = "Тверь"  
'''

б) Найти все многосегментные маршруты, имеющие точно однодневный трансфер из Москвы в Санкт-Петербург
'''
   SELECT TrainNr, COUNT(TrainNr) OVER (PARTITION BY TraiNr, DAY(Departure)) AS segment  
   FROM Connection  
   WHERE DAY(Departure) = DAY(Arrival) AND FromStation = 'Москва' and ToStation = 'Санкт-Петербург'   
   HAVING segment >= 3  
'''

в) Если отношение Connection не содержит дополнительных кортежей для транзитивного замыкания
а) 
'''
     SELECT TrainNr, LEAD(ToStation) OVER (PARTITION BY TrainNr, DAY(Departure) ORDER BY Arrival) AS next_station  
     FROM Connection  
     WHERE FromStation = 'Москва'  
     HAVING next_station = 'Санкт-Петербург' 
'''
б) 
'''  
     SELECT TrainNr, LEAD(ToStation) OVER (PARTITION BY TrainNr, DAY(Departure) ORDER BY Arrival) AS next_station  
     FROM Connection  
     WHERE DAY(Departure) = DAY(Arrival) AND FromStation = 'Москва'  
     HAVING next_station = 'Санкт-Петербург' 
'''

## Задача 3

П<sub>&alpha;<sub>1</sub>, .., &alpha;<sub>m</sub>, R.&gamma;<sub>1</sub>, .., R.&gamma;<sub>n</sub>, &beta;<sub>1</sub>, .., &beta;<sub>k</sub></sub>(&sigma;<sub>R.&gamma;<sub>1</sub> = L.&gamma;<sub>1</sub> &and; .. &and; R.&gamma;<sub>n</sub> = L.&gamma;<sub>n</sub></sub>(R &times;L))&cup;П<sub>&alpha;<sub>1</sub>, .., &alpha;<sub>m</sub>, R.&gamma;<sub>1</sub>, .., R.&gamma;<sub>n</sub>, &beta;<sub>1</sub>, .., &beta;<sub>k</sub></sub>(L - &sigma;<sub>R.&gamma;<sub>1</sub>=L.&gamma;<sub>1</sub> &and; .. &and; R.&gamma;<sub>n</sub>=L.&gamma;<sub>n</sub></sub>(L))&cup;П<sub>&alpha;<sub>1</sub>, .., &alpha;<sub>m</sub>, R.&gamma;<sub>1</sub>, .., R.&gamma;<sub>n</sub>, &beta;<sub>1</sub>, .., &beta;<sub>k</sub></sub>(R - &sigma;<sub>R.&gamma;<sub>1</sub>=L.&gamma;<sub>1</sub> &and; .. &and; R.&gamma;<sub>n</sub>=L.&gamma;<sub>n</sub></sub>(R))
