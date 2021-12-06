using Hse.DbGenerator.Options;
using Hse.DbGenerator.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Hse.DbGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var dbOptions = hostContext.Configuration
                        .GetSection("Postgres")
                        .Get<PostgresOptions>();

                    var connectionString = ConnectionString.Build(dbOptions);
                    services.AddDbContextFactory<DatabaseContext>(p =>
                        p.UseNpgsql(connectionString));

                    services.AddHostedService<Worker>();
                });
    }
}