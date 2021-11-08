using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW_7
{
    class AppContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Olympiad> Olympiads { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Result> Results { get; set; }

        public AppContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=my_postgres;Username=postgres;Password=admin");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().HasKey(p => p.PlayerId);
            modelBuilder.Entity<Country>().HasKey(p => p.CountryId);
            modelBuilder.Entity<Olympiad>().HasKey(p => p.OlympiadId);
            modelBuilder.Entity<Event>().HasKey(p => p.EventId);
            modelBuilder.Entity<Result>().HasKey(r => r.ResultId);

            modelBuilder.Entity<Country>().HasMany(o => o.Olympiads).WithOne(c => c.Country).IsRequired();

            modelBuilder.Entity<Player>().HasMany(o => o.Results).WithOne(c => c.Player).IsRequired();
            modelBuilder.Entity<Event>().HasMany(o => o.Results).WithOne(c => c.Event).IsRequired();

            modelBuilder.Entity<Olympiad>().HasMany(o => o.Events).WithOne(c => c.Olympiad).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
