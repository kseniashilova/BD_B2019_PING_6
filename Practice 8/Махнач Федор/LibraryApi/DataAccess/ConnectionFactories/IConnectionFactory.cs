using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApi.DataAccess.ConnectionFactories
{
    public interface IConnectionFactory
    {
        Task<DbConnection> GetMasterConnectionAsync(CancellationToken ct);
        
        Task<DbConnection> GetRandomConnectionAsync(CancellationToken ct);
    }
}