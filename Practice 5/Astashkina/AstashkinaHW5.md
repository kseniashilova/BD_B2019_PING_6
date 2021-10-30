# Home Work 5
## Astashkina Maria BPI196

### Task 1
##### a) 
~~~~sql
SELECT title, author FROM books;
~~~~
#### b)
~~~~sql
SELECT title FROM books
WHERE page_count = (SELECT MAX(page_count) FROM books);
~~~~
#### c)
~~~~sql
SELECT author FROM books GROUP BY author HAVING COUNT(author) > 5;
~~~~
#### d)
~~~~sql
SELECT title FROM BOOKS 
WHERE page_count > (SELECT AVG(page_count) * 2 FROM BOOKS);
~~~~
#### e)
~~~~sql
SELECT DISTINCT parent_name FROM categories WHERE categories.parent_name IS NOT NULL; 
~~~~
#### f)
~~~~sql
SELECT author FROM books GROUP BY author
HAVING count(*) = (SELECT max(c)
                   FROM (SELECT count(*) as c
                         FROM books
                         GROUP BY author) as p);
~~~~
#### g)
~~~~sql
SELECT r.number, r.last_name, r.first_name
FROM (SELECT r.number, r.last_name, r.first_name, count(*) as res
      FROM readers r
               LEFT JOIN bookings b1 ON b1.reader_number = r.number
               LEFT JOIN books b2 ON b2.isbn = b1.ISBN
      WHERE author = 'Марк Твен'
      GROUP BY r.number) as r
WHERE res = (SELECT count(*) FROM books b WHERE b.author = 'Марк Твен');
~~~~
#### h)
~~~~sql
SELECT max(title)
FROM copies c
         LEFT JOIN books b ON b.isbn = c.isbn
GROUP BY c.isbn HAVING count(*) > 1;
~~~~
#### i)
~~~~sql
SELECT title, year FROM books
ORDER BY year LIMIT 10;
~~~~
#### j)
~~~~sql
WITH RECURSIVE SpSubCat AS (
    SELECT *
    FROM categories
    WHERE parent_name = 'Спорт'
    UNION ALL
    SELECT C.*
    FROM categories C
             JOIN SpSubCat ON C.parent_name = SpSubCat.name
)
SELECT distinct name
FROM SpSubCat;
~~~~

### Task 2
##### a)
~~~~ sql
INSERT INTO bookings (reader_number, ISBN, copy_number, return_date)
SELECT number, '123456', 4, null
FROM readers
WHERE first_name = 'Василий'
  AND last_name = 'Петров';
~~~~
#### b)
~~~~sql
DELETE
FROM copies
WHERE isbn IN (SELECT isbn FROM books WHERE books.year > 2000);
DELETE
FROM bookings
WHERE isbn IN (SELECT isbn FROM books WHERE books.year > 2000);
DELETE
FROM book_categories
WHERE isbn IN (SELECT isbn FROM books WHERE books.year > 2000);
DELETE
FROM books
WHERE books.year > 2000;
~~~~
#### c)
~~~~sql
UPDATE bookings SET return_date = bookings.return_date + days(30)
WHERE bookings.return_date >= to_date('01 01 2016', 'DD MM YYYY') AND isbn IN (SELECT isbn FROM book_categories WHERE category_name = 'База данных')
~~~~

### Task 3
+ Student( MatrNr, Name, Semester )
+ Check( MatrNr, LectNr, ProfNr, Note )
+ Lecture( LectNr, Title, Credit, ProfNr )
+ Professor( ProfNr, Name, Room )

#### a)
~~~~sql
SELECT s.Name, s.MatrNr FROM Student s 
  WHERE NOT EXISTS ( 
    SELECT * FROM Check c WHERE c.MatrNr = s.MatrNr AND c.Note >= 4.0 ) ; 
~~~~
Выбрать _имя_ и _уникальный номер_ студентов, которые не получали оценки >= 4.0
#### b) 
~~~~sql
( SELECT p.ProfNr, p.Name, sum(lec.Credit) 
FROM Professor p, Lecture lec 
WHERE p.ProfNr = lec.ProfNr
GROUP BY p.ProfNr, p.Name)
UNION
( SELECT p.ProfNr, p.Name, 0 
FROM Professor p
WHERE NOT EXISTS ( 
  SELECT * FROM Lecture lec WHERE lec.ProfNr = p.ProfNr )); 
~~~~
Выбрать _имя_, _уникальный номер_ и _сумму кредитов_ профессоров, и если у него нет ни одной лекции, то будет считать, что его сумма кредитов = 0.
#### c)
~~~~sql
SELECT s.Name, p.Note
  FROM Student s, Lecture lec, Check c
  WHERE s.MatrNr = c.MatrNr AND lec.LectNr = c.LectNr AND c.Note >= 4 
    AND c.Note >= ALL ( 
      SELECT c1.Note FROM Check c1 WHERE c1.MatrNr = c.MatrNr ) 
~~~~
Выбрать _имя_ и _самые высокие оценки_ студентов, если их наивысшая оценка >= 4.