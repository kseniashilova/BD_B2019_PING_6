using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibraryApi.Domain.Models;

namespace LibraryApi.Domain.Services
{
    public interface IGeneratorService
    {
        Task<IReadOnlyCollection<Book>> GenerateBooksAsync(int count, CancellationToken ct);

        Task<IReadOnlyCollection<BookCopy>> GenerateCopiesAsync(int count, CancellationToken ct);

        Task<IReadOnlyCollection<Reader>> GenerateReadersAsync(int count, CancellationToken ct);

        Task<IReadOnlyCollection<Borrowing>> GenerateBorrowingsAsync(int count, CancellationToken ct);
    }
}