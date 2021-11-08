from datetime import timedelta
import random


def gen_olympics(faker, cursor, country_ids, n):
    olympics = []
    for i in range(n):
        country = random.choice(country_ids)
        city = faker.city()
        date = faker.date_object()
        id = f"{city[:3].upper()}{date.year}"

        cursor.execute("INSERT INTO Olympics VALUES (%s, %s, %s, %s, %s, %s);",
                       (id, country, city, date.year, date, date + timedelta(days=15)))
        olympics.append(id)

    return olympics
