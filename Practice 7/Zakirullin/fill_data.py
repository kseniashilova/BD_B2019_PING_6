#! /usr/bin/env python3.8

from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker
import argparse
import models
import faker
import random

DEFAULT_DB_URL = 'postgresql://:qwerty@localhost:5432/practice7'

parser = argparse.ArgumentParser()
parser.add_argument('-u', '--database-url', type=str, default=DEFAULT_DB_URL)
for k, d in {
        'countries': -1,
        'olympics': 2,
        'players': -1,
        'events': -1,
        'results': -1,
        }.items():
    parser.add_argument(f'--{k}-number', type=int, default=d,
                        help=f'Number of {k}, -1 for random')
args = parser.parse_args()

engine = create_engine(args.database_url)
random.seed(42)

for k, m in {
        'countries': 195,
        'olympics': 5,
        'players': 1000,
        'events': 300,
        'results': 1000,
        }.items():
    if args.__dict__[f'{k}_number'] < 0:
        args.__dict__[f'{k}_number'] = random.randint(1, m)

f = faker.Factory.create()
models.Base.metadata.create_all(engine)
Session = sessionmaker(bind=engine)
session = Session()

sampled_countries = random.sample(faker.providers.date_time.Provider.countries,
                                  args.countries_number)

fake_countries = [models.Country(name=c['name'], country_id=c['alpha-3-code'],
    area_sqkm=random.randint(1, 20000000),
    population=random.randint(1, 20000000)) for c in sampled_countries]
session.add_all(fake_countries)

fake_olympics = [
        models.Olympic(olympic_id=f'{start_date[:4]}{city[:3].upper()}',
            country_id=random.choice(fake_countries).country_id,
            city=city,
            year=int(start_date.split('-')[0]),
            startdate=start_date,
            enddate=f.date_between_dates(start_date))
        for (start_date, city) in ((f.date(), f.city())
            for _ in range(args.olympics_number))]
session.add_all(fake_olympics)

fake_players = [models.Player(name=' '.join(name())[:40],
            player_id=f'{name[-1][:5]}{name[0][:3]}{i % 100:02}',
            country_id=random.choice(fake_countries).country_id,
            birthdate=f.date())
        for (name, i) in ((f.name().split(), i)
            for i in range(args.players_number))]
session.add_all(fake_players)

fake_events = [models.Event(event_id=f'E{i}',
            name=f.bs()[:40],
            eventtype=random.choice(('ATH', 'SWI')),
            olympic_id=random.choice(olympics).olympic_id,
            is_team_event=t,
            num_players_in_team=random.randint(1, 10) if t else -1,
            result_noted_in=f.currency()[:100])
        for (i, t) in ((i, random.choice((0, 1)))
            for i in range(args.events_number))]
session.add_all(fake_events)

fake_results = [models.Result(event_id=random.choice(fake_events).event_id,
            player_id=random.choice(fake_players).player_id,
            medal=random.choice(('GOLD', 'SILVER', 'BRONZE')),
            result=random.randint(10000) / 100)
        for _ in range(args.results_number)]
session.add_all(fake_results)

session.commit()
