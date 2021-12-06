using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibraryApi.Domain.Exceptions;
using LibraryApi.Domain.Models;
using LibraryApi.Domain.Repositories;
using LibraryApi.Domain.Services;

namespace LibraryApi.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;

        public BooksService(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<Book> GetAsync(string isbn, CancellationToken ct)
        {
            var book = await _booksRepository.GetAsync(isbn, ct);

            if (book is null)
            {
                throw new EntityNotFoundException($"Book with ISBN='{isbn}' not found.");
            }

            return book;
        }

        public Task<IReadOnlyCollection<Book>> GetAllAsync(CancellationToken ct)
        {
            return _booksRepository.GetAllAsync(ct);
        }

        public async Task CreateAsync(Book newBook, CancellationToken ct)
        {
            var bookExists = await _booksRepository.DoesExistAsync(newBook.ISBN, ct);

            if (bookExists)
            {
                throw new DomainException($"Book with ISBN='{newBook.ISBN}' already exists.");
            }

            await _booksRepository.CreateAsync(newBook, ct);
        }

        public async Task UpdateAsync(Book bookToUpdate, CancellationToken ct)
        {
            var bookExists = await _booksRepository.DoesExistAsync(bookToUpdate.ISBN, ct);

            if (!bookExists)
            {
                throw new EntityNotFoundException($"Book with ISBN='{bookToUpdate.ISBN}' does not exist.");
            }

            await _booksRepository.UpdateAsync(bookToUpdate, ct);
        }

        public async Task DeleteAsync(string isbn, CancellationToken ct)
        {
            var bookExists = await _booksRepository.DoesExistAsync(isbn, ct);

            if (!bookExists)
            {
                throw new EntityNotFoundException($"Book with ISBN='{isbn}' does not exist.");
            }

            await _booksRepository.DeleteAsync(isbn, ct);
        }
    }
}