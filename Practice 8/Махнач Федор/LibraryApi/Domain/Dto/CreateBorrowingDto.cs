using System;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace LibraryApi.Domain.Dto
{
    public class CreateBorrowingDto
    {
        public long ReaderId { get; init; }

        public string ISBN { get; init; }

        public int CopyNumber { get; init; }

        public DateTime ReturnDate { get; init; }
    }
}