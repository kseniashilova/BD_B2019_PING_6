using Hse.DbGenerator.Configurations;
using Hse.DbGenerator.Models;
using Microsoft.EntityFrameworkCore;

namespace Hse.DbGenerator
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }

        public DbSet<Event> Events { get; set; }
        
        public DbSet<Olympic> Olympics { get; set; }
        
        public DbSet<Player> Players { get; set; }
        
        public DbSet<Result> Results { get; set; }
        
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new EventConfiguration());
            builder.ApplyConfiguration(new OlympicConfiguration());
            builder.ApplyConfiguration(new PlayerConfiguration());
            builder.ApplyConfiguration(new ResultConfiguration());
        }
    }
}