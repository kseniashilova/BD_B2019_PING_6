### Задание 1 <br>
а. Какие фамилии читателей в Москве? <br>
select LastName from Reader where Address = 'Moscow' <br>
б. Какие книги (author, title) брал Иван Иванов? <br>
<pre>
select Author, Title
       Book where isbn in (
         select Borrowing.isbn from Borrowing
         where ReaderNr in (
             select ReaderNr from Reader
             where FirstName = 'Ivan' and LastName= 'Ivanov'
             )
)</pre>
<br>
в. Какие книги (ISBN) из категории "Горы" не относятся к категории "Путешествия"? <br>
<pre>
select isbn from BookCat
       where CategoryName = 'Mountains'
       and isbn not in (
           select isbn from BookCat
             where CategoryName = 'Travel'
)</pre>
<br>
г. Какие читатели (LastName, FirstName) вернули копию книгу? <br>
<pre>
select distinct FirstName, LastName Reader
       where id in (
         select ReaderNr Borrowing
         where ReturnDate >= current_date() and isbn in (
             select Borrowing.isbn from Borrowing
             where ReturnDate < current_date
         )
)</pre>
<br>
д. Какие читатели (LastName, FirstName) брали хотя бы одну книгу (не копию), которую брал также Иван Иванов (не включайте Ивана Иванова в результат)? <br>
<pre>
select FirstName, LastName from Reader
   where id in (
      select distinct ReaderNr from Borrowing
      where isbn in (
select isbn from Borrowing where ReaderNr in (
              select id from Reader
              where FirstName = 'Ivan' and LastName = 'Ivanov'
              )
) )
   and (FirstName, LastName) not in (
        select FirstName, LastName from Reader
)</pre>
<br>
<br>
### Задание 2 <br>
а. Найдите все прямые рейсы из Москвы в Тверь <br>
б. Найдите все многосегментные маршруты, имеющие точно однодневный трансфер из Москвы в Санкт-Петербург (первое отправление и прибытие в конечную точку должны быть в одну и ту же дату). Вы можете применить функцию DAY () к атрибутам Departure и Arrival, чтобы определить дату <br>
<pre>
select distinct Connection.TrainNr from Connection conn,
      Connection rep
      where conn.FromStation = 'Moscow'
      and conn.ToStation != 'Sankt-Petersburg'
      and conn.TrainNr = rep.TrainNr
      and rep.ToStation = 'Sankt-Petersburg'
      and date_part('day', rep.ToStation ) - date_part('day', conn.FromStation) =1 </pre>
<br>
в. Что изменится в выражениях для а) и б), если отношение "Connection" не содержит дополнительных кортежей для транзитивного замыкания, поэтому многосегментный маршрут Москва-> Тверь-> Санкт-Петербург содержит только кортежи Москва-> Тверь и Тверь-Санкт-Петербург? <br>
а) select * from connections
where FromStation ='Moscow' and ToStation='Tver' <br>
б) Без изменений
