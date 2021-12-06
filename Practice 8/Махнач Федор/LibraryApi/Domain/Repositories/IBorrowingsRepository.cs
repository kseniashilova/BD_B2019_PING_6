using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibraryApi.Domain.Dto;
using LibraryApi.Domain.Models;

namespace LibraryApi.Domain.Repositories
{
    public interface IBorrowingsRepository
    {
        Task<IReadOnlyCollection<Borrowing>> SearchAsync(SearchBorrowingDto searchDto, CancellationToken ct);

        Task CreateAsync(CreateBorrowingDto newBorrowingDto, CancellationToken ct);
    }
}