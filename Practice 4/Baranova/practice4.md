# Задание 4

## Задача 1

**Библиотека:** <br/>

**Book:** {[<ins>ISBN: integer</ins>,  Year: integer, Title: string, Author: string, numberOfPages: integer, PublisherId: integer]}<br/>
**Publisher:** {[<ins>PublisherId: integer</ins>, Name: string, Address: string]}<br/>
**Copy:** {[<ins>CopyNumber: integer</ins>, ISBN: integer, Position: integer]}<br/>
**Category:** {[<ins>CategoryName: string</ins>, ParentCategory: string]}<br/>
**Reader:** {[<ins>ReaderId: integer</ins>, Surname: string, Name: string, Address: string, DateOfBirth: date]}<br/>
**ReturnDate:** {[<ins>ReaderId: integer, CopyNumber: intrger</ins>, Date: date]}<br/>

**BelongToCategory:** {[<ins>ISBN: integer, CategoryName: string</ins>]}


#### а) Какие фамилии читателей в Москве?
```sql
SELECT Surname FROM Reader 
	WHERE Address LIKE '%Москва%';
```

#### б) Какие книги (author, title) брал Иван Иванов?
```sql
SELECT DISTINCT Book.Author, Book.Title FROM ReturnDate 
	JOIN Reader ON ReturnDate.ReaderId = Reader.ReaderId
	JOIN Copy ON ReturnDate.CopyNumber = Copy.CopyNumber
	JOIN Book ON Copy.ISBN = Book.ISBN
	WHERE Reader.Name = "Иван" AND Reader.Surname = "Иванов";
```

#### в) Какие книги (ISBN) из категории "Горы" не относятся к категории "Путешествия"? Подкатегории не обязательно принимать во внимание!
```sql
SELECT ISBN FROM BelongToCategory
	WHERE CategoryName = "Горы"
		EXCEPT
	( SELECT ISBN FROM BelongToCategory
		 WHERE CategoryName = "Путешествия" );
```

#### 	г) Какие читатели (LastName, FirstName) вернули копию книгу?
```sql
SELECT DISTINCT Reader.Surname, Reader.Name FROM ReturnDate 
	JOIN Reader ON ReturnDate.ReaderId = Reader.ReaderId 
	WHERE ReturnDate.Date < CURDATE();
```

#### д) Какие читатели (LastName, FirstName) брали хотя бы одну книгу (не копию), которую брал также Иван Иванов (не включайте Ивана Иванова в результат)?
```sql
SELECT DISTINCT Reader.Surname, Reader.Name FROM ReturnDate 
	JOIN Reader ON ReturnDate.ReaderId = Reader.ReaderId
	JOIN Copy ON ReturnDate.CopyNumber = Copy.CopyNumber
  WHERE ( Reader.Name != "Иван" OR Reader.Surname != "Иванов")
  AND Copy.ISBN IN
	( SELECT DISTINCT Copy.ISBN FROM ReturnDate 
		 JOIN Reader ON ReturnDate.ReaderId = Reader.ReaderId
		 JOIN Copy ON ReturnDate.CopyNumber = Copy.CopyNumber
		 WHERE Reader.Name = "Иван" AND Reader.Surname = "Иванов" );
```
<br/>

## Задача 2
**Поезда:**<br/>
**City:** {[<ins>Name: string</ins>, <ins>Region: string</ins>]}<br/>
**Station:** {[<ins>Name: string</ins>, #Tracks, City: string, Region: string]}<br/>
**Train:** {[<ins>TrainNr: integer</ins>, Length: integer, Start: string, End: string]}<br/>

**Connected:** {[<ins>TrainNr: integer, FromStation: string, ToStation: string</ins>, Departure, Arrival]}<br/>

*Предположим, что отношение "Connected" уже содержит транзитивное замыкание. Когда поезд 101 отправляется из Москвы в Санкт-Петербург через Тверь, содержит кортежи для связи Москва->Тверь, Тверь-Санкт-Петербург и Москва->Санкт-Петербург.*

#### а) Найдите все прямые рейсы из Москвы в Тверь.

Прямой рейс - это рейс, при  котором  пассажир  не  делает  пересадок  на  всем  пути  следования.
Тогда, так как отношение "Connected" содержит транзитивное замыкание, то все прямые рейсы из Москвы в Тверь содержатся в Connected.

Для красткости ведем обозначения:

![equation](https://latex.codecogs.com/png.latex?A_1&space;=TrainNr)<br/>
![equation](https://latex.codecogs.com/png.latex?A_2&space;=&space;FromStation)<br/>
![equation](https://latex.codecogs.com/png.latex?A_3&space;=&space;ToStation)<br/>
![equation](https://latex.codecogs.com/png.latex?A_4&space;=&space;\text{City&space;as&space;FromCity})<br/>
![equation](https://latex.codecogs.com/png.latex?A_5&space;=&space;\text{City&space;as&space;ToCity})<br/>

![equation](https://latex.codecogs.com/png.latex?P_{R_1}&space;=&space;(Connected.FromStation&space;=Station.Name))<br/>
![equation](https://latex.codecogs.com/png.latex?P_{R}&space;=&space;(Connected.ToStation&space;=Station.Name))<br/>
![equation](https://latex.codecogs.com/png.latex?P&space;=&space;(FromCity&space;=&space;"Moscow")&space;\cap&space;(ToCity&space;=&space;"Tver"))<br/>

Получим отношение R1: Connected + названия городов отправдения.<br/>
![equation](https://latex.codecogs.com/png.latex?R_1&space;=&space;{\prod}_{A_1,A_2,A_3,A_4}(\sigma_{P_{R_1}}(Connected&space;\times&space;Station)))

Получим отношение R: Connected + названия городов станций отправления и назначения.<br/>
![equation](https://latex.codecogs.com/png.latex?R&space;=&space;{\prod}_{A_1,A_2,A_3,A_4,A_5}(\sigma_{P_{R}}(R_1&space;\times&space;Station)))

Тогда в реляционной алгебре все прямые рейсы из Москвы в Тверь:<br/>
![equation](https://latex.codecogs.com/png.latex?{\prod}_{A_1}(\sigma_P(R)))


#### б) Найдите все многосегментные маршруты, имеющие точно однодневный трансфер из Москвы в Санкт-Петербург.

*Первое отправление и прибытие в конечную точку должны быть в одну и ту же дату. Вы можете применить функцию DAY () к атрибутам Departure и Arrival, чтобы определить дату.*

Необходимо найти только многосегментные маршруты, то есть такие, для которых между Москвой и Санкт-Петербургом есть промежуточные станции. Так как Connected содержит транзитивное замыкание, все эти маршруты содержатся в Connected, но необходимо исключить те, где между Москвой Санкт-Петербургом нет других станций.

Аналогично пункту а) найдем R - таблицу Connected + названия городов станций отправления и назначения, назовем ее здесь R1.


Для красткости введем обозначения:

![equation](https://latex.codecogs.com/png.latex?FirstTransit&space;=R_1\text{&space;as&space;FirstTransit})<br/>
![equation](https://latex.codecogs.com/png.latex?SecondTransit&space;=R_1\text{&space;as&space;SecondTransit})

![equation](https://latex.codecogs.com/png.latex?A_1&space;=FirstTransit.TrainNr)<br/>
![equation](https://latex.codecogs.com/png.latex?A_2&space;=&space;FirstTransit.FromStation)<br/>
![equation](https://latex.codecogs.com/png.latex?A_3&space;=&space;SecondTransit.ToStation)<br/>
![equation](https://latex.codecogs.com/png.latex?A_4&space;=&space;FirstTransit.FromCity)<br/>
![equation](https://latex.codecogs.com/png.latex?A_5&space;=&space;SecondTransit.ToCity)<br/>
![equation](https://latex.codecogs.com/png.latex?A_6&space;=&space;FirstTransit.Departure)<br/>
![equation](https://latex.codecogs.com/png.latex?A_7&space;=&space;SecondTransit.Arrival)<br/>

Если маршрут Москва->Санкт-Петербург многосегментный, то в R1 существует маршрут Москва->N и N->Санкт-Петербург. Соединим таблицу R1 с собой по следующему правилу:

![equation](https://latex.codecogs.com/png.latex?P_1&space;=&space;(FirstTransit.TrainNr=SecondTransit.TrainNr)\cap&space;(FirstTransit.ToStation=SecondTransit.FromStation))

Тогда получим:

![equation](https://latex.codecogs.com/png.latex?R_2&space;=&space;{\prod}_{A_1,A_2,A_3,A_4,A_5,A_6,A_7}(\sigma_{P_{1}}(FirstTransit&space;\times&space;SecondTransit)))

Полученная таблица R2 содержит все существующие многосегментные связи. Осталось выбрать однодневные маршруты из Москвы в Санкт-Петербург.

![equation](https://latex.codecogs.com/png.latex?P&space;=&space;(FromCity&space;=&space;"Moscow")&space;\cap&space;(ToCity&space;=&space;"Saint-Petersburg")&space;\cap&space;(DAY(Departure)=DAY(Arrival)))

И все многосегментные маршруты, имеющие точно однодневный трансфер из Москвы в Санкт-Петербург:

![equation](https://latex.codecogs.com/png.latex?R&space;=&space;{\prod}_{TrainNr}(\sigma_{P}(R_2)))


#### в) Что изменится в выражениях для а) и б), если отношение "Connected" не содержит дополнительных кортежей для транзитивного замыкания?

*Многосегментный маршрут Москва-> Тверь-> Санкт-Петербург содержит только кортежи Москва-> Тверь и Тверь-Санкт-Петербург.*

а) Нужно будет соединить таблицы Cоnnected (as C1) X Cоnnected (as C2), соденять по: 
1. одинаковый номер поезда;
2. станция отправления (C1) - в Москве;
3. станция прибытия (C2) - в Твери;
4. отправление из Москвы раньше прибытия в Трерь.

Тогда мы имеем все односегментные и многомегментные маршруты, соединяющие Москву и Тверь.

б) Можно сделать аналогично пункту в.а), затем убрать уже имеющиеся односегментные маршруты (исходное отношение Connected), и выбрать из оставшихся однодневные.

## Задача 3

Представьте внешнее объединение (outer join ) в виде выражения реляционной алгебры с использованием только базовых операций (select, project, cartesian, rename, union, minus)

Рассмотрим два отношения:

**R1:** {[R11, R12, ..., R1n]}<br/>
**R2:** {[R21, R22, ..., R2m]}<br/>

Определим inner join:<br/>
![equation](https://latex.codecogs.com/png.latex?\text{inner&space;join}(R_1,R_2,P)={\prod}_{R_{11},R_{12},...,R_{1n},R_{21},R_{22},...,R_{2m}}(\sigma_P(R_1\times&space;R_2)))

Тогда outer join это union inner join, значений первого отношения, не попавших в inner join и значений второго отношения, не попавших в inner join. Отсутсвующие данные заполняются null.

![equation](https://latex.codecogs.com/png.latex?\text{outer&space;join}(R_1,R_2,P)=\text{inner&space;join}(R_1,R_2,P)&space;\cup)<br/>
![equation](https://latex.codecogs.com/png.latex?\cup&space;{\prod}_{R_{11},...,R_{1n},null,...,null}\left(R_1-{\prod}_{R_{11},...,R_{1n}}(\text{inner&space;join}(R_1,R_2,P))\right)&space;\cup)<br/>
![equation](https://latex.codecogs.com/png.latex?\cup&space;{\prod}_{null,...,null,R_{21},...,R_{2m}}\left(R_2-{\prod}_{R_{21},...,R_{2m}}(\text{inner&space;join}(R_1,R_2,P))\right))<br/>

