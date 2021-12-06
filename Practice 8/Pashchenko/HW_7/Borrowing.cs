using System;
using System.Collections.Generic;
using System.Text;

namespace HW_8
{
    class Borrowing
    {
        public int BorrowingId { get; set; }
        public int ReaderNr { get; set; }
        public int ISBN { get; set; }
        public int CopyNumber { get; set; }
        public DateTime ReturnDate { get; set; }

        public Copy Copy { get; set; }
        public Reader Reader { get; set; }
    }
}
