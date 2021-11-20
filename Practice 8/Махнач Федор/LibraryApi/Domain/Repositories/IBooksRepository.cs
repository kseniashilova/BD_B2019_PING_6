using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibraryApi.Domain.Models;

namespace LibraryApi.Domain.Repositories
{
    public interface IBooksRepository
    {
        Task<Book?> GetAsync(string isbn, CancellationToken ct);

        Task<IReadOnlyCollection<Book>> GetAllAsync(CancellationToken ct);

        Task<bool> DoesExistAsync(string isbn, CancellationToken ct);

        Task CreateAsync(Book newBook, CancellationToken ct);

        Task UpdateAsync(Book bookToUpdate, CancellationToken ct);

        Task DeleteAsync(string isbn, CancellationToken ct);
    }
}