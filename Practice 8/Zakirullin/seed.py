#! ./source_py_script.sh

from django_seed import Seed
from librarysystem.mainapp.models import Reader, Book, Publisher, Category, \
        Copy, Borrowing, BookCat
import random

random.seed(42)
seeder = Seed.seeder()

seeder.add_entity(Category, 10, {
    'category_name': lambda x: seeder.faker.word(),
})
seeder.add_entity(Publisher, 10, {
    'pub_name': lambda x: ' '.join(seeder.faker.words(nb=2)),
})
seeder.add_entity(Reader, 10)
seeder.add_entity(Book, 10, {
    'isbn': lambda x: seeder.faker.isbn13(),
})
seeder.add_entity(BookCat, 10)
seeder.add_entity(Copy, 10)
seeder.add_entity(Borrowing, 10)

inserted_pks = seeder.execute()
