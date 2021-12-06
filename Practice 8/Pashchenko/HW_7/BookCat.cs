using System;
using System.Collections.Generic;
using System.Text;

namespace HW_8
{
    class BookCat
    {
        public int BookCatId { get; set; }
        public int ISBN { get; set; }
        public string CategoryName { get; set; }

        public Category Category { get; set; }
        public Book Book { get; set; }
    }
}
