using System;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace LibraryApi.Domain.Dto
{
    public class SearchBorrowingDto
    {
        public long? ReaderId { get; init; }

        public string? ISBN { get; init; }

        public DateTime? ReturnDateFrom { get; init; }

        public DateTime? ReturnDateTo { get; init; }
    }
}