
# Practice 3

## Задние 1
Q: Почему любое отношение в реляционной схеме имеет по крайней мере один ключ?

A: Ключ необходим, чтобы отношение можно было однозначно идентифицировать. В таблице все кортежи должны быть униклальными, двух одинаковых строк быть не должно. Могут быть ситуации, когда в разных отношениях разные объекты имеют одинаковые значения атрибутов. В подобных случаях по уникальному ключу можно отличить одно отношение от другого.<br/>

## Задние 2
#### 1.  Библиотека

Entities:<br/>
	**Book:** {[<ins>ISBN: integer</ins>,  Year: integer, Title: string, Author: string, numberOfPages: integer, PublisherId: integer]}<br/>
	**Publisher:** {[<ins>PublisherId: integer</ins>Name: string, Address: string]}
	**Copy:** {[<ins>CopyId: integer</ins>, ISBN: integer]}<br/>
	**Category:** {[<ins>CategoryName: string</ins>, SuperCategory: string]}<br/>
	**Reader:** {[<ins>ReaderId: integer</ins>, Surname: string, Name: string, Address: string, DateOfBirth: date]}<br/>
	**ReturnDate:** {[<ins>ReaderId: integer, CopyId: intrger</ins>, Date: date]}

Relationships:<br/>
	**BelongToCategory:** {[<ins>ISBN: integer, CategoryName: string</ins>]}

<br/>

#### 2.1. Квартира - здание - улица - город - страна

Entities:<br/>
	**Apartment:** {[<ins>ApartmentId: integer</ins>, BuildingId: integer]}<br/>
	**Building:** {[<ins>BuildingId: integer</ins>, StreetId: integer]}<br/>
	**Street:** {[<ins>StreetId: integer</ins>, CityId: integer]}<br/>
	**City:** {[<ins>CityId: integer</ins>, CountryId: integer]}<br/>
	**Country:** {[<ins>CountryId: integer</ins>]}

<br/>

#### 2.2. Футбол

Entities:<br/>
	**Arbitrator:** {[<ins>ArbitratorId: integer</ins>]}<br/>
	**Player:** {[<ins>PlayerId: integer</ins>, ArbitratorId: integer]}

Relationships:<br/>
	**Play:** {[<ins>Player1Id: integer, Player2Id: integer</ins>]}

<br/>

#### 2.3. Матери/отцы

Entities:<br/>
	**Woman:** {[<ins>WomanId: integer</ins>, FatherId: integer, MotherId: integer]}<br/>
	**Man:** {[<ins>ManId: integer</ins>, FatherId: integer, MotherId: integer]}

<br/>

#### 3. ER модель

Entities:<br/>
	**Entity:** {[<ins>EntityName: string</ins>, IsWeak: bool]}<br/>
	**Relationship:** {[<ins>RelationshipName: string</ins>, Type: string]}<br/>
	**Attribute:** {[<ins>AttributeName: string</ins>, IsKey: bool, EntityName: string]}

Relationships:<br/>
	**EntityInRelationship:** {[<ins>RelationshipName: string, EntityName: string</ins>]}<br/>

## Задние 3

#### 1. Поезда

Entities:<br/>
	**City:** {[<ins>Name: string</ins>, <ins>Region: string</ins>]}<br/>
	**Station:** {[<ins>RelationshipName: string</ins>, #Tracks, City: string]}<br/>
	**Train:** {[<ins>TrainNr: integer</ins>, Length: integer, Start: string, End: string]}<br/>

Relationships:<br/>
	**Connected:** {[<ins>TrainNr: integer, Station1: string, Station2: string</ins>, Departure, Arrival]}

<br/>

#### 2. Hospital

Entities:<br/>
	**Station:** {[<ins>StatNr: integer</ins>, Name: string]}<br/>
	**Room:** {[<ins>RoomNr: integer</ins>, #Beds: integer, StatNr: integer]}<br/>
	**StationPersonell:** {[<ins>PersNr: integer</ins>, #Name: string, StatNr: integer]}<br/>
	**Doctor:** {[<ins>PersNr: integer</ins>, Area:string, Area: string, Rank: string]}<br/>
	**Caregiver:** {[<ins>PersNr: integer</ins>, Qualification: string]}<br/>
	**Patient:** {[<ins>PatientNr: integer</ins>, Name: string, Disease: string, DoctorNr: integer, RoomNr: integer, AdmissionFrom, AdmissionTo]}
