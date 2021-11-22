using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HW_8
{
    class CopyRepository : MainRepository
    {
        internal static void CreateCopy(AppContext db)
        {
            var copy = new Copy();
            MakeNewVersionOfCopy(db, copy, false);
            db.Copies.Add(copy);
            db.SaveChanges();
        }

        internal static void ReadCopy(AppContext db, Copy copy)
        {
            Console.WriteLine("\n|ISBN|CopyNumber|ShelfPosition|");
            Console.WriteLine($"{copy.ISBN}. {copy.CopyNumber} {copy.ShelfPosition}");
        }


        internal static void UpdateCopy(AppContext db, Copy copy)
        {
            MakeNewVersionOfCopy(db, copy, true);
            db.Copies.Update(copy);
            db.SaveChanges();
        }

        internal static void DeleteCopy(AppContext db, Copy copy)
        {
            db.Copies.Remove(copy);
            db.SaveChanges();
        }

        internal static void MakeNewVersionOfCopy(AppContext db, Copy copy, bool update)
        {
            if (!update)
            {
                Console.WriteLine($"\nВ настоящее время существует {db.Books.Count()} книг:");
                ReadBooks(db);
                Console.WriteLine("Пожалуйста, выберите ISBN книги");
                var isbn = GetPositiveInt("Пожалуйста, введите валидный ISBN книги");
                Book book;
                book = db.Books.Find(isbn);
                while (book == null)
                {
                    isbn = GetPositiveInt("Пожалуйста, введите валидный ISBN книги");
                    book = db.Books.Find(isbn);
                }
                copy.Book = book;
                copy.ISBN = book.ISBN;

                Console.WriteLine("Пожалуйста, введите номер копии");
                copy.CopyNumber = GetUniqueCopyNumber(db);
            }
            Console.WriteLine("Пожалуйста, введите положительную позицию копии на полке");
            copy.ShelfPosition = GetPositiveInt("Пожалуйста, введите положительную позицию копии на полке");
        }
        private static int GetUniqueCopyNumber(AppContext db)
        {
            var copyNbrStr = Console.ReadLine();
            int copyNbr;
            while (!int.TryParse(copyNbrStr, out copyNbr) || copyNbr <= 0 || (db.Copies.Find(copyNbr) != null))
            {
                Console.WriteLine("Пожалуйста, введите валидный (положительный и уникальный) номер ISBN");
                copyNbrStr = Console.ReadLine();
            }

            return copyNbr;
        }
    }
}
