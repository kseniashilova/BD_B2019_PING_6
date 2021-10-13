# Task 1 
## Показать все названия книг вместе с именами издателей.
    select title, publisher_name from books;
## В какой книге наибольшее количество страниц?
    select title from books where page_count=(select Max(page_count) from books);
## Какие авторы написали более 5 книг?
    select author from books group by author having Count(title)>5;
## В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?
    select title from books where page_count>2*(select AVG(page_count) from books);
## Какие категории содержат подкатегории?
    select distinct parent_name from categories where parent_name is not null;
## У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?
    select author from books group by author order by Count(title) desc limit 1;
## Какие читатели забронировали все книги (не копии), написанные "Марком Твеном"?
    with
    new_bookings (reader_number, last_name, first_name, isbn)
    as
    (
        select distinct
            readers.number,
            readers.last_name,
            readers.first_name,
            bookings.isbn
        from
            bookings
        join
            readers on bookings.reader_number = readers.number
    )
    select
        new_bookings.last_name, new_bookings.first_name
    from
        new_bookings
    join
        books on new_bookings.isbn = books.isbn
    where
        books.author = 'Марк Твен'
    group by
        new_bookings.reader_number, new_bookings.last_name, new_bookings.first_name
    having count(*) = (
        select
            count(isbn)
        from
            books
        where
            author = 'Марк Твен'
    );
## Какие книги имеют более одной копии?
    select distinct 
        books.title
    from
        books, copies
    where
        books.isbn=copies.isbn
            and copies.isbn in
                (select isbn from copies group by isbn having Count(isbn)>1);
## ТОП 10 самых старых книг
    select title from books order by year limit 10;
## Перечислите все категории в категории “Спорт” (с любым уровнем вложености).
    with recursive
        current_category
    as
    (
        select
            *
        from
            categories
        where
            categories.parent_name = 'Спорт'
        union all
        select
            current_category.*
        from
            categories
            join
                current_category
                on
                    current_category.parent_name = current_category.name
    )
    select distinct
        name
    from
        current_category;
# Task 2
## Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.
insert into bookings (reader_number, isbn, copy_number, return_date)
values ((
            select number
            from readers
            where last_name = 'Петров'
              and first_name = 'Василий'
        ),
        123456,
        4,
        '2021-11-11'
        );
## Удалить все книги, год публикации которых превышает 2000 год.
    delete from books where books.year > 2000
## Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).
    update 
        bookings
    set 
        return_date = return_date+30
    where 
        isbn in (select isbn from book_categories where category_name = 'Базы данных');
# Task 3
## 1
Выведет имена студентов и работ для которых все оценки за эту работу ниже 4.0
## 2
Выведет номера, имена и сумму кредитов лекция профессоров. (для профессоров у которых нет лекций в таблице Lecture сумму кредитов выведет 0)
## 3
Выведет студентов и их оценку при условии что эта оценка не меньше всех оценок за этот вид рыботы и оценка больше 4.0 