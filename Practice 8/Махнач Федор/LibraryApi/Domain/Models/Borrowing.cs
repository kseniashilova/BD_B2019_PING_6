using System;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace LibraryApi.Domain.Models
{
    public class Borrowing
    {
        public long ReaderId { get; init; }

        public string LastName { get; init; } = string.Empty;

        public string FirstName { get; init; } = string.Empty;

        public string ISBN { get; init; } = string.Empty;
        
        public string Title { get; init; } = string.Empty;

        public string? Author { get; init; }

        public int CopyNumber { get; init; }
        
        public DateTime ReturnDate { get; init; }
    }
}