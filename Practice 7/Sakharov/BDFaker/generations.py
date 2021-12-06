import configparser
import random
from datetime import timedelta


def create_config(config_name):
    rnd = random.Random()
    config = configparser.ConfigParser()
    config['SOURCE'] = {'database': 'test'}
    config['OPTIONS'] = {'drop': 'True'}
    config['COUNTS'] = {'counties_cnt': f'{rnd.randint(20, 50)}',
                        'players_cnt': f'{rnd.randint(20, 50)}',
                        'olympics_cnt': f'{rnd.randint(20, 50)}',
                        'events_cnt': f'{rnd.randint(20, 50)}'}
    with open(config_name, 'w') as configfile:
        config.write(configfile)


def countries_insert(faker, cursor, conn, n):
    ids = []
    for i in range(n):
        country_name = ''
        while not (1 < len(country_name) < 40):
            country_name = faker.country()
        country_id = country_name[:3].upper() + faker.unique.numerify('###')
        ids.append(country_id)
        area_sqkm = random.randint(100, 320000)
        population = random.randint(100000, 40000000)

        cursor.execute("INSERT INTO Countries VALUES(%s, %s, %s, %s);",
                       (country_id, country_name, area_sqkm, population))
    conn.commit()
    return ids


def players_insert(faker, cursor, conn, country_ids, n):
    ids = []
    for i in range(n):
        name = faker.name()
        birthday = faker.date_object()
        sex = random.choice(["male", "female"])
        country = random.choice(country_ids)
        id = faker.unique.numerify('###') + '/' + name.split(' ')[-1][:3].upper() + name.split(' ')[0][:3].upper()
        ids.append(id)

        cursor.execute("INSERT INTO Players VALUES(%s, %s, %s, %s, %s);", (id, name, sex, country, birthday))
    conn.commit()
    return ids


def olympics_insert(faker, cursor, conn, country_ids, n):
    ids = []
    for i in range(n):
        country = random.choice(country_ids)
        city = faker.city()
        date = faker.date_object()
        olympic_id = f"{city[:3].upper()}{date.year}#{faker.unique.numerify('##')}"
        ids.append(olympic_id)

        cursor.execute("INSERT INTO Olympics VALUES (%s, %s, %s, %s, %s, %s);",
                       (olympic_id, country, city, date.year, date, date + timedelta(days=15)))
    conn.commit()
    return ids


def events_insert(faker, cursor, conn, olympics, players, n):
    events = []
    for i in range(n):

        if random.random() > 0.3:
            id = f'E{i}'
            e_type = random.choice(['ATH', 'SWI'])
            olympic = random.choice(olympics)
            scores = random.choice(['meters', 'seconds', 'points'])

            name = random.choice(["Men's", "Women's"])
            name += random.choice([" skiing", " snowboarding", " shooting", " sprint"])

            cursor.execute("insert into Events values(%s, %s, %s, %s, 0, '', %s);", (id, name, e_type, olympic, scores))
            events.append(id)
        else:
            id = f'E{i}'
            e_type = random.choice(['ATH', 'SWI'])
            olympic = random.choice(olympics)
            scores = 'points'

            name = random.choice(["Volleyball", "Basketball", "Football", "Polo"])

            teams = ''
            team1 = random.choices(players, k=6)
            team2 = random.choices(players, k=6)
            for j in range(6):
                teams += team1[j] + ';'
            teams += '|'
            for j in range(6):
                teams += team2[j] + ';'

            cursor.execute("insert into Events values(%s, %s, %s, %s, 1, %s, %s);",
                           (id, name, e_type, olympic, teams, scores))
            events.append(id)

    conn.commit()
    return events


def results_insert(faker, cursor, conn, events, players):
    for id in events:
        scores = sorted([faker.pyfloat(positive=True, max_value=100) for _ in range(3)])
        for score, medal in zip(scores, ["GOLD", "SILVER", "BRONZE"]):
            player = random.choice(players)
            cursor.execute("insert into Results values(%s, %s, %s, %s);", (id, player, medal, score))
    conn.commit()
