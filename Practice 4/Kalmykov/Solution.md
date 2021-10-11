# Задание 1

### а) Какие фамилии читателей в Москве?
```sql
SELECT LastName FROM Reader
WHERE Address LIKE '%Москва%'
```


### б) Какие книги (author, title) брал Иван Иванов?
```sql
SELECT Book.Author, Book.Title FROM Book 
        JOIN Reader ON Borrowing.ReaderNr = Reader.ID 
WHERE Reader.FirstName = 'Иван' AND Reader.LastName = 'Иванов'
```

### в) Какие книги (ISBN) из категории "Горы" не относятся к категории "Путешествия"? Подкатегории не обязательно принимать во внимание!

```sql
SELECT DISTINCT ISBN FROM BookCategory
WHERE BookCategory.CategoryName = 'Mountains'
EXCEPT 
SELECT DISTINCT ISBN FROM BookCategory
WHERE BookCategory.CategoryName = 'Travel';
```

### г) Какие читатели (LastName, FirstName) вернули копию книги?

```sql
SELECT DISTINCT Reader.FirstName, Reader.LastName FROM Reader 
        JOIN Borrowing ON Reader.ID = Borrowing.ReaderNr
WHERE Borrowing.ReturnDate IS NOT NULL;
```

### д) Какие читатели (LastName, FirstName) брали хотя бы одну книгу (не копию), которую брал также Иван Иванов (не включайте Ивана Иванова в результат)?

```sql
SELECT Reader.LastName, Reader.FirstName FROM Reader
        JOIN Borrowing ON Reader.ID = Borrowing.ReaderNr
WHERE ISBN IN (
              SELECT ISBN FROM Borrowing
                      JOIN Reader ON Reader.ID = Borrowing.ReaderNr
              WHERE FirstName = 'Иван' AND LastName = 'Иванов'
              )
EXCEPT
SELECT Reader.LastName, Reader.FirstName FROM Borrowing
        JOIN Reader ON Reader.ID = Borrowing.ReaderNr
WHERE FirstName = 'Иван' AND LastName = 'Иванов'
```


# Задание 2

### а) Найдите все прямые рейсы из Москвы в Тверь.

![image](https://user-images.githubusercontent.com/59960079/135726820-ece37329-61cb-4ea0-ae0f-2907086caab2.png)

### б) Найдите все многосегментные маршруты, имеющие точно однодневный трансфер из Москвы в Санкт-Петербург (первое отправление и прибытие в конечную точку должны быть в одну и ту же дату). Вы можете применить функцию DAY () к атрибутам Departure и Arrival, чтобы определить дату. 

![image](https://user-images.githubusercontent.com/59960079/135729518-03e8413a-2007-4653-b9de-abec5d036a61.png)

### в) Что изменится в выражениях для а) и б), если отношение "Connection" не содержит дополнительных кортежей для транзитивного замыкания, поэтому многосегментный маршрут Москва-> Тверь-> Санкт-Петербург содержит только кортежи Москва-> Тверь и Тверь-Санкт-Петербург?

В а) пришлось бы сделать полный перебор, путём умножения Train x Connection и выбирать записи с подходящими городами

В б) бы аналогично пришлось бы сначала сделать полный перебор (см. выше), далее логика бы сохранилась

# Задание 3

Рассмотрим две сущности:

A(A1, ..., An) и B(B1, ..., Bm)

Тогда, согласно лекции, Outer Join можно представить как Inner Join вместе с значениями из A, которые не попали в Inner Join и значениями из B, которые также не попали в Inner Join 

Тогда для начала выразим Inner Join, а затем через него Outer Join:

`Inner Join(A, B, Condition) = project[A1, ..., An, B1, ..., Bm](select(cartesian(A, B), Condition))`


```
Outer Join(A, B) = 
    union(
        Inner Join(A, B, Condition),

        project[A1, ..., An, NULL, ..., NULL](A - project[A1, ..., An](Inner Join(A, B))),

        project[NULL, ..., NULL, B1, ..., Bm](B - project[B1, ..., Bm](Inner Join(A, B)))
    )
```
