### Задание 1 <br>
1. Показать все названия книг вместе с именами издателей. <br>
select title, pub_name from book; <br>
2. В какой книге наибольшее количество страниц? <br>
<pre>
select * from book
         inner join (
         select max(pages_num) mx from book
         ) as sq
         on pages_num = sq.mx;
</pre>
<br>
3. Какие авторы написали более 5 книг? <br>
<pre>
select distinct book.author, number_of_books from book
         inner join(select author, count(author) as number_of_books from book group by
         author) as rep
         on rep.author = book.author
         where number_of_books > 5;</pre>
<br>
4. В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг? <br>
<pre>
select * from book as orig
        inner join (select avg(pages_num) as avg_page_num from book) as 
        rep on avg_page_num * 2 < orig.pages_num;</pre>
<br>
5. Какие категории содержат подкатегории? <br>
<pre>
select category_name
        from category
        where parent_cat notnull;
</pre>
<br>
6. У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг? <br>
<pre>
select author, count(author) from book
         group by author
         having count(author) = ( select max(number_of_books)
         from book as rep2
         inner join( select author, count(author) as number_of_books
         from book
         group by author ) as rep
         on rep2.author = rep.author );
</pre>
<br>
7. Какие читатели забронировали все книги (не копии), написанные "Марком Твеном"? <br>
8. Какие книги имеют более одной копии? <br>
<pre>
select book.isbn, title from book
      inner join (
      select isbn as key_isbn
      from copy
      group by isbn
      having count(isbn) > 1) as key
      on book.isbn = key_isbn;
</pre>
<br>
9. ТОП 10 самых старых книг <br>
<pre>
select title,pub_year
      from book as orig
      where 10 > ( select count(rep.pub_year) from book 
      as rep where rep.pub_year < orig.pub_year
);
</pre>
<br>
10. Перечислите все категории в категории “Спорт” (с любым уровнем вложености).<br>
<br>
<br>
### Задание 2 <br>
1. Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4. <br>
<pre>
insert into borrowing (reader_nr, isbn, copy_number)
   values ('Василеем Петровым', 123456, 4);</pre>
<br>
2. Удалить все книги, год публикации которых превышает 2000 год. <br>
<pre>
delete from book
   where pub_year > 2000;
</pre>
<br>
3. Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам). <br>
<pre>
update borrowing
    set return_date = interval '1' day + return_date
    where return_date >= '01.01.2016'::date
    and isbn in ( select book_cat.isbn from book_cat where category_name = 'Databases' )
</pre>
<br>

