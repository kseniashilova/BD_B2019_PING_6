1. There are 3 types of relations: one to one, one to many, many to many
One to one scheme the key supposed to be any of keys for two entities, which are participate in 
the relationship.
One to many scheme key supposed to be the key of the entity, which we use to build 
relationships with others.
Many to many scheme key supposed to compose keys of all existed related entities. All 
described keys could unify each combinations of relationships.

2. We have a relation with several records that are stored in similar fields. Difference is in
CategoryName field. There are as many entries, as there are categories of the book. 
{BookTitle, Book Year, BookAuthor, __BookISBN__, BookNumberOfPage}
{ParentCategoryName}
{__CategoryName__, __CopyNumber__, CopyPosition}
{__ReaderNumber__, ReaderLastName, ReaderFirstName, ReaderBirthday, ReaderAddress}
{ReturnDate}

3. READER {ReaderNumber, ReaderFirstName, ReaderLastName, ReaderBirthday, ReaderAddress}
BOOK {Year, Title, Author, ISBN, NumberOfPages}
PUBLISHER{Address, Name}
Category{CategoryName}
COPY {PositionOnBookshelf, CopyNumber}
BORROWS {ReaderNumber, ISBN, CopyNumber, ReturnDate}
BOOK_ASSIGNS_TO_CATEGORY {Name}

* APARTMENT{HOUSE}
HOUSE{STREET}
STREET{CITY}

* ARBITRATOR {ArbitratorId}
TEAM {TeamId}

* MAN {PeopleId, MotherId, FatherId}
WOMEN {PeopleId, MotherId, FatherId}