## Задача 1
a) select LastName  
   from Reader  
   where Address = "Москва"  

б) select book.Title, book.Author  
   from Book book, Reader r, Borrowing b  
   where book.ISBN = b.ISBN and b.ReaderNr =   
   (select r.ID   
    from Reader r  
    where r.FirstName = "Иван" and r.LastName = "Иванов")  

в) select ISBN  
   from BookCat  
   where CategoryName = "Горы"  
   minus   
   (select ISBN  
   from BookCat  
   where CategoryName = "Путешествия")  

г) select r.lastname, r.firstname from borrowing b  
   left join reader r on b.readernr = r.id  
   where b.copynumber != null and b.returndate != null  

д) select r.lastname, r.firstname   
   from (select distinct b.isbn, b.readernr from borrowing b jeft join reader r on b.readernr = r.id  
   where r.lastname = 'Иванов' and r.firstname = 'Иван' and b.copynumber = null) t1  
   left join borrowing b on t1.isbn != b.isbn and t1.readernr = b.readernr  
   left join reader r on b.readernr = r.id  

## Задача 2
а) select *  
   from Connection  
   where FromStation = "Москва" and ToStation = "Тверь"  

б) select TrainNr, count(TrainNr) over (partition by TraiNr, day(Departure)) as segm  
   from connection  
   where day(Departure) = day(Arrival) and FromStation = 'Москва' and ToStation = 'Санкт-Петербург'   
   having segm >= 3  

в) 
	а) select TrainNr, lead(ToStation) over (partition by TrainNr, day(Departure) order by Arrival) as next_station  
     from connection  
     where FromStation = 'Москва'  
     having next_station = 'Санкт-Петербург'  


	б) select TrainNr, lead(ToStation) over (partition by TrainNr, day(Departure) order by Arrival) as next_station  
     from connection  
     where day(Departure) = day(Arrival) and FromStation = 'Москва'  
     having next_station = 'Санкт-Петербург'  

## Задача 3


   
