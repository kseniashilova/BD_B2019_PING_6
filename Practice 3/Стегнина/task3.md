1. Так как все кортежи в отношении уникальны, любые два кортежа должны различаться хотя бы в одном атрибуте. Этот атрибут и есть ключ.

2. 1) 

Экземпляр: {[<ins> Номер_копии </ins>, <ins> ISBN </ins>, Положение_на_полке]}
Запись_о_взятой_книге: {[<ins> Номер_записи </ins>, Дата_возвращения, ISBN_взятой_книги, Номер_взятой_копии, Номер_читателя]}
Книга: {[<ins> ISBN </ins>, Год, Название, Автор, Количество страниц, Имя_издательства]}
Категория: {[<ins> Имя </ins>, Имя_старшей_категории]}
Издательство: {[<ins> Имя </ins>], Адрес}
Читатель: {[<ins> Номер </ins>, Фамилия, Имя, Адрес, День_рождения]}
Категория_книги: {[<ins> ISBN </ins>, <ins> Имя_категории </ins>]}
2.1)
Страна: {[<ins> Название </ins>]}
Город: {[<ins> Название_страны </ins>, <ins> Название_города </ins>]}]}
Улица: {[<ins> Название_страны </ins>, <ins> Название_города </ins>, <ins> Название_улицы </ins>]}
Дом: {[<ins> Название_страны </ins>, <ins> Название_города </ins>, <ins> Название_улицы </ins>, <ins> Номер_дома </ins>]}
Квартира: {[<ins> Название_страны </ins>, <ins> Название_города </ins>, <ins> Название_улицы </ins>, <ins> Номер_дома </ins>, <ins> Номер_квартиры </ins>]}
2.2)
Арбитр: {[<ins> Id </ins>]}
Команда: {[<ins> Id </ins>]}
Матч: {[<ins> Номер_матча </ins>, Команда_1, Команда_2, Арбитр_id]}
2.3)
Женщина: {[<ins> id </ins>, Мать_id, Отец_id]}
Мужчина: {[<ins> id </ins>, Мать_id, Отец_id]}
3)
Сущность: {[<ins> Имя </ins>]}
Атрибут: {[<ins> Имя </ins>, <ins> Имя_сущности </ins>]}
Связь: {[<ins> Имя </ins>, <ins> Сущность_1 </ins>, <ins> Сущность_2 </ins>, Тип]}

3.

1)

Station: {[<ins> Name </ins>], #Tracks, <ins> City_Name </ins>, <ins> City_Region </ins>]}
City: {[<ins> Name </ins>, <ins> Region </ins>]}
Train: {[<ins> TrainNr </ins>, Length, Start_Station, End_Station]}
Connection: {[Station1_Name, Station1_City_Name, Station1_City_Region, Station2_Name, Station2_City_Name, Station2_City_Region, <ins> Departure </ins>, Arrival, <ins> Train_ID </ins>]}
2)
Room: {[<ins> RoomNr </ins>, <ins> StatNr </ins>, #Beds]}
Patient: {[<ins> PatientNr </ins>, Name, Disease, Doctor_id, RoomNr, from, to]}
Station: {[<ins> StatNr </ins>, Name]}
StationPersonell: {[<ins> PersNr </ins>, <ins> StatNr </ins>, #Name]}
Caregiver: {[<ins> PersNr </ins>, Qualification]}
Doctor: {[<ins> PersNr </ins>, Area, Rank]}

