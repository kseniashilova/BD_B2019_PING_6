using System;
using System.Collections.Generic;
using System.Text;

namespace HW_8
{
    class Book
    {
        public int ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PagesNum { get; set; }
        public DateTime PubYear { get; set; }
        public string PubName { get; set; }

        public Publisher Publisher { get; set; }
        public ICollection<BookCat> BookCats { get; set; }
        public ICollection<Copy> Copies { get; set; }

        public Book()
        {
            BookCats = new List<BookCat>();
            Copies = new List<Copy>();
        }
    }
}
