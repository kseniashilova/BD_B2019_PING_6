## Задача 1 

* Показать все названия книг вместе с именами издателей.  
```
SELECT Title, PubName
FROM Book
```
* В какой книге наибольшее количество страниц?  
```
SELECT ISBN  
FROM Book  
WHERE PagesNum = (SELECT MAX(PagesNum) FROM Book)
```
* Какие авторы написали более 5 книг?  
```
SELECT Author FROM (SELECT Author, COUNT(*) cnt FROM Book
GROUP BY Author) t
GROUP BY Author
HAVING t.cnt >= 5
```
* В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?  
```
SELECT * 
FROM Book  
WHERE PagesNum = 2 * (SELECT MEAN(PagesNum) FROM Book)  
```

* Какие категории содержат подкатегории?  
```
SELECT DISTINCT t1.CategoryName 
FROM Category t1 INNER JOIN Category t2
ON t1.CategoryName = t2.ParentCat

```
* У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?  
```
SELECT Author FROM (SELECT Author, COUNT(*) cnt FROM Book
GROUP BY Author) t
GROUP BY Author
HAVING t.cnt = MAX(t.cnt)
```
* Какие читатели забронировали все книги (не копии), написанные "Марком Твеном"?
```
```

* Какие книги имеют более одной копии?
```
SELECT DISTINCT b.Title FROM Book b
LEFT JOIN Copy c ON b.ISBN = c.ISBN
GROUP BY b.Title
HAVING COUNT(*) >=2
```
* ТОП 10 самых старых книг  
```
SELECT Title
FROM Book
HAVING row_number() over(ORDER BY PubYear) <=10
ORDER BY row_number() over(ORDER BY PubYear)
```

* Перечислите все категории в категории “Спорт” (с любым уровнем вложености).
```
```

## Задача 2  

* Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4. 
```
INSERT INTO Borrowing(ReaderNr, ISBN, CopyNumber, ReturnDate)
	SELECT ReaderNr FROM Reader WHERE FirstName = 'Василий' AND LastName = 'Петров', 123456, 4
```

* Удалить все книги, год публикации которых превышает 2000 год.  
```
DELETE * FROM Book WHERE PubYear > 2000
```

* Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).  
```
UPDATE Borrowing
SET ReturnDate = ReturnDate + 30
FROM Borrowing INNER JOIN BookCat ON Borrowing.ISBN = BookCat.ISBN
WHERE CategoryName = 'Базы данных' AND ReturnDate > 01.01.2016
```

## Задача 3  

1. Перечислить студентов, у которых оценка меньше 4.  

2. Вывести учебную нагрузку каждого профессора.  

3. Какая максимальная оценка для каждого студента по всем его предметам?

