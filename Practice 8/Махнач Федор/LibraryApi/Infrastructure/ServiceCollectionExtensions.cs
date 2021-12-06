using System;
using LibraryApi.DataAccess.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace LibraryApi.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureNpgsqlDatabase<TOptions>(this IServiceCollection services)
            where TOptions : ConnectionOptions
        {
            static string Builder(IConfigurationSection section, string path)
            {
                var connectionBuilder = new NpgsqlConnectionStringBuilder(section[path]);

                return connectionBuilder.ConnectionString;
            }

            return services.ConfigureDatabase<TOptions>(Builder);
        }

        private static IServiceCollection ConfigureDatabase<TOptions>(
            this IServiceCollection services,
            Func<IConfigurationSection, string, string> builder)
            where TOptions : ConnectionOptions
        {
            return services.Configure<TOptions>(
                settings =>
                {
                    var section = services.BuildServiceProvider()
                        .GetRequiredService<IConfiguration>()
                        .GetSection(typeof(TOptions).Name);

                    settings.MasterConnectionString = builder(section, "MasterConnectionString");

                    var readOnlyConnection = section.GetSection("ReadOnlyConnectionString").Get<string>();

                    if (!string.IsNullOrEmpty(readOnlyConnection))
                    {
                        settings.ReadOnlyConnectionString = builder(section, "ReadOnlyConnectionString");
                    }
                }
            );
        }
    }
}