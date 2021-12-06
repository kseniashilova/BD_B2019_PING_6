using LibraryApi.Domain.Dto;
using LibraryApi.Domain.Models;

namespace LibraryApi.Domain.Services
{
    public interface IFakerService
    {
        Book GetBook();

        BookCopy GetBookCopy(string isbn);

        CreateReaderDto GetReader();
    }
}