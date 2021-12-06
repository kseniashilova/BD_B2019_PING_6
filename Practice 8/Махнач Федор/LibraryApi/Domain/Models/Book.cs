// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LibraryApi.Domain.Models
{
    public class Book
    {
        public string ISBN { get; init; } = string.Empty;

        public string Title { get; init; } = string.Empty;

        public string? Author { get; init; }

        public int? PagesNum { get; init; }

        public int? PublishYear { get; init; }

        public string? PublisherName { get; init; }
    }
}