# Задание 1.
##### а) Какие фамилии читателей в Москве?
```sql
SELECT LastName 
FROM Reader 
WHERE Address LIKE '%Moscow%';
```
##### б) Какие книги (author, title) брал Иван Иванов?
```sql
SELECT Book.Author,Book.Title
FROM Book
JOIN Reader ON Reader.ID = Borrowing.ReaderNr
WHERE Reader.LastName = 'Ivanov' 
	AND Reader.FirstName = 'IVAN';
```
##### в) Какие книги (ISBN) из категории "Горы" не относятся к категории "Путешествия"? Подкатегории не обязательно принимать во внимание!
```sql
SELECT DISTINCT Book.ISBN FROM BookCategory
WHERE BookCategory.CategoryName = 'Mountains'
EXCEPT 
SELECT DISTINCT Book.ISBN
FROM BookCategory
WHERE BookCategory.CategoryName = 'Travel';
```
##### г) Какие читатели (LastName, FirstName) вернули копию книгу?
```sql
SELECT DISTINCT Reader.FirstName,Reader.LastName
FROM Reader
JOIN Borrowing ON Reader.ID = Borrowing.ReaderNr
WHERE Borrowing.ReturnDate IS NOT NULL;
```
##### д) Какие читатели (LastName, FirstName) брали хотя бы одну книгу (не копию), которую брал также Иван Иванов (не включайте Ивана Иванова в результат)?
```sql
FROM Reader
JOIN Borrowing ON Reader.ID == Borrowing.ReaderNr
WHERE ISBN 
IN (SELECT ISBN
		FROM Borrowing
		JOIN Reader 
		ON Reader.ID = Borrowing.ReaderNr
		WHERE LastName = 'Ivanov' 
			AND FirstName = 'Ivan')
		EXCEPT 
		SELECT Reader.LastName,Reader.FirstName
		FROM Borrowing
		JOIN Reader 
		ON Reader.ID == Borrowing.ReaderNr
		WHERE LastName = 'Ivanov' 
			AND FirstName = 'Ivan';
```
# Задание 2.
##### a) Найдите все прямые рейсы из Москвы в Тверь.
```sql 
SELECT conn.TrainNr,conn.Departure,conn.Arrival
FROM Train AS train
JOIN Connection AS conn 
ON Connection.TrainNr = TrainNr
WHERE conn.FromStation = 'Moscow'
	AND conn.ToStation = 'Tver'
	AND NOT EXISTS(
		SELECT 1
		FROM Connection sconn
		WHERE sconn.TrainNr = conn.TrainNr
			AND sconn.FromStation = 'Moscow'
			AND sconn.ToStation <> 'Tver');
```
##### б) Найдите все многосегментные маршруты, имеющие точно однодневный трансфер из Москвы в Санкт-Петербург (первое отправление и прибытие в конечную точку должны быть в одну и ту же дату). Вы можете применить функцию DAY () к атрибутам Departure и Arrival, чтобы определить дату.
```sql
SELECT conn.TrainNr,conn.Departure,sconn.Arrival
FROM Connection AS conn
JOIN Connection AS sconn 
ON conn.TrainNr = sconn.TrainNr
WHERE conn.FromStation = 'Moscow'
	AND sconn.FromStation <> 'Moscow'
	AND sconn.ToStation = 'St.Petersburg'
	AND DAY (conn.Departure) = DAY (conn.Arrival); 
```
##### в) Что изменится в выражениях для а) и б), если отношение "Connection" не содержит дополнительных кортежей для транзитивного замыкания, поэтому многосегментный маршрут Москва-> Тверь-> Санкт-Петербург содержит только кортежи Москва-> Тверь и Тверь-Санкт-Петербург?
Выражение а) можно будет упростить до: 
```sql
SELECT train.TrainNr
FROM Train AS train
JOIN Connection AS conn 
ON train.TrainNr = conn.TrainNr
WHERE conn.FromStation = 'Moscow' 
	AND conn.ToStation = 'Tver';
```
Выражение б) не измениться
# Задание 3.
`L(A1,...,Ai)
R(B1,...,Bj)`
outer_join = inner_join + значения L вне inner_join + значения R вне inner_join.
* p1 - берем строки из L, для которых нет соответствия из R, вместо R - null.
* p2 - берем строки из R, для которых нет соответствия из L, вместо значений L - null.
```
p1 = project[A1,...,Ai,NULL,...,NULL](L - project[A1,...,Ai](inner_join(L,R))) 
p2 = project[NULL,...,NULL,B1,...,Bj](R - project[B1,...,Bj](inner_join(L, R)))
outer_join(L, R) = union(inner_join(L, R),p1,p2)
```
