using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraryApi.Domain.Dto;
using LibraryApi.Domain.Exceptions;
using LibraryApi.Domain.Models;
using LibraryApi.Domain.Services;

namespace LibraryApi.Services
{
    public class GeneratorService : IGeneratorService
    {
        private static readonly Random Random = new();

        private readonly IFakerService _fakerService;
        private readonly IBooksService _booksService;
        private readonly IBookCopiesService _bookCopiesService;
        private readonly IReadersService _readersService;
        private readonly IBorrowingsService _borrowingsService;

        public GeneratorService(
            IFakerService fakerService,
            IBooksService booksService,
            IBookCopiesService bookCopiesService,
            IReadersService readersService,
            IBorrowingsService borrowingsService)
        {
            _fakerService = fakerService;
            _booksService = booksService;
            _bookCopiesService = bookCopiesService;
            _readersService = readersService;
            _borrowingsService = borrowingsService;
        }

        public async Task<IReadOnlyCollection<Book>> GenerateBooksAsync(int count, CancellationToken ct)
        {
            var generatedBooks = new List<Book>();

            while (generatedBooks.Count < count)
            {
                var newBook = _fakerService.GetBook();
                try
                {
                    await _booksService.CreateAsync(newBook, ct);
                    generatedBooks.Add(newBook);
                }
                catch (DomainException)
                {
                    // ignored
                }
            }

            return generatedBooks;
        }

        public async Task<IReadOnlyCollection<BookCopy>> GenerateCopiesAsync(int count, CancellationToken ct)
        {
            var allBooks = await _booksService.GetAllAsync(ct);
            if (allBooks.Count == 0)
            {
                throw new DomainException("No books found");
            }

            var allIsbns = allBooks.Select(x => x.ISBN).ToArray();

            var result = new List<BookCopy>();
            while (result.Count < count)
            {
                var randomIsbn = RandomElement(allIsbns);
                var newBookCopy = _fakerService.GetBookCopy(randomIsbn);
                try
                {
                    await _bookCopiesService.CreateAsync(newBookCopy, ct);
                    result.Add(newBookCopy);
                }
                catch (DomainException)
                {
                    // ignored
                }
            }

            return result;
        }

        public async Task<IReadOnlyCollection<Reader>> GenerateReadersAsync(int count, CancellationToken ct)
        {
            var readers = new List<Reader>();

            for (int i = 0; i < count; i++)
            {
                var newReaderDto = _fakerService.GetReader();
                var newReaderId = await _readersService.CreateAsync(newReaderDto, ct);
                readers.Add(ToReader(newReaderId, newReaderDto));
            }

            return readers;
        }

        public async Task<IReadOnlyCollection<Borrowing>> GenerateBorrowingsAsync(int count, CancellationToken ct)
        {
            var allReaders = await _readersService.GetAllAsync(ct);
            var allCopies = await _bookCopiesService.GetAllAsync(ct);
            var allBooks = await _booksService.GetAllAsync(ct);

            var allReadersList = allReaders.ToList();
            var allCopiesList = allCopies.ToList();
            var allBooksDict = allBooks.ToDictionary(x => x.ISBN);

            var borrowings = new List<Borrowing>();

            for (int i = 0; i < count; i++)
            {
                var reader = RandomElement(allReadersList);
                var copy = RandomElement(allCopiesList);
                var book = allBooksDict[copy.ISBN];

                var newBorrowing = new Borrowing
                {
                    ReaderId = reader.Id,
                    LastName = reader.LastName,
                    FirstName = reader.FirstName,
                    ISBN = book.ISBN,
                    Title = book.Title,
                    Author = book.Author,
                    CopyNumber = copy.CopyNumber,
                    ReturnDate = RandomReturnDate()
                };

                await _borrowingsService.CreateAsync(ToCreateDto(newBorrowing), ct);
                borrowings.Add(newBorrowing);
            }

            return borrowings;
        }

        private static Reader ToReader(long readerId, CreateReaderDto dto)
        {
            return new Reader
            {
                Id = readerId,
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                Address = dto.Address,
                BirthDate = dto.BirthDate
            };
        }

        private static CreateBorrowingDto ToCreateDto(Borrowing borrowing)
        {
            return new CreateBorrowingDto
            {
                ReaderId = borrowing.ReaderId,
                ISBN = borrowing.ISBN,
                CopyNumber = borrowing.CopyNumber,
                ReturnDate = borrowing.ReturnDate
            };
        }

        private static T RandomElement<T>(IReadOnlyList<T> array) => array[Random.Next(array.Count)];

        private static DateTime RandomReturnDate()
        {
            var dateFrom = new DateTime(2010, 1, 1);
            var dateTo = new DateTime(2022, 6, 1);
            var days = (dateTo - dateFrom).Days;

            return dateFrom.AddDays(Random.Next(days));
        }
    }
}