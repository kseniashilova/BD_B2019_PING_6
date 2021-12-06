using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using LibraryApi.DataAccess.Options;
using Microsoft.Extensions.Options;
using Npgsql;

namespace LibraryApi.DataAccess.ConnectionFactories
{
    public class LibraryDbConnectionFactory : ConnectionFactory, ILibraryDbConnectionFactory
    {
        public LibraryDbConnectionFactory(IOptions<LibraryDbConnectionOptions> options)
            : base(options.Value.ReadOnlyConnectionString, options.Value.MasterConnectionString)
        {
        }

        protected override async Task<DbConnection> OpenConnectionAsync(string connectionString, CancellationToken ct)
        {
            var connection = new NpgsqlConnection(connectionString);

            await connection.OpenAsync(ct);

            return connection;
        }
    }
}