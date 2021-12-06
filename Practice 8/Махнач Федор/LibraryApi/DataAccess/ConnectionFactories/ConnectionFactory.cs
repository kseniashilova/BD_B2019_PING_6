using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApi.DataAccess.ConnectionFactories
{
    public abstract class ConnectionFactory : IConnectionFactory
    {
        private string MasterConnectionString { get; }

        private string RandomConnectionString { get; }


        protected ConnectionFactory(string randomConnectionString, string masterConnectionString)
        {
            RandomConnectionString = randomConnectionString;
            MasterConnectionString = masterConnectionString;
        }


        public Task<DbConnection> GetMasterConnectionAsync(CancellationToken ct)
        {
            return OpenConnectionAsync(MasterConnectionString, ct);
        }

        public Task<DbConnection> GetRandomConnectionAsync(CancellationToken ct)
        {
            return OpenConnectionAsync(RandomConnectionString, ct);
        }

        protected abstract Task<DbConnection> OpenConnectionAsync(string connectionString, CancellationToken ct);
    }
}