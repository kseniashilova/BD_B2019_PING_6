using System.Collections.Generic;
using System.Linq;
using LibraryApi.Domain.Dto;
using LibraryApi.Domain.Models;
using LibraryApi.Domain.Services;

namespace LibraryApi.Services
{
    public class FakerService : IFakerService
    {
        private readonly Dictionary<string, int> _bookCopyNumbers = new();

        public Book GetBook()
        {
            return new Book
            {
                ISBN = Faker.Identification.SocialSecurityNumber(), // well, doesn't matter much
                Title = Faker.Lorem.Words(1).First(),
                Author = Faker.Name.FullName(),
                PagesNum = Faker.RandomNumber.Next(10, 3000),
                PublishYear = Faker.RandomNumber.Next(1900, 2021),
                PublisherName = Faker.Company.Name()
            };
        }

        public BookCopy GetBookCopy(string isbn)
        {
            if (_bookCopyNumbers.TryGetValue(isbn, out var copyNum))
            {
                _bookCopyNumbers[isbn]++;
            }
            else
            {
                _bookCopyNumbers[isbn] = copyNum = 1;
            }

            return new BookCopy
            {
                ISBN = isbn,
                CopyNumber = copyNum,
                ShelfPosition = $"{Faker.RandomNumber.Next(100)}:{Faker.RandomNumber.Next(100)}"
            };
        }

        public CreateReaderDto GetReader()
        {
            return new CreateReaderDto
            {
                FirstName = Faker.Name.First(),
                LastName = Faker.Name.Last(),
                Address = Faker.Address.City(),
                BirthDate = Faker.Identification.DateOfBirth()
            };
        }
    }
}