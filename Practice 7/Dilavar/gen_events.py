import random


def gen_events(faker, cursor, olympics, athletes, n):
    events = []
    for i in range(n):
        id = f'E{i}'
        e_type = random.choice(['ATH', 'SWI'])
        olympic = random.choice(olympics)
        scores = random.choice(['meters', 'seconds', 'points'])

        name = random.choice(["Men's", "Women's"])
        name += random.choice(
            [" 100m", " 200m", " 400m", " 1km"])
        name += random.choice(
            [" Freestyle", " Hurdles", " Jump"])
        cursor.execute("insert into Events values(%s, %s, %s, %s, 0, -1, %s);", (id, name, e_type, olympic, scores))
        events.append(id)

    for id in events:
        scores = sorted([faker.pyfloat(positive=True, max_value=100) for _ in range(3)])
        for score, medal in zip(scores, ["GOLD", "SILVER", "BRONZE"]):
            athlete = random.choice(athletes)
            cursor.execute("insert into Results values(%s, %s, %s, %s);", (id, athlete, medal, score))
    return events
