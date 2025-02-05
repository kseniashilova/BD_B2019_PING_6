from sqlalchemy import Column, String, Integer, Date, Float, \
        ForeignKey, CheckConstraint
from sqlalchemy.orm import declarative_base, relationship

Base = declarative_base()


class Country(Base):
    __tablename__ = 'Countries'

    name = Column(String(40))
    country_id = Column(String(3), primary_key=True)
    area_sqkm = Column(Integer)
    population = Column(Integer)
    players = relationship('Player', backref='country')


class Olympic(Base):
    __tablename__ = 'Olympics'

    olympic_id = Column(String(7), primary_key=True)
    country_id = Column(String(3), ForeignKey('Countries.country_id'))
    city = Column(String(50))
    year = Column(Integer)
    startdate = Column(Date)
    enddate = Column(Date)


class Player(Base):
    __tablename__ = 'Players'

    name = Column(String(40))
    player_id = Column(String(10), primary_key=True)
    country_id = Column(String(3), ForeignKey('Countries.country_id'))
    birthdate = Column(Date)


class Event(Base):
    __tablename__ = 'Events'

    event_id = Column(String(7), primary_key=True)
    name = Column(String(40))
    eventtype = Column(String(20))
    olympic_id = Column(String(7), ForeignKey('Olympics.olympic_id'))
    is_team_event = Column(Integer, CheckConstraint('is_team_event in (0, 1)'))
    num_players_in_team = Column(Integer)
    result_noted_in = Column(String(100))


class Result(Base):
    __tablename__ = 'Results'

    event_id = Column(String(7), ForeignKey('Events.event_id'), primary_key=True)
    player_id = Column(String(10), ForeignKey('Players.player_id'), primary_key=True)
    medal = Column(String(7))
    result = Column(Float)
