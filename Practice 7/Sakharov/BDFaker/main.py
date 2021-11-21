import psycopg2
import configparser
from faker import Faker

from generations import create_config, players_insert, countries_insert, olympics_insert, events_insert, results_insert

CONFIG_FILE_NAME = "config.ini"
# create_config(CONFIG_FILE_NAME)

config = configparser.ConfigParser()
config.read(CONFIG_FILE_NAME)
DATABASE = config['SOURCE']['database']
COUNTRIES_CNT = int(config['COUNTS']['counties_cnt'])
PLAYERS_CNT = int(config['COUNTS']['counties_cnt'])
OLYMPICS_CNT = int(config['COUNTS']['counties_cnt'])
EVENTS_CNT = int(config['COUNTS']['counties_cnt'])

faker = Faker()
conn = psycopg2.connect(f"dbname={DATABASE}")
cur = conn.cursor()

if bool(config['OPTIONS']['drop']):
    f = open("drop.sql", "r")
    drop = f.read()
    cur.execute(drop)
    conn.commit()

f = open("create_tables.sql", "r")
create_tables = f.read()
cur.execute(create_tables)
conn.commit()

counties = countries_insert(faker, cur, conn, COUNTRIES_CNT)
players = players_insert(faker, cur, conn, counties, PLAYERS_CNT)
olympics = olympics_insert(faker, cur, conn, counties, OLYMPICS_CNT)
events = events_insert(faker, cur, conn, olympics, players, EVENTS_CNT)
results_insert(faker, cur, conn, events, players)
