using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hse.DbGenerator.Models;
using Hse.DbGenerator.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hse.DbGenerator
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IDbContextFactory<DatabaseContext> _dbContextFactory;
        private static readonly Random Random = new();

        public Worker(ILogger<Worker> logger, IDbContextFactory<DatabaseContext> dbContextFactory)
        {
            _logger = logger;
            _dbContextFactory = dbContextFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using var db = _dbContextFactory.CreateDbContext();

            var countries = Enumerable.Range(0, 100)
                .Select(p => GenerateCountry(p))
                .ToList();

            var olympics = Enumerable.Range(0, 15)
                .Select(p => GenerateOlympic(p, countries))
                .ToList();

            await db.Countries.AddRangeAsync(countries, cancellationToken);

            foreach (var (olympic, players, events) in olympics)
            {
                await db.Olympics.AddAsync(olympic, cancellationToken);
                await db.Players.AddRangeAsync(players, cancellationToken);
                await db.Events.AddRangeAsync(events, cancellationToken);
            }

            await db.SaveChangesAsync(cancellationToken);
        }

        private static Country GenerateCountry(int index)
        {
            return new Country
            {
                Name = $"name{index}",
                CountryId = $"{index}",
                AreaSqkm = Random.Next(0, 10000),
                Population = Random.Next(0, 1000)
            };
        }

        private static (Olympic, List<Player>, List<Event>) GenerateOlympic(int index, List<Country> countries)
        {
            var year = Random.Next(1950, 2022);
            var(startDate, endDate) = GetStartEndDate(year);

            var olympic = new Olympic
            {
                City = $"city{index}",
                OlympicId = $"{index}",
                Year = year,
                StartDate = startDate,
                EndDate = endDate,
                Country = countries.GetRandomElement()
            };

            var players = Enumerable.Range(0, 150)
                .Select(p => GeneratePlayer(p, countries))
                .ToList();
            
            var events = Enumerable.Range(0, 20)
                .Select(p => GenerateEvent(p, olympic))
                .ToList();

            return (olympic, players, events);
        }

        private static Player GeneratePlayer(int index, List<Country> countries)
        {
            var player = new Player
            {
                Name = $"playerName{index}",
                PlayerId = $"{Random.Next(0, 99999)}{index}",
                Birthdate = RandomExtensions.GetRandomDate(DateTime.UnixEpoch, DateTime.Now),
                Country = countries.GetRandomElement()
            };

            return player;
        }

        private static Event GenerateEvent(int index, Olympic olympic)
        {
            var ev = new Event
            {
                Name = $"eventName{index}",
                EventId = $"{Random.Next(0, 99999)}{index}",
                EventType = $"eventType{index}",
                IsTeamEvent = 1,
                NumPlayersInTeam = Random.Next(0, 15),
                ResultNotedIn = "someResultNotedIn",
                Olympic = olympic
            };

            return ev;
        }

        private static (DateTime, DateTime) GetStartEndDate(int year)
        {
            var dates = new List<DateTime>()
            {
                RandomExtensions.GetRandomDate(new DateTime(year, 1, 1, 0, 0, 0),
                    new DateTime(year + 1, 1, 1, 0, 0, 0)),
                RandomExtensions.GetRandomDate(new DateTime(year, 1, 1, 0, 0, 0),
                    new DateTime(year + 1, 1, 1, 0, 0, 0))
            };

            var sortedDates = dates.OrderBy(p => p.Ticks).ToList();
            return (sortedDates[0], sortedDates[1]);
        }
    }
}