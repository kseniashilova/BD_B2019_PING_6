# Task 1 
## Показать все названия книг вместе с именами издателей.
    select Title, PubName from Book;
## В какой книге наибольшее количество страниц?
    select Title from Book where PagesNum=(select Max(PagesNum) from Book);
## Какие авторы написали более 5 книг?
    select Author from Book group by Author having Count(Title)>5;
## В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?
    select Title from Book where PagesNum>2*(select AVG(PagesNum) from Book);
## Какие категории содержат подкатегории?
    select distinct ParentCat from Category;
## У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?
    select Author from Book group by Author order by Count(Title) desc limit 1; 
## Какие читатели забронировали все книги (не копии), написанные "Марком Твеном"?
    with 
    NewBorrows (ReaderNr, LastName, FirstName, ISBN) 
    as 
    (
        select distinct
            Reader.ID, 
            Reader.LastName, 
            Reader.FirstName, 
            Borrowing.ISBN 
        from
            Borrowing 
        join 
            Reader on Borrowing.ReaderNr = Reader.ID
    ) 
    select 
        NewBorrows.LastName, NewBorrows.FirstName 
    from 
        NewBorrows 
    join 
        Book on NewBorrows.ISBN = Book.ISBN 
    where 
        Book.Author = "Марк Твен" 
    group by 
        NewBorrows.ReaderNr 
    having count(*) = (
        select 
            count(ISBN) 
        from 
            Book 
        where 
            Author = "Марк Твен"
    );
## Какие книги имеют более одной копии?
    select 
        Book.Title 
    from 
        Book, Copy 
    where 
        Book.ISBN=Copy.ISBN 
            and Copy.ISBN in 
                (select ISBN from Copy group by ISBN having Count(ISBN)>1);
## ТОП 10 самых старых книг
    select Title from Book order by PubYear limit 10;
## Перечислите все категории в категории “Спорт” (с любым уровнем вложености).
    with recursive 
        current_category 
    as
    (
        select
            * 
        from
            Category
        where 
            Category.ParentCat = 'Спорт'
        union all
        select 
            subcategory.* 
        from 
            Category 
            join
                current_category
                on 
                    subcategory.ParentCat = current_category.CategoryName
    )
    select distinct
        CategoryName 
    from
        current_category;
# Task 2
## Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.
    insert into 
        Borrowing (ReaderNr, ISBN, CopyNumber) 
        values 
        (
                (
                    select 
                    ID 
                    from 
                    Reader 
                    where 
                    LastName=’Петров’ and FirstName=’Василий’
                ),
                123456,
                4
        );
## Удалить все книги, год публикации которых превышает 2000 год.
    delete from Book where PubYear>2000
## Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).
    update 
        Borrowing 
    set 
        ReturnDate = ReturnDate+30 
    where 
        ISBN in (select ISBN from BookCat where CategoryName=’Базы данных’)
# Task 3
## 1
Выведет имена студентов и работ для которых все оценки за эту работу ниже 4.0
## 2
Выведет номера, имена и сумму кредитов лекция профессоров. (для профессоров у которых нет лекций в таблице Lecture сумму кредитов выведет 0)
## 3
Выведет студентов и их оценку при условии что эта оценка не меньше всех оценок за этот вид рыботы и оценка больше 4.0 