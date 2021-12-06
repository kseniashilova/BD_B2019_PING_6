using System;
using System.Collections.Generic;
using System.Text;

namespace HW_8
{
    class Copy
    {
        public int ISBN { get; set; }
        public int CopyNumber { get; set; }
        public int ShelfPosition { get; set; }

        public Book Book { get; set; }
        public ICollection<Borrowing> Borrowings { get; set; }
        public Copy()
        {
            Borrowings = new List<Borrowing>();
        }
    }
}
