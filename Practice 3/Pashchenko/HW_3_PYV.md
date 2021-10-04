ѕочему любое отношение в рел€ционной схеме имеет по крайней мере один ключ? 

ѕервичный ключ Ц это поле или набор полей, однозначно идентифицирующих запись в таблице. —оответственно дл€ однозначной и точной идентификации записей такой ключ необходим

\2. ѕереведите все диаграммы ER из предыдущей домашней работы в рел€ционные схемы. 

1 схема 

Book: {[ISBN: integer, Year: integer, Name: string, Author: string, Page Count: integer, Publisher Id: integer]} Publisher: {[Id: integer, Name: string, Address: string]} 

Copy: {[Copy number: integer, ISBN: integer, Shelf position: integer]} 

Reader: {[Id: integer, Surname: string, Name: string, Address: string, Birthday: Datetime]} 

Coupon: {[ Id: integer, Copy Id: integer, Reader Id: integer, Return date: Datetime]} 

Category: {[Id: integer, Name: string, Parent Id: integer]} 

Book Ц Category Link {[Id: integer, ISBN: integer, Category Id: integer]} 

2 схема 

Apartment: {[House Number: integer, Apartment Number: integer]} 

House: {[Street Name: string, House Number: integer]} 

Street: {[City Name: string, Street Name: string]}

City: {[Country Name: string, City Name: string]} 

Country: {[Country Name: string]} 

3 схема

` `Player: {[Id: integer, Name: string, Surname: string, Role: string]}

` `Team: {[Name: string]} 

Match {[Id: integer, Team A Name: string, Team B Name: string, Team A Score: integer, Team B Score: integer, Time: Datetime, Arbitrator Id: integer]} 

Arbitrator: {[Id: integer, Name: string, Surname: string]} 

4 схема 

Man: {[Id: integer, Father: string, Mother: string, Name: string, Surname: string, Patronymic: string]} Woman: {[Id: integer, Father: string, Mother: string, Name: string, Surname: string, Patronymic: string]} Have Father: {[ Id: integer, Parent Id: integer, Children Id: integer]} 

Have Mother: {[ Id: integer, Parent Id: integer, Children Id: integer]} 

\3. ѕереведите приведенные диаграммы ER в рел€ционные схемы. 

3.1 

Station: {[Name: string, #Tracks: integer]} 

City: {[Region: string, Name: string]} 

Train: {[TrainNr: integer, Length: integer]} 

Lie in: {[ StationName: string, #Tracks: integer, Region: string, CityName: string]} 

Connected: {[Name: string, TrainNr: integer, Departure: string, Arrival: string]} 

Start: {[Name: string, TrainNr: integer]} 

End: {[Name: string, TrainNr: integer]} 

3.2

StationPersonell: {[PersNr: integer, #Name: string, StatNr: integer]} 

Station: {[StatNr: integer, Name: string]} 

Caregiver: {[PersNr: integer, #Name: string, Qualification: string]} 

Doctor: {[PersNr: integer, #Name: string, Area: string, Rank: double]} 

Room: {[StatNr: integer, RoomNr: integer, #Beds: integer]} 

Patient: {[PatientNr: integer, Name: string, Disease: string, PersNr: integer]} 

Admission: {[ StatNr: integer, RoomNr: integer, PatientNr: integer, from:string, to:string]}
