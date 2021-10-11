##I.	Почему любое отношение в реляционной схеме имеет по крайней мере один ключ?
В отношении не может быть двух одинаковых записей, таким образом, ключ является именно тем аттрибутом, который гарантирует уникальность записей.

##II.	Перевод диаграмм из задания 2.
1.	Библиотека:
Reader: {[<ins>ReaderID: integer</ins>, Name: string, Surname: string, Address: string, Birthday: date]}
Copy: {[<ins>CopyID: integer</ins>, Position: string, <ins>ISBN: integer</ins>]}
Book: {[<ins>ISBN: integer</ins>, Title: string, Author: string, Year: integer, PagesNum: integer, CategoryName: string, PublisherName: string, PublisherAddress: string]}
Publisher: {[<ins>Name: string, Address: string</ins>]}
Category: {[<ins>Name: string</ins>]}
Subordinate: {[<ins>subactegory: string, supercategory: string</ins>]}
Borrow: {[<ins>ReaderID: integer, CopyID: integer, ISBN:integer</ins>, ReturnDate: date]} 

2.	a) 
Apartment: {[<ins>Number:integer, EntranceNumber: integer, HouseNumber:integer, StreetName:string, СityName:string, CountryName: string</ins>]}
House: {[<ins>HouseNumber:integer, StreetName:string, СityName:string, CountryName: string</ins>]}
Street: {[<ins>StreetName:string, СityName:string, CountryName: string</ins>]}
City: {[<ins>CityName:string, CountryName:string</ins>]}	
Country: {[<ins>CountryName: string</ins>]}

б) 
Team: {[<ins>Name: string</ins>]}
Game: {[<ins>Location: string</ins>, Score: string, <ins>Date: datetime</ins>, TeamName:string, ArbitratorName:string, ArbitratorSurname: string]}
Arbitrator: {[<ins>Name: string, Surname: string</ins>]}

в) 
Person: {[Gender:string, <ins>Name: string, Surname: string</ins>, MotherName: string, MotherSurname:string, FatherName:string, FatherSurname: string, ChildName:string, ChildSurname:string]}

3.	
Attribute: {[<ins>Name: string</ins>]}
Relationship: {[<ins>Name:string</ins>, FirstEntityName:string, SecondEntityName:string, AttributeName: string]}
Entity: {[<ins>Name:string</ins>, AttributeName: string]}
Role: {[<ins>Name: string</ins>, EntityName: string]}
Key: {[<ins>Name:string</ins>, EntityName: string]}
Participate: {[<ins>EntityName: string, RelationshipName: string</ins>]}

##III.	
1. City: {[<ins>Name: string, Region: string</ins>]}
Station: {[<ins>Name: string</ins>, #Tracks: integer, CityName: string, Region: string]}
Train: {[<ins>TrainNr: integer</ins>, Length: integer, StationName: string, Departure: date, Arrival: date]}

2. Patient: {[<ins>PatientNr:integer</ins>, Name: string, Disease: string, RoomNr:integer, StatNr: integer, from: string, to: string, DoctorPersNr: integer]}
Room: {[<ins>RoomNr: integer</ins>, #Beds: integer, <ins>StatNr: integer</ins>]}
Station: {[<ins>StatNr: integer</ins>, Name: string]}
StationPersonell: {[<ins>PersNr:integer</ins>, #Name: string, StatNr: integer]}
Caregiver: {[<ins>PersNr: integer</ins>, Qualification: string]}
Doctor: {[<ins>PersNr:integer</ins>, Area: string, Rank: integer]}
