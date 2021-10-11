## Задание 1.
a) Показать все названия книг вместе с именами издателей.
SELECT b.title, b.author FROM books b ;
б) В какой книге наибольшее количество страниц?
SELECT * FROM books b WHERE b.page_count =  max(b.page_count); 
в) Какие авторы написали более 5 книг?
SELECT author, COUNT(author) FROM books
     GROUP BY author
     HAVING COUNT(author) > 5 ;
г)В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?
SELECT * FROM books b 
   WHERE b.page_count > 2 * AVG(b.page_count)
д)Какие категории содержат подкатегории?
SELECT DISTINCT c1.name FROM categories c1
   INNER JOIN categories c2 ON c1.name = c2.parent_name ;
е) У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?
-
ж) - 
з)Какие книги имеют более одной копии?
SELECT c.isbn, COUNT(c.isbn) as count FROM copies c
     GROUP BY c.isbn
     HAVING c.count > 1 ;
и) ТОП 10 самых старых книг
SELECT * FROM books b
   ORDER BY b.year
   LIMIT 10 ;
к)Перечислите все категории в категории “Спорт” (с любым уровнем вложености).
-

## Задание 2
1. Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.
INSERT INTO bookings (reader_number, isbn, copy_number, return_date)
   SELECT number, "123456", 4, Null FROM readers r
   WHERE r.first_name = "Василий" AND r.last_name = "Петров" ;

2. Удалить все книги, год публикации которых превышает 2000 год.
DELETE FROM copies c
 WHERE c.isbn IN (SELECT isbn FROM books b
                     WHERE b.year > 2000) 

DELETE FROM books b
 WHERE b.year > 2000 ;


3. Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016,
 чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).
UPDATE bookings bor SET bor.return_date = bor.return_date + day(30)
 WHERE bor.isbn in ( SELECT isbn FROM categories bc
                       WHERE bc.name = "Базы данных" )
       AND bor.return_date >= Date("01.01.2016")


## Задание 3
1. Выбрать имя и номер студентов, у которых нет ни одной оценки большей или равной 4.0.
2. Выбрать номер, имя и сумму всех кредитов (за все лекции) всех лекторов. Если у лектора нет ни одной лекции, сумма кредитов равна 0. 