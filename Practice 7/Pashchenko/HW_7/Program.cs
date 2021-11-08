using System;
using System.Collections.Generic;
using System.Linq;

namespace HW_7
{
    class Program
    {
        public static Random rnd;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static List<string> time = new List<string> { "seconds", "meters" };
        static List<string> medals = new List<string> { "GOLD", "SILVER", "BRONZE" };
        static void Main(string[] args)
        {
            bool flag = true;
            rnd = new Random();
            Console.WriteLine("Здравствуйте!\nДанная программа добавляет новые записи в базу данных.\n");
            using (AppContext db = new AppContext())
            {
                while (flag)
                {


                    Console.WriteLine("[1] Просмотр таблиц.\n[2] Добавление 1 значения в таблицу олимпиад\n[3] Добавление 1 значения в таблицу стран\n" +
                   "[4] Добавление 1 значения в таблицу событий\n[5] Добавление 1 значения в таблицу игроков\n[6] Добавление 1 значения в таблицу результатов\n" +
                   "[7] Выход из программы");
                    var command = Console.ReadKey();
                    if (db.Countries.Count() == 0)
                    {
                        CreateDatabase(db);
                    }


                    switch (command.Key)
                    {
                        case ConsoleKey.D1:
                            Console.WriteLine();
                            ShowAllTables(db);
                            break;

                        case ConsoleKey.D2:
                            AddOlympiad(db);
                            break;
                        case ConsoleKey.D3:
                            AddCountry(db);
                            break;
                        case ConsoleKey.D4:
                            AddEvent(db);
                            break;
                        case ConsoleKey.D5:
                            AddPlayer(db);
                            break;
                        case ConsoleKey.D6:
                            AddResult(db);
                            break;
                        case ConsoleKey.D7:
                            flag = false;
                            break;
                        default:
                            Console.WriteLine("Неизвестная комманда\n");
                            break;
                    }

                }
            }
        }


        private static void CreateDatabase(AppContext db)
        {
            var country = new Country();
            country.Name = "Australia";
            country.CountryId = "AUS";
            country.AreaSqkm = 7682300;
            country.Population = 21050000;
            country.Olympiads = new List<Olympiad>();

            var olympiad = new Olympiad();
            olympiad.OlympiadId = "SYD2000";
            olympiad.CountryId = country.CountryId;
            olympiad.City = "Sydney";
            olympiad.Year = 2000;
            olympiad.StartDate = new DateTime(2000, 09, 15);
            olympiad.EndDate = new DateTime(2000, 10, 01);
            olympiad.Country = country;
            olympiad.Events = new List<Event>();
            country.Olympiads.Add(olympiad);

            var event_ = new Event();
            event_.EventId = "E1";
            event_.Name = "10000m Men";
            event_.EventType = "ATH";
            event_.OlympiadId = olympiad.OlympiadId;
            event_.IsTeamEvent = 0;
            event_.NumPlayersInTeam = -1;
            event_.ResultNotedIn = "seconds";
            olympiad.Events.Add(event_);
            event_.Olympiad = olympiad;
            event_.Results = new List<Result>();

            var result = new Result();
            result.EventId = event_.EventId;
            result.Medal = "GOLD";
            result.PlayerResult = 9.87;
            event_.Results.Add(result);
            result.Event = event_;


            var player = new Player();
            player.Results = new List<Result>();
            player.CountryId = country.CountryId;
            player.PlayerId = "EGBELAAR01";
            player.Name = "Aaron Egbele";
            player.BirthDate = new DateTime(1978, 04, 21);
            player.Results.Add(result);
            result.ResultId = result.EventId + player.PlayerId;
            result.Player = player;
            result.PlayerId = player.PlayerId;

            db.Players.Add(player);
            db.Countries.Add(country);
            db.Olympiads.Add(olympiad);
            db.Events.Add(event_);
            db.Results.Add(result);
            db.SaveChanges();
        }

        private static void AddPlayer(AppContext db)
        {
            var player = new Player();
            player.Results = new List<Result>();
            int toSkip = rnd.Next(1, db.Countries.Count());
            player.CountryId = db.Countries.Skip(toSkip).Take(1).First().CountryId;
            player.PlayerId = GetName();
            player.Name = $"{GetName()} {GetName()}";
            player.BirthDate = GetRandomDate(DateTime.Today);

            db.Players.Add(player);
            db.SaveChanges();
        }
        private static void AddCountry(AppContext db)
        {
            var country = new Country();

            country.Name = GetName();
            country.CountryId = GetRandomString(3, country.Name);

            country.AreaSqkm = rnd.Next(10, 100000000);
            country.Population = rnd.Next(10000, 1000000000);
            country.Olympiads = new List<Olympiad>();

            db.Countries.Add(country);
            db.SaveChanges();
        }

        private static void AddOlympiad(AppContext db)
        {
            var olympiad = new Olympiad();
            var year = rnd.Next(1900, 2021);
            olympiad.OlympiadId = GetRandomString(3, chars) + year;
            olympiad.City = GetName();
            olympiad.Year = year;
            olympiad.StartDate = GetRandomDate(DateTime.Today);
            olympiad.EndDate = GetRandomDate(olympiad.StartDate);
            if (db.Countries.Count() != 0)
            {
                int toSkip = rnd.Next(db.Countries.Count());
                var country = db.Countries.Skip(toSkip).Take(1).First();
                olympiad.Country = country;
                olympiad.CountryId = country.CountryId;
                if (country.Olympiads == null)
                    country.Olympiads = new List<Olympiad>();
                country.Olympiads.Add(olympiad);
            }

            db.Olympiads.Add(olympiad);
            db.SaveChangesAsync();
        }
        private static void AddEvent(AppContext db)
        {
            var event_ = new Event();
            event_.EventId = "E" + (db.Events.Count() + 1);
            event_.Name = GetName();
            event_.EventType = GetRandomString(3, chars);
            int toSkip = rnd.Next(db.Olympiads.Count());
            var olympiad = db.Olympiads.Skip(toSkip).Take(1).First();
            event_.OlympiadId = olympiad.OlympiadId;
            event_.IsTeamEvent = rnd.Next(1);
            event_.NumPlayersInTeam = event_.IsTeamEvent > 0 ? rnd.Next(2, 20) : -1;
            event_.ResultNotedIn = time[rnd.Next(2)];
            event_.Olympiad = olympiad;
            event_.Results = new List<Result>();
            if (olympiad.Events == null)
                olympiad.Events = new List<Event>();
            olympiad.Events.Add(event_);

            db.Events.Add(event_);
            db.SaveChanges();
        }
        private static void AddResult(AppContext db)
        {
            var result = new Result();
            int toSkipEvent = rnd.Next(db.Events.Count());
            int toSkipPlayer = rnd.Next(db.Players.Count());
            var event_ = db.Events.Skip(toSkipEvent).Take(1).First();
            var player = db.Players.Skip(toSkipPlayer).Take(1).First();
            result.Event = event_;
            result.EventId = event_.EventId;
            result.Player = player;
            result.PlayerId = player.PlayerId;
            result.Medal = medals[rnd.Next(3)];
            result.PlayerResult = rnd.NextDouble() * rnd.Next(40);
            result.ResultId = "R" + (db.Results.Count() + 1);
            if (event_.Results.Count == 0)
                event_.Results = new List<Result>();
            event_.Results.Add(result);
            player.Results.Add(result);

            db.Results.Add(result);
            db.SaveChanges();
        }
        private static void ShowAllTables(AppContext db)
        {
            var players = db.Players.ToList();
            Console.WriteLine("Список игроков:\n|PlayerId|Name|CountryId|BirthDate|");
            foreach (Player p in players)
            {
                Console.WriteLine($"{p.PlayerId}. {p.Name} {p.CountryId} {p.BirthDate.ToString($"dd/MM/yyyy")}");
            }

            var olympiads = db.Olympiads.ToList();
            Console.WriteLine("Список олимпиад:\n|OlympiadId|CountryId|City|Year|StartDate|EndDate|");
            foreach (Olympiad o in olympiads)
            {
                Console.WriteLine($"{o.OlympiadId}. {o.CountryId} {o.City} {o.Year} {o.StartDate.ToString($"dd/MM/yyyy")} {o.EndDate.ToString($"dd/MM/yyyy")}");
            }

            var countries = db.Countries.ToList();
            Console.WriteLine("Список стран:\n|CountryId|Name|AreaSqkm|Population|");
            foreach (Country c in countries)
            {
                Console.WriteLine($"{c.CountryId}. {c.Name} {c.AreaSqkm} {c.Population} ");
            }

            var events = db.Events.ToList();
            Console.WriteLine("Список событий:\n|EventId|Name|EventType|OlympiadId|IsTeamEvent|NumPlayersInTeam|ResultNotedIn|");
            foreach (Event e in events)
            {
                Console.WriteLine($"{e.EventId} {e.Name} {e.EventType} {e.OlympiadId} {e.IsTeamEvent} {e.NumPlayersInTeam} {e.ResultNotedIn}");
            }

            var results = db.Results.ToList();
            Console.WriteLine("Список результатов:\n|EventId|PlayerId|Medal|PlayerResult|");
            foreach (Result r in results)
            {
                Console.WriteLine($"{r.EventId} {r.PlayerId} {r.Medal} {r.PlayerResult}");
            }
        }
        public static string GetRandomString(int len, string source)
        {
            return new string(Enumerable.Repeat(source, len)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
        public static string GetName()
        {
            var nameLength = rnd.Next(5, 10);
            var name = GetRandomString(1, chars).ToUpper();
            name += GetRandomString(nameLength, chars);
            return name;
        }
        public static DateTime GetRandomDate(DateTime date)
        {
            DateTime start = new DateTime(1900, 1, 1);
            int range = (date - start).Days;
            return start.AddDays(rnd.Next(range));
        }
    }
}
