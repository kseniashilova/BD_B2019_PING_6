// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LibraryApi.Domain.Models
{
    public class BookCopy
    {
        public string ISBN { get; init; } = string.Empty;

        public int CopyNumber { get; init; }

        public string? ShelfPosition { get; set; }
    }
}