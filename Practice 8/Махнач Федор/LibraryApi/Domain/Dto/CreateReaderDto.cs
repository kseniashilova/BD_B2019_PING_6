using System;

namespace LibraryApi.Domain.Dto
{
    public class CreateReaderDto
    {
        public string LastName { get; init; } = string.Empty;

        public string FirstName { get; init; } = string.Empty;

        public string? Address { get; init; }

        public DateTime BirthDate { get; init; }
    }
}