using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibraryApi.Domain.Dto;
using LibraryApi.Domain.Models;

namespace LibraryApi.Domain.Repositories
{
    public interface IReadersRepository
    {
        Task<Reader?> GetAsync(long readerId, CancellationToken ct);

        Task<IReadOnlyCollection<Reader>> GetAllAsync(CancellationToken ct);

        Task<bool> DoesExistAsync(long readerId, CancellationToken ct);

        Task<long> CreateAsync(CreateReaderDto newReaderDto, CancellationToken ct);

        Task UpdateAsync(Reader readerToUpdate, CancellationToken ct);

        Task DeleteAsync(long readerId, CancellationToken ct);
    }
}