using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibraryApi.Domain.Dto;
using LibraryApi.Domain.Exceptions;
using LibraryApi.Domain.Models;
using LibraryApi.Domain.Repositories;
using LibraryApi.Domain.Services;

namespace LibraryApi.Services
{
    public class BorrowingsService : IBorrowingsService
    {
        private readonly IBorrowingsRepository _borrowingsRepository;
        private readonly IReadersRepository _readersRepository;
        private readonly IBookCopiesRepository _copiesRepository;

        public BorrowingsService(
            IBorrowingsRepository borrowingsRepository,
            IReadersRepository readersRepository,
            IBookCopiesRepository copiesRepository)
        {
            _borrowingsRepository = borrowingsRepository;
            _readersRepository = readersRepository;
            _copiesRepository = copiesRepository;
        }

        public Task<IReadOnlyCollection<Borrowing>> SearchAsync(SearchBorrowingDto request, CancellationToken ct)
        {
            return _borrowingsRepository.SearchAsync(request, ct);
        }

        public async Task CreateAsync(CreateBorrowingDto request, CancellationToken ct)
        {
            var readerExists = await _readersRepository.DoesExistAsync(request.ReaderId, ct);
            if (!readerExists)
            {
                throw new EntityNotFoundException($"Reader with ID='{request.ReaderId}' not found.");
            }

            var copyExists = await _copiesRepository.DoesExistAsync(request.ISBN, request.CopyNumber, ct);
            if (!copyExists)
            {
                throw new EntityNotFoundException($"Book copy with ISBN='{request.ISBN}', CopyNr='{request.CopyNumber}' not found.");
            }

            await _borrowingsRepository.CreateAsync(request, ct);
        }
    }
}