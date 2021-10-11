## 1

The key is a unique reference to the "entity", so each relation must have a unique identifier, so there must be at least one key in the relational schema.

### 2.1

- Category {[<ins>id</ins>, name, parent_category_id]}

- Book:{[<ins>isbn</ins>, title, author, year, pages_count, publisher_id]}

- Copy {[<ins>id</ins>, position]}

- Publisher {[<ins>id</ins>, name, address]}

- Reader {[<ins>id</ins>, first_name, last_name, birthday, address]}

- Borrow {[<ins>id</ins>, copy_id, reader_id, return_date]}

### 2.2.1

- Flat {[<ins>id</ins>, house_id]}

- House {[<ins>id</ins>, street_id]}

- Street {[<ins>id</ins>, city_id]}

- City {[<ins>id</ins>, country_id]}

- Country {[<ins>id</ins>]}

### 2.2.2

- Referee {[<ins>id</ins>]}

- Team {[<ins>id</ins>]}

- Play {[<ins>first_team_id</ins>, <ins>second_team_id</ins>, <ins>referee_id</ins>]}


### 2.2.3

- Man {[<ins>id</ins>, father_id, mother_id]}

- Woman {[<ins>id</ins>, father_id, mother_id]}

### 2.3

- Object: {[ <ins>id</ins>]}

- Child: {[<ins>id</ins>, parent_id, child_id, name, color]}


### 3.1

- schedule {[<ins>TrainNr</ins>, <ins>StationName</ins>, arrival, departure]}
  
- Train {[<ins>TrainNr</ins>, length, start, finish]}

- City {[<ins>Name</ins>, <ins>Region</ins>]}

- Station {[<ins>Name</ins>, #Tracks, nextStation, prevStation, city]}

### 3.2

- Patient {[<ins>PatientNr</ins>, name, disease, doctorNr, roomNr, admissionFrom, admissionTo]}

- Station {[<ins>StatNr</ins>, name]}

- Caregiver {[<ins>PersNr</ins>, qaulification]}

- StationPersonell {[<ins>PersNr</ins>, #name, StatNr]}

- Doctor {[<ins>PersNr</ins>, area, rank]}


### 3.3

- Room {[<ins>RoomNr</ins>, StatNr, beds]}