# Задание 1

В каждом отношении должен быть как минимум один ключ, чтобы на него могли ссылаться другие отношения.

Ведь если ключа нет, то нельзя однозначно идентифицировать запись в отношении и тогда не будет возможности построить связь (точнее возможность-то будет, просто это будет бесполезно и неинформативно)

# Задание 2

## Пункт 2.1

**category**: {[<ins>CategoryId</ins>, Name, BaseCategoryId]}\
**book**: {[<ins>ISBN</ins>, Title, Year, Author, PageNumber, PublisherId]}\
**publisher**: {[<ins>PublisherId</ins>, Name, Address]}\
**reader**: {[<ins>ReaderId</ins>, Name, Surname, Address, BirthDate]}\
**book_duplicate**: {[<ins>CopyId</ins>, Position, ISBN]}

**contains**: {[<ins>CategoryId</ins>, <ins>ISBN</ins>]}\
**orders**: {[<ins>CopyId</ins>, <ins>ISBN</ins>, <ins>ReaderId</ins>, ReturnDate]}

## Пункт 2.2

### Пункт 2.2.1

**flat**: {[<ins>FlatNumber</ins>, HouseNumber, StreetName, CityName, CountryName, RoomNumber]}\
**house**: {[<ins>HouseNumber</ins>, StreetName, CityName, CountryName, FloorNumber]}\
**street**: {[<ins>StreetName</ins>, CityName, CountryName, Area]}\
**city**: {[<ins>CityName</ins>, CountryName, Population]}\
**country**: {[<ins>CountryName</ins>, FoundationDate]}

### Пункт 2.2.2

**team**: {[<ins>TeamId</ins>]}\
**arbiter**: {[<ins>ArbiterId</ins>]}

**plays_with**: {[<ins>ArbiterId</ins>, <ins>Team1Id</ins>, <ins>Team2Id</ins>]}

### Пункт 2.2.3

**man**: {[<ins>Id</ins>, MotherId, FatherId, Name, Age]}\
**woman**: {[<ins>Id</ins>, MotherId, FatherId, Name, Age]}

# Задание 3

## Пункт 3.1

**City**: {[<ins>Name</ins>, <ins>Region</ins>]}\
**Station**: {[<ins>Name</ins>, Tracks, NextStationName, PreviousStationName, CityName]}\
**Train**: {[<ins>TrainNr</ins>, Length, StartStationName, EndStationName]}

**Connected**: {[<ins>TrainNr</ins>, <ins>StationName</ins>, Arrival, Departure]}

## Пункт 3.2

**Station**: {[<ins>StatNr</ins>, Name]}\
**StationPersonell**: {[<ins>PersNr</ins>, Name, StationNr]}\
**Caregiver**: {[<ins>PersNr</ins>, Qualification]}\
**Doctor**: {[<ins>PersNr</ins>, Rank, Area]}\
**Patient**: {[<ins>PatientNr</ins>, Name, Disease, DoctorNr, AdmissionFrom, AdmissionTo, RoomNr]}\
**Room**: {[<ins>RoomNr</ins>, Beds, StationNr]}
