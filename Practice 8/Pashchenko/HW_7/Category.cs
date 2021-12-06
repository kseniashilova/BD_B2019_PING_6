using System;
using System.Collections.Generic;
using System.Text;

namespace HW_8
{
    class Category
    {
        public string CategoryName { get; set; }
        public string ParentCat { get; set; }

        public Category ParentCategory { get; set; }
        public ICollection<BookCat> BookCats { get; set; }
        public Category()
        {
            BookCats = new List<BookCat>();
        }
    }
}
