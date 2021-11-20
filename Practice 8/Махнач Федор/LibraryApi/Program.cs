using FluentMigrator.Runner;
using LibraryApi.Migrations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LibraryApi
{
    public static class Program
    {
        // Definitely not a good thing
        private const string ConnectionString =
            "Host=localhost;Port=5432;ApplicationName=library-api;Database=library-db;Pooling=True;Minimum Pool Size=1;Maximum Pool Size=10;UserId=library-db-user;Password=123456;Include Error Detail=true;";

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            MigrateDatabase();

            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }

        private static void MigrateDatabase()
        {
            var serviceProvider = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(ConnectionString)
                    .ScanIn(typeof(CreateTableBook).Assembly).For.Migrations())
                .BuildServiceProvider(false);

            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Use -1 to rollback all migrations
            //runner.MigrateDown(-1);

            runner.MigrateUp();
        }
    }
}