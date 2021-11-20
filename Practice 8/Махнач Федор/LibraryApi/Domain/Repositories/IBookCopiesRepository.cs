using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibraryApi.Domain.Models;

namespace LibraryApi.Domain.Repositories
{
    public interface IBookCopiesRepository
    {
        Task<BookCopy?> GetAsync(string isbn, int copyNumber, CancellationToken ct);

        Task<IReadOnlyCollection<BookCopy>> GetAllByIsbnAsync(string isbn, CancellationToken ct);

        Task<IReadOnlyCollection<BookCopy>> GetAllAsync(CancellationToken ct);

        Task<bool> DoesExistAsync(string isbn, int copyNumber, CancellationToken ct);

        Task CreateAsync(BookCopy bookCopy, CancellationToken ct);

        Task UpdateAsync(BookCopy bookCopy, CancellationToken ct);

        Task DeleteAsync(string isbn, int copyNumber, CancellationToken ct);
    }
}