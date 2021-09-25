
# Practice 3

## Задние 1
Q: Почему любое отношение в реляционной схеме имеет по крайней мере один ключ?
A: Ключ необходим, чтобы отношение можно было однозначно идентифицировать. В таблице все кортежи должны быть униклальными, двух одинаковых строк быть не должно. Могут быть ситуации, когда в разных отношениях разные объекты имеют одинаковые значения атрибутов. В подобных случаях по уникальному ключу можно отличить одно отношение от друго.го.

## Задние 2
#### 1.  Библиотека

Entities:
	**Book:** {[<ins>ISBN: integer</ins>,  Year: integer, Title: string, Author: string, numberOfPages: integer, PublisherId: integer]} 
	**Publisher:** {[<ins>PublisherId: integer</ins>Name: string, Address: string]}
	**Copy:** {[<ins>CopyId: integer</ins>, ISBN: integer]}
	**Category:** {[<ins>CategoryName: string</ins>, SuperCategory: string]}
	**Reader:** {[<ins>ReaderId: integer</ins>, Surname: string, Name: string, Address: string, DateOfBirth: date]}
	**ReturnDate:** {[<ins>ReaderId: integer, CopyId: intrger</ins>, Date: date]}

Relationships:
	**BelongToCategory:** {[<ins>ISBN: integer, CategoryName: string</ins>]}

<br/>

#### 2.1. Квартира - здание - улица - город - страна

Entities:
	**Apartment:** {[<ins>ApartmentId: integer</ins>, BuildingId: integer]}
	**Building:** {[<ins>BuildingId: integer</ins>, StreetId: integer]}
	**Street:** {[<ins>StreetId: integer</ins>, CityId: integer]}
	**City:** {[<ins>CityId: integer</ins>, CountryId: integer]}
	**Country:** {[<ins>CountryId: integer</ins>]}

<br/>

#### 2.2. Футбол

Entities:
	**Arbitrator:** {[<ins>ArbitratorId: integer</ins>]}
	**Player:** {[<ins>PlayerId: integer</ins>, ArbitratorId: integer]}

Relationships:
	**Play:** {[<ins>Player1Id: integer, Player2Id: integer</ins>]}

<br/>

#### 2.3. Матери/отцы

Entities:
	**Woman:** {[<ins>WomanId: integer</ins>, FatherId: integer, MotherId: integer]}
	**Man:** {[<ins>ManId: integer</ins>, FatherId: integer, MotherId: integer]}

<br/>

#### 3. ER модель

Entities:
	**Entity:** {[<ins>EntityName: string</ins>, IsWeak: bool]}
	**Relationship:** {[<ins>RelationshipName: string</ins>, Type: string]}
	**Attribute:** {[<ins>AttributeName: string</ins>, IsKey: bool, EntityName: string]}

Relationships:
	**EntityInRelationship:** {[<ins>RelationshipName: string, EntityName: string</ins>, ]}

## Задние 3

#### 1. Поезда

Entities:
	**City:** {[<ins>Name: string</ins>, <ins>Region: string</ins>]}
	**Station:** {[<ins>RelationshipName: string</ins>, #Tracks, City: string]}
	**Train:** {[<ins>TrainNr: integer</ins>, Length: integer, Start: string, End: string]}

Relationships:
	**Connected:** {[<ins>TrainNr: integer, Station1: string, Station2: string</ins>, Departure, Arrival]}

<br/>

#### 2. Hospital

Entities:
	**Station:** {[<ins>StatNr: integer</ins>, Name: string]}
	**Room:** {[<ins>RoomNr: integer</ins>, #Beds: integer, StatNr: integer]}
	**StationPersonell:** {[<ins>PersNr: integer</ins>, #Name: string, StatNr: integer]}
	**Doctor:** {[<ins>PersNr: integer</ins>, Area:string, Area: string, Rank: string]}
	**Caregiver:** {[<ins>PersNr: integer</ins>, Qualification: string]}
	**Patient:** {[<ins>PatientNr: integer</ins>, Name: string, Disease: string, DoctorNr: integer, RoomNr: integer, AdmissionFrom, AdmissionTo]}
