import random


def gen_athletes(faker, cursor, country_ids, n):
    athletes = []
    for i in range(n):
        name = faker.name()
        birthday = faker.date_object()
        country = random.choice(country_ids)

        split_name = [it for it in name.split(' ')]
        id = split_name[-1][:5].upper() + split_name[0][:3].upper() + faker.unique.numerify('##')

        cursor.execute("INSERT INTO Players VALUES(%s, %s, %s, %s);", (name, id, country, birthday))
        athletes.append(id)

    return athletes
