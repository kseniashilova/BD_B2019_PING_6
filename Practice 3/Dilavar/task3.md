# ДЗ 3
## Задание 1
Ключи используются в отношениях для уникальных ссылок на сущности. Уникальность необходима во избежание неоднозначности, когда неизвестно к какой записи таблицы можно обратиться, если в таблице есть повторяющиеся записи.
## Задание 2
### 2.1
- Category: {[<ins>id</ins>, CategoryName, idParentCategory]}
- Book: {[<ins>ISBN</ins>, Title, Year, Author, NumberOfPages, idPublisher]}
- Copy: {[<ins>id</ins>, Position, ISBN]}
- Publisher: {[<ins>id</ins>, CompanyName, Address]}
- Customer: {[<ins>id</ins>, Firstname, Lastname, Address, DoB]}
- Reservation: {[<ins>id</ins>, idCopy, idCustomer, ReturnDate]}

- categorized as: {[<ins>idBook</ins>, <ins>idCategory</ins>]}

### 2.2.1
- Apartment: {[<ins>id</ins>, idBuilding]}
- Building: {[<ins>id</ins>, idStreet]}
- Street: {[<ins>id</ins>, idCity]}
- City: {[<ins>id</ins>, idCountry]}
- Country: {[<ins>id</ins>]}

### 2.2.2
- Referee: {[<ins>id</ins>]}
- Team: {[<ins>id</ins>]}
- playing: {[<ins>idFirstTeam</ins>, <ins>idSecondTeam</ins>, <ins>idReferee</ins>]}

### 2.2.3
Male: {[<ins>id</ins>, idFather, idMother]}
Female: {[<ins>id</ins>, idFather, idMother]}

### 2.3
- Relationship: {[<ins>id</ins>, name]}
- Entity: {[<ins>id</ins>, name]}
- Relationship attribute: {[<ins>id</ins>, name]}
- Entity attribute: {[<ins>id</ins>, name, isKey, idEntity]}

- related: {[<ins>idEntity</ins>, <ins>idRelationship</ins>]}

## Задание 3
### 3.1
- Train: {[<ins>TrainNr</ins>, Length, Start, End]}
- Station: {[<ins>Name</ins>, Tracks, NextStation, PreviousStation, City]}
- City: {[<ins>Name</ins>, <ins>Region</ins>]}

- connected: {[<ins>TrainNr</ins>, <ins>StationName</ins>, Arrival, Departure]}

### 3.2
- Station: {[<ins>StatNr</ins>, Name]}
- StationPersonell: {[<ins>PersNr</ins>, #Name, StatNr]}
- Room: {[<ins>RoomNr</ins>, StatNr, NumberOfBeds]}
- Doctor: {[<ins>PersNr</ins>, Area, Rank]}
- Caregiver: {[<ins>PersNr</ins>, Qualification]}
- Patient: {[<ins>PatientNr</ins>, Name, Disease, DoctorNr, RoomNr, AdmissionFrom, AdmissionTo]}
