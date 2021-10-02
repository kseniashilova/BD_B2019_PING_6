## 1. Почему любое отношение в реляционной схеме имеет по крайней мере один ключ?

Для того, чтобы в отношениях не было неоднозначости (то есть повторяющиеся данные) - нужен ключ. Ключ является уникальной ссылкой на "сущность".

## 2. Переведите все диаграммы ER из предыдущей домашней работы в реляционные схемы.

### 2.1

Категория: {[<ins>id</ins>, name, id_parentCategory]}

Книга: {[<ins>ISBN</ins>, title, author, year, pages, id_publisher]}

Копия: {[<ins>id</ins>, position, ISBN]}

Издатель: {[<ins>id</ins>, name, address]}

Читатель: {[<ins>id</ins>, first_name, last_name, birthday, address]}

Бронирование: {[<ins>id</ins>, id_copy, id_reader, return_date]}

### 2.2.1

Квартира: {[id, id_building]}

Дом: {[id, id_street]}

Улица: {[id, id_city]}

Город: {[id, id_country]}

Страна: {[id]}

### 2.2.2

Арбитр: {[<ins>id</ins>]}

Команда: {[<ins>id</ins>]}

Игра: {[<ins>id_first</ins>, <ins>id_second</ins>, <ins>id_arbitos</ins>]}


### 2.2.3

Мужчина: {[<ins>id</ins>, id_father, id_mother]}

Женщина: {[<ins>id</ins>, id_father, id_mother]}

### 2.3

Атрибут: {[ <ins>id</ins>, name, entity_name, partOfKey]}

Сущность: {[<ins>id</ins>, name]}

Связь: {[ <ins>id</ins>, name]}

Сущность-Связь: {[ entity_name, connection_name]}

## 3. Переведите приведенные диаграммы ER в реляционные схемы.

### 3.1

City: {[<ins>Name</ins>, <ins>Region</ins>]}

Station: {[<ins>Name</ins>, #Tracks, nextStation, prevStation, city]}

Train: {[<ins>TrainNr</ins>, length, start, finish]}

- schedule: {[<ins>TrainNr</ins>, <ins>StationName</ins>, arrival, departure]}

### 3.2

Station: {[<ins>StatNr</ins>, name]}

StationPersonell: {[<ins>PersNr</ins>, #name, StatNr]}

Doctor: {[<ins>PersNr</ins>, area, rank]}

Caregiver: {[<ins>PersNr</ins>, qaulification]}

Patient: {[<ins>PatientNr</ins>, name, disease, doctorNr, roomNr, admissionFrom, admissionTo]}

Room: {[<ins>RoomNr</ins>, StatNr, beds]}
