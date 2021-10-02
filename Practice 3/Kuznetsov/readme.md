# Task 1
Потому что иначе нельзя будет понять для двух сущностей с одинаковыми атрибутами, это одна и таже сущьность или две разные.

# Task 2
## Task 2.1
Читатель {[<ins>Id читателя: integer</ins>, Фамилия: string, Имя: string, Адрес: string, День рождения: date]}

Взятие книги {[<ins>Id взятия: integer</ins>, Id читателя: integer, Дата воврата: date]}

выбирает книгу {[<ins>Id взятия: integer</ins>, <ins>Id копии книги: integer</ins>]}

Копия книги {[<ins>Id копии книги: integer</ins>, Положение на полке: string]}

Оригинал книги {[<ins>Номер ISBN: integer</ins>, Год: integer, Название: string, Автор: string, Колличество страниц: integer]}

копирует {[<ins>Id копии книги: integer</ins>, Номер ISBN оригинала: integer]}

Издательство {[<ins>Id издательсятва: integer</ins>, Имя: string, Адрес: string]}

издает {[<ins>Номер ISBN оригинала: integer</ins>, Id издательсятва: integer]}

Категория {[<ins>Имя: string</ins>]}

принадлежит {[<ins>Имя категории: string</ins>, <ins>Номер ISBN оригинала: integer</ins>]}

содержит подкатегорию {[Имя категории: string, <ins>Имя подкатегории: string</ins>]}

## Task 2.2
### Task 2.2.1
расположен {[<ins>Квартира: string</ins>, <ins>Дом: string</ins>, <ins>Улица: string</ins>, <ins>Город: string</ins>, <ins>Страна: string</ins>]}

### Task 2.2.2
играют {[<ins>Команда хозяев: string</ins>, <ins>Команда гостей: string</ins>, Арбитр: string]}

### Task 2.2.3

Мужчина {[<ins>Id мужчины: integer</ins>, Фамилия: string, Имя: string, Отчетво: string]}

Женщина {[<ins>Id женщины: integer</ins>, Фамилия: string, Имя: string, Отчетво: string]}

являеться отцом сына{[<ins>Id сына: integer</ins>, Id отца: integer]}

являеться отцом дочери{[<ins>Id дочери: integer</ins>, Id отца: integer]}

являеться матерью сына{[<ins>Id сына: integer</ins>, Id матери: integer]}

являеться матерью дочери{[<ins>Id дочери: integer</ins>, Id матери: integer]}

## Task 2.3

Связь {[<ins>Название: string</ins>, Тип соединения: string]}

Атрибут {[<ins>Название: string</ins>]}

Сущность {[<ins>Название: string</ins>]}

связывает {[<ins>Название связи: string</ins>, <ins>Название сущности: string</ins>]}

принадлежит {[<ins>Название атрибутв: string</ins>, Название сущности: string]}

# Task 3
## Task 3.1
Station {[<ins>Name: string</ins>, Tracks: list<string>]}

City {[<ins>Region: string</ins>, <ins>Name: string</ins>]}

Train {[<ins>TrainNr: integer</ins>, Length: integer]}

connected {[<ins>TrainNr: integer</ins>, Departure: string, Arrival: string, First station name: string, Second station name: string]}

start {[<ins>TrainNr: integer</ins>, Station name: string]}

end {[<ins>TrainNr: integer</ins>, Station name: string]}

lie_in {[<ins>Station name: string</ins>, City region: string, City name: string]}

## Task 3.2

Station {[<ins>StatNr: integer</ins>, Name: string]}

Room {[<ins>StatNr: integer</ins>, <ins>RoomNr: integer</ins>, Beds: integer]}

Patient {[<ins>PatientNr: integer</ins>, Name: string, Disease: string]}

StationPersoneell {[<ins>PersNr: integer</ins>, Name: string]}

Doctor {[<ins>PersNr: integer</ins>, Area: string, Rank: string]}

Caregiver {[<ins>PersNr: integer</ins>, Qualification: string]}

works-for {[<ins>PersNr: integer</ins>, StatNr: integer]}

admission {[<ins>PatientNr: integer</ins>, StatNr: integer, RoomNr: integer, From: string, To: string]}

treats {[<ins>PatientNr: integer</ins>, PersNr: integer]}
