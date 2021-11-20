using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibraryApi.Domain.Exceptions;
using LibraryApi.Domain.Models;
using LibraryApi.Domain.Repositories;
using LibraryApi.Domain.Services;

namespace LibraryApi.Services
{
    public class BookCopiesService : IBookCopiesService
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IBookCopiesRepository _bookCopiesRepository;

        public BookCopiesService(IBooksRepository booksRepository, IBookCopiesRepository bookCopiesRepository)
        {
            _booksRepository = booksRepository;
            _bookCopiesRepository = bookCopiesRepository;
        }

        public async Task<BookCopy> GetAsync(string isbn, int copyNumber, CancellationToken ct)
        {
            var book = await _bookCopiesRepository.GetAsync(isbn, copyNumber, ct);

            if (book is null)
            {
                throw new EntityNotFoundException($"Book copy with ISBN='{isbn}', CopyNr='{copyNumber}' not found.");
            }

            return book;
        }

        public Task<IReadOnlyCollection<BookCopy>> GetAllByIsbnAsync(string isbn, CancellationToken ct)
        {
            return _bookCopiesRepository.GetAllByIsbnAsync(isbn, ct);
        }

        public Task<IReadOnlyCollection<BookCopy>> GetAllAsync(CancellationToken ct)
        {
            return _bookCopiesRepository.GetAllAsync(ct);
        }

        public async Task CreateAsync(BookCopy bookCopy, CancellationToken ct)
        {
            // Creation of copy requires existence of book with such ISBN
            var bookExists = await _booksRepository.DoesExistAsync(bookCopy.ISBN, ct);
            if (!bookExists)
            {
                throw new EntityNotFoundException($"Book [ISBN='{bookCopy.ISBN}'] does not exist.");
            }

            var copyExists = await _bookCopiesRepository.DoesExistAsync(bookCopy.ISBN, bookCopy.CopyNumber, ct);
            if (copyExists)
            {
                throw new DomainException($"Book copy [ISBN='{bookCopy.ISBN}', CopyNr='{bookCopy.CopyNumber}'] already exists.");
            }

            await _bookCopiesRepository.CreateAsync(bookCopy, ct);
        }

        public async Task UpdateAsync(BookCopy bookCopy, CancellationToken ct)
        {
            var copyExists = await _bookCopiesRepository.DoesExistAsync(bookCopy.ISBN, bookCopy.CopyNumber, ct);

            if (!copyExists)
            {
                throw new EntityNotFoundException($"Book copy [ISBN='{bookCopy.ISBN}', CopyNr='{bookCopy.CopyNumber}'] does not exist.");
            }

            await _bookCopiesRepository.UpdateAsync(bookCopy, ct);
        }

        public async Task DeleteAsync(string isbn, int copyNumber, CancellationToken ct)
        {
            var copyExists = await _bookCopiesRepository.DoesExistAsync(isbn, copyNumber, ct);

            if (!copyExists)
            {
                throw new EntityNotFoundException($"Book copy [ISBN='{isbn}', CopyNr='{copyNumber}'] does not exist.");
            }

            await _bookCopiesRepository.DeleteAsync(isbn, copyNumber, ct);
        }
    }
}