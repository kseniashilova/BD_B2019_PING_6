#!/usr/bin/env python3

import psycopg2
from faker import Faker

from gen_athletes import gen_athletes
from gen_countries import gen_countries
from gen_events import gen_events
from gen_olympics import gen_olympics

DB_NAME = 'postgres'
NUMBER_OF_PARTICIPANT_COUNTRIES = 20
NUMBER_OF_ATHLETES = 20
NUMBER_OF_OLYMPICS = 20
NUMBER_OF_EVENTS = 20

faker = Faker()

conn = psycopg2.connect(f"dbname={DB_NAME}")
cursor = conn.cursor()

f = open("script.sql", "r")
script = f.read()

cursor.execute(script)


countries = gen_countries(faker, cursor, NUMBER_OF_PARTICIPANT_COUNTRIES)

players = gen_athletes(faker, cursor, countries, NUMBER_OF_ATHLETES)

olympics = gen_olympics(faker, cursor, countries, NUMBER_OF_OLYMPICS)

events = gen_events(faker, cursor, olympics, players, NUMBER_OF_OLYMPICS)

conn.commit()
cursor.close()
conn.close()
