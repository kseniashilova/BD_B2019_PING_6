## Task 1
Отношение — множество кортежей. Множество не хранит информацию о количестве вхождения элементов. Чтобы сохранить в отношении две различимых записи, они должны отличаться как минимум в одном поле кортежа. Ключ позволяет это обеспечить: элементы с различным ключом будут считаться различными; если ключи совпадают, это один и тот же объект.

## Task 2 
### 1: Library
 - BookPublication: {[ <ins>ISBN</ins>, Name, Year, Author, NumberOfPages, PubId ]}
 - Category: {[ <ins>CatId</ins>, Name, ParentId ]}
 - categorizes: {[ <ins>CatId, ISBN</ins> ]}
 - Publisher: {[ <ins>PubId</ins>, Name, Address ]}
 - BookInstance: {[ <ins>ISBN, InstanceId</ins>, Position ]}
 - Reader: {[ <ins>ReaderID</ins>, LastName, FirstName, Address, DateOfBirth ]}
 - Capture: {[ <ins>ReaderID, ISBN, InstanceId</ins>, ReturnDate ]}  (would be a verb, if I hadn't used Chen notation)

### 2.1: Addresses
 - Country: {[ <ins>CountryName</ins> ]}
 - City: {[ <ins>CountryName, CityName</ins> ]}
 - Street: {[ <ins>CountryName, CityName, StreetName</ins> ]}
 - House: {[ <ins>CountryName, CityName, StreetName, HouseNumber</ins> ]}
 - Flat: {[ <ins>CountryName, CityName, StreetName, HouseNumber, FlatNumber</ins> ]}

### 2.2: Football
 - Team: {[ <ins>TeamId</ins> ]}
 - Referee: {[ <ins>RefereeId</ins> ]}
 - play: {[ <ins>FirstTeam, SecondTeam</ins>, RefereeId ]}

### 2.3: Genealogy
 - Human: {[ <ins>HumanId</ins>, Father, Mother ]}
 - Male: {[ <ins>HumanId</ins> ]}
 - Female: {[ <ins>HumanId</ins> ]}

### 3: ER
 - Attribute: {[ <ins>AttrId</ins>, Name ]}
 - Relationship: {[ <ins>RelId</ins>, Name ]}
 - RelationshipAttribute: {[ <ins>AttrId</ins>, RelId ]}
 - Entity: {[ <ins>EntId</ins>, Name ]}
 - EntityAttribute: {[ <ins>AttrId</ins>, EntId, PartOfPK ]}
 - participates: {[ <ins>EntId, RelId</ins>, Functionality, Role ]}
 - WeakEntity: {[ <ins>EntId</ins> ]}
 - IdentifyingRelationship: {[ <ins>RelId</ins>, Identifiee ]}
 - Generalisation: {[ <ins>GenId</ins>, Name, Supertype ]}
 - isSubtype: {[ <ins>GenId, Subertype</ins> ]}

## Task 3

### 1: Trains
 - City: {[ <ins>Region, Name</ins> ]}
 - Station: {[ <ins>Name</ins>, #Tracks, Region, CityName ]}
 - Train: {[ <ins>TrainNr</ins>, Length, StartStation, EndStation ]}
 - connected: {[ <ins>TrainNr_, Departure, _DepartureStation</ins>, Arrival, ArrivalStation ]}  (Ambiguous diagram! Assumed map from the train & any non-end station to the next station)

### 2: Personnel
 - Station: {[ <ins>StatNr</ins>, Name ]}
 - Room: {[ <ins>StatNr, RoomNr</ins>, #Beds ]}
 - StationPersonnel: {[ <ins>PersNr</ins>, #Name, StatNr ]}
 - Caregiver: {[ <ins>PersNr</ins>, Qualification ]}
 - Doctor: {[ <ins>PersNr</ins>, Area, Rank ]}
 - Patient: {[ <ins>PatientNr</ins>, Name, Disease, TreatingDoctor, AdmitedRoomStatNr, AdmitedRoomNr, AdmissionFrom, AdmissionTo ]}
