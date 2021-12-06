using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HW_8
{
    public class MainRepository
    {
        internal static void ReadBooks(AppContext db)
        {
            var books = db.Books.ToList();
            Console.WriteLine("Список книг:\n|ISBN|Title|Author|PagesNum|PubYear|PubName|");
            foreach (Book b in books)
            {
                Console.WriteLine($"{b.ISBN}. {b.Title} {b.Author} {b.PagesNum} {b.PubYear.ToString($"dd/MM/yyyy")} {b.PubName}");
            }
        }

        internal static void ReadPublishers(AppContext db)
        {
            var publishers = db.Publishers.ToList();
            Console.WriteLine("Список издателей:\n|PubName|PubAddress|");
            foreach (Publisher b in publishers)
            {
                Console.WriteLine($"{b.PubName}. {b.PubAddress}");
            }
        }

        internal static void ShowAllTables(AppContext db)
        {
            ReadReaders(db);

            ReadBooks(db);

            ShowPublishers(db);

            ReadCategories(db);

            ReadCopies(db);

            ReadBorrowings(db);

            ReadBookCats(db);
        }

        private static void ReadBookCats(AppContext db)
        {
            var bookCats = db.BookCats.ToList();
            Console.WriteLine("Список связей книга-категория:\n|ISBN|CategoryName|");
            foreach (BookCat b in bookCats)
            {
                Console.WriteLine($"{b.ISBN}. {b.CategoryName}");
            }
        }

        private static void ReadCategories(AppContext db)
        {
            var categories = db.Categories.ToList();
            Console.WriteLine("Список категорий книг:\n|CategoryName|ParentCat|");
            foreach (Category b in categories)
            {
                Console.WriteLine($"{b.CategoryName}. {b.ParentCat}");
            }
        }

        internal static void ReadReaders(AppContext db)
        {
            var readers = db.Readers.ToList();
            Console.WriteLine("Список читателей:\n|ReaderId|LastName|FirstName|Address|BirthDate|");
            foreach (Reader b in readers)
            {
                Console.WriteLine($"{b.ReaderId}. {b.LastName} {b.FirstName} {b.Address} {b.BirthDate.ToString($"dd/MM/yyyy")}");
            }
        }

        internal static void ReadBorrowings(AppContext db)
        {
            var borrowings = db.Borrowings.ToList();
            Console.WriteLine("Список бронирований книг:\n|BorrowingId|ReaderNr|ISBN|CopyNumber|ReturnDate|");
            foreach (Borrowing b in borrowings)
            {
                Console.WriteLine($"{b.BorrowingId} {b.ReaderNr} {b.ISBN} {b.CopyNumber} {b.ReturnDate.ToString($"dd/MM/yyyy")}");
            }
        }

        internal static void ReadCopies(AppContext db)
        {
            var copies = db.Copies.ToList();
            Console.WriteLine("Список копий:\n|ISBN|CopyNumber|ShelfPosition|");
            foreach (Copy b in copies)
            {
                Console.WriteLine($"{b.ISBN}. {b.CopyNumber} {b.ShelfPosition}");
            }
        }

        private static void ShowPublishers(AppContext db)
        {
            var publishers = db.Publishers.ToList();
            Console.WriteLine("Список издателей:\n|PubName|PubAddress|");
            foreach (Publisher b in publishers)
            {
                Console.WriteLine($"{b.PubName}. {b.PubAddress}");
            }
        }

        internal static int GetPositiveInt(string warning)
        {
            int res;
            var str = Console.ReadLine();
            while (!int.TryParse(str, out res) || res <= 0)
            {
                Console.WriteLine(warning);
                str = Console.ReadLine();
            }
            return res;
        }
    }
}
