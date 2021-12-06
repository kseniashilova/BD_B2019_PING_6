using System;
using System.Collections.Generic;
using System.Text;

namespace HW_8
{
    class Publisher
    {
        public string PubAddress { get; set; }
        public string PubName { get; set; }
        public ICollection<Book> Books { get; set; }
        public Publisher()
        {
            Books = new List<Book>();
        }
    }
}
