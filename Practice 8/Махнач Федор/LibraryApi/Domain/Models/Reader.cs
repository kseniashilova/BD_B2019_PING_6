using System;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace LibraryApi.Domain.Models
{
    public class Reader
    {
        public long Id { get; init; }

        public string LastName { get; init; } = string.Empty;

        public string FirstName { get; init; } = string.Empty;

        public string? Address { get; init; }

        public DateTime BirthDate { get; init; }
    }
}