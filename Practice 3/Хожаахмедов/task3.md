# Task 1

Ключ – это набор атрибутов, позволяющий определить кортеж. Отношение
представляет собой множество, состоящее из кортежей. В множестве все элементы
различные, таким образом, наличие ключа необходимо, чтобы избежать появления
одинаковых кортежей в отношении.

# Task 2 (Диаграммы из дз_2)

1. 
Reader {reader number, last name, first name, address, birthday}<br />
Book {ISBN, year, title, author, number of pages, category, publisher name, publisher<br />
address}
Publisher {name, address}<br />
Copy {copyNumber, position on shelf, book ISBN}<br />
Borrowing {return date, reader number, copyNumber}

2. 
Country {country}<br />
City {country, city}<br />
Street {country, city, street}<br />
House {country, city, street, house}<br />
Apartment {country, city, street, house, apartment}

3. 
Play {teamA, teamB, arbitrator}

4. 
Parents {Dad, Mommy, child}

5. 
Connection {relationship, entity}<br />
Attributes {entity, attributes}

# Task 3

1. 
Station {Name, #Tracks, City_Name, City_Region}<br />
City {Region, Name}<br />
Train {TrainNr, Length, Station_Name_Start, Station_Name_End}<br />
Connected {TrainNr, Station_Name, Departure, Arrival}

2. 
StationPersonell {PersNr, #Name, Station_StatNr}<br />
Doctor {Area, Rank, StationPersonell PersNr}<br />
Caregiver {Qualification, StationPersonell PersNr}<br />
Station {StatNr, Name}<br />
Room {RoomNr, #Beds, Station StatNr}<br />
Patient {PatientNr, Name, Disease, Room RoomNr, StatiomPersonell PersNr}

Works_for {StatiomPersonell PersNr, Station StatNr}<br />
Has {Station StatNr, Room RoomNr}<br />
Admission {from, to, Patient PatientNr, Room RoomNr}<br />
Treats {StatiomPersonell PersNr, Patient PatientNr}
