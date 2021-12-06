using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibraryApi.Domain.Dto;
using LibraryApi.Domain.Models;

namespace LibraryApi.Domain.Services
{
    public interface IBorrowingsService
    {
        Task<IReadOnlyCollection<Borrowing>> SearchAsync(SearchBorrowingDto request, CancellationToken ct);

        Task CreateAsync(CreateBorrowingDto request, CancellationToken ct);
    }
}