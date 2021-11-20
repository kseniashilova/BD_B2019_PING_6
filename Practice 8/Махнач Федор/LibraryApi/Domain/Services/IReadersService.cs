using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibraryApi.Domain.Dto;
using LibraryApi.Domain.Models;

namespace LibraryApi.Domain.Services
{
    public interface IReadersService
    {
        Task<Reader> GetAsync(long readerId, CancellationToken httpContextRequestAborted);

        Task<IReadOnlyCollection<Reader>> GetAllAsync(CancellationToken httpContextRequestAborted);

        Task<long> CreateAsync(CreateReaderDto newReaderDto, CancellationToken httpContextRequestAborted);

        Task UpdateAsync(Reader readerToUpdate, CancellationToken httpContextRequestAborted);

        Task DeleteAsync(long readerId, CancellationToken httpContextRequestAborted);
    }
}