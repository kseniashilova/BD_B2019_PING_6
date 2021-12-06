using Hse.DbGenerator.Options;

namespace Hse.DbGenerator.Utils
{
    public static class ConnectionString
    {
        public static string Build(PostgresOptions options)
        {
            return $"Host={options.Host};" +
                   $"Port={options.Port};" +
                   $"Database={options.Database};" +
                   $"Username={options.Username};" +
                   $"Password={options.Password}";
        }
    }
}