using System;
using System.Collections.Generic;
using System.Text;

namespace HW_8
{
    class Reader
    {
        public int ReaderId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Borrowing> Borrowings { get; set; }
        public Reader()
        {
            Borrowings = new List<Borrowing>();
        }
    }
}
