# Task 1

Ключ – это набор атрибутов, позволяющий определить кортеж. Отношение
представляет собой множество, состоящее из кортежей. В множестве все элементы
различные, таким образом, наличие ключа необходимо, чтобы избежать появления
одинаковых кортежей в отношении.

# Task 2 (Диаграммы из дз_2)

1. 
Reader {reader number, last name, first name, address, birthday}
Book {ISBN, year, title, author, number of pages, category, publisher name, publisher
address}
Publisher {name, address}
Copy {copyNumber, position on shelf, book ISBN}
Borrowing {return date, reader number, copyNumber}

2. 
Country {country}
City {country, city}
Street {country, city, street}
House {country, city, street, house}
Apartment {country, city, street, house, apartment}

3. 
Play {teamA, teamB, arbitrator}

4. 
Parents {Dad, Mommy, child}

5. 
Connection {relationship, entity}
Attributes {entity, attributes}

# Task 3

1. 
Station {Name, #Tracks, City_Name, City_Region}
City {Region, Name}
Train {TrainNr, Length, Station_Name_Start, Station_Name_End}
Connected {TrainNr, Station_Name, Departure, Arrival}

2. 
StationPersonell {PersNr, #Name, Station_StatNr}
Doctor {Area, Rank, StationPersonell PersNr}
Caregiver {Qualification, StationPersonell PersNr}
Station {StatNr, Name}
Room {RoomNr, #Beds, Station StatNr}
Patient {PatientNr, Name, Disease, Room RoomNr, StatiomPersonell PersNr}

Works_for {StatiomPersonell PersNr, Station StatNr}
Has {Station StatNr, Room RoomNr}
Admission {from, to, Patient PatientNr, Room RoomNr}
Treats {StatiomPersonell PersNr, Patient PatientNr}