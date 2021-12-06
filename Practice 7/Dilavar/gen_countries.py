import random
import string

def gen_countries(faker, cursor, n):
    countries = []

    for i in range(n):
        country_id = (''.join([random.choice(string.ascii_letters) for _ in range(3)])).upper()
        country_name = ''
        while not (1 < len(country_name) < 40):
            country_name = faker.country()
        area_sqkm = random.randint(10, 1000000)
        population = random.randint(10, 1000000)

        cursor.execute("INSERT INTO Countries VALUES(%s, %s, %s, %s);",
                       (country_name, country_id, area_sqkm, population))
        countries.append(country_id)

    return countries
