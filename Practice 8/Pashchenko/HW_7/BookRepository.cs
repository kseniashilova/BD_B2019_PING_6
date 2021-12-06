using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HW_8
{
    public class BookRepository : MainRepository
    {
        internal static void CreateBook(AppContext db)
        {
            var book = new Book();
            MakeNewVersionOfBook(db, book, false);
            db.Books.Add(book);
            db.SaveChanges();
        }

        internal static void ReadBook(AppContext db, Book book)
        {
            Console.WriteLine("\n|ISBN|Title|Author|PagesNum|PubYear|PubName|");
            Console.WriteLine($"{book.ISBN}. {book.Title} {book.Author} {book.PagesNum} " +
                $"{book.PubYear.ToString($"dd/MM/yyyy")} {book.PubName}");
        }


        internal static void UpdateBook(AppContext db, Book book)
        {
            MakeNewVersionOfBook(db, book, true);
            db.Books.Update(book);
            db.SaveChanges();
        }

        internal static void DeleteBook(AppContext db, Book book)
        {
            db.Books.Remove(book);
            db.SaveChanges();
        }

        internal static void MakeNewVersionOfBook(AppContext db, Book book, bool update)
        {
            Console.WriteLine($"\nВ настоящее время существует {db.Publishers.Count()} издательств:");
            ReadPublishers(db);
            Console.WriteLine("Пожалуйста, выберите имя существующего издательства");
            var pubName = Console.ReadLine();
            Publisher publisher;
            publisher = db.Publishers.Find(pubName);
            while (publisher == null)
            {
                Console.WriteLine("Пожалуйста, выберите имя существующего издательства");
                pubName = Console.ReadLine();
                publisher = db.Publishers.Find(pubName);
            }
            book.PubName = pubName;
            book.Publisher = publisher;

            if (!update)
            {
                Console.WriteLine("Пожалуйста, введите номер ISBN");
                book.ISBN = GetUniqueISBN(db);
            }
            Console.WriteLine("Пожалуйста, введите название книги");
            book.Title = Console.ReadLine();

            Console.WriteLine("Пожалуйста, введите автора книги");
            book.Author = Console.ReadLine();

            Console.WriteLine("Пожалуйста, введите количество страниц");
            var pgsNmrStr = Console.ReadLine();
            int pgsNmr;
            while (!int.TryParse(pgsNmrStr, out pgsNmr) || pgsNmr <= 0)
            {
                Console.WriteLine("Пожалуйста, введите валидное (положительное) количество страниц");
                pgsNmrStr = Console.ReadLine();
            }
            book.PagesNum = pgsNmr;

            Console.WriteLine("Пожалуйста, введите год издания");
            var dateStr = Console.ReadLine();
            int date;
            while (!int.TryParse(dateStr, out date) || date < 0)
            {
                Console.WriteLine("Пожалуйста, введите валидный год издания");
                dateStr = Console.ReadLine();
            }
            book.PubYear = new DateTime(date, 1, 1);
            publisher.Books.Add(book);
        }

        private static int GetUniqueISBN(AppContext db)
        {
            var isbnStr = Console.ReadLine();
            int isbn;
            while (!int.TryParse(isbnStr, out isbn) || isbn <= 0 || (db.Books.Find(isbn) != null))
            {
                Console.WriteLine("Пожалуйста, введите валидный (положительный и уникальный) номер ISBN");
                isbnStr = Console.ReadLine();
            }

            return isbn;
        }
    }
}
