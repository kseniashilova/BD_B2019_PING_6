# ДЗ 3
## Задание 1
Ключи используются в отношениях для уникальных ссылок на сущности. Уникальность необходима во избежание неоднозначности, когда неизвестно к какой записи таблицы можно обратиться, если в таблице есть повторяющиеся записи.
## Задание 2
### 2.1
- Category: {[*id*, CategoryName, idParentCategory]}
- Book: {[*ISBN*, Title, Year, Author, NumberOfPages, idPublisher]}
- Copy: {[*id*, Position, ISBN]}
- Publisher: {[*id*, CompanyName, Address]}
- Customer: {[*id*, Name, Address, DoB]}
  
- reserved by: {[*idCustomer*, *idCopy*, ReturnDate]}
- categorized as: {[*idBook*, *idCategory*]}

### 2.2.1
- Apartment: {[id, idBuilding]}
- Building: {[id, idStreet]}
- Street: {[id, idCity]}
- City: {[id, idCountry]}
- Country: {[id]}

### 2.2.2
- Referee: {[id]}
- Team: {[id]}
- playing: {[idFirstTeam, idSecondTeam, idReferee]}

### 2.2.3
Male: {[id, idFather, idMother]}
Female: {[id, idFather, idMother]}

### 2.3
- Relationship: {[id, name]}
- Entity: {[id, name]}
- Relationship attribute: {[id, name]}
- Entity attribute: {[id, name, isKey, idEntity]}

- related: {[idEntity, idRelationship]}

## Задание 3
### 3.1
- Train: {[*TrainNr*, Length, Start, End]}
- Station: {[*Name*, NumberOfTracks City]}
- City: {[*Name*, Region]}

- connected: {[TrainNr, nextStationName, Arrival, Departure]}

### 3.2
- Station: {[StatNr, Name]}
- StationPersonell: {[PersNr, #Name, StatNr]}
- Room: {[RoomNr, StatNr, NumberOfBeds]}
- Doctor: {[PersNr, Area, Rank]}
- Caregiver: {[PersNr, Qualification]}
- Patient: {[PatientNr, Name, Disease, DoctorNr, RoomNr, AdmissionFrom, AdmissionTo]}
