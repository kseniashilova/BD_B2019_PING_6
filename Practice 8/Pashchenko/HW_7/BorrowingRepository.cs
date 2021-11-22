using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HW_8
{
    class BorrowingRepository : MainRepository
    {
        internal static void ReadBorrowing(AppContext db, Borrowing borrowing)
        {
            Console.WriteLine("\n|ReaderNr|ISBN|CopyNumber|ReturnDate|");
            Console.WriteLine($"{borrowing.ReaderNr}. {borrowing.ISBN} " +
                $"{borrowing.CopyNumber} {borrowing.ReturnDate.ToString($"dd/MM/yyyy")}");
        }

        internal static void CreateBorrowing(AppContext db)
        {
            var borrowing = new Borrowing();
            MakeNewVersionOfBorrowing(db, borrowing, false);
            db.Borrowings.Add(borrowing);
            db.SaveChanges();
        }
        internal static void UpdateBorrowing(AppContext db, Borrowing borrowing)
        {
            MakeNewVersionOfBorrowing(db, borrowing, true);
            db.Borrowings.Update(borrowing);
            db.SaveChanges();
        }

        internal static void DeleteBorrowing(AppContext db, Borrowing borrowing)
        {
            db.Borrowings.Remove(borrowing);
            db.SaveChanges();
        }

        internal static void MakeNewVersionOfBorrowing(AppContext db, Borrowing borrowing, bool update)
        {
            if (!update)
            {
                Console.WriteLine("Пожалуйста, введите номер брони");
                borrowing.BorrowingId = GetUniqueBorrowingId(db);
            }

            Console.WriteLine($"\nВ настоящее время существует {db.Readers.Count()} читателей:");
            ReadReaders(db);
            Console.WriteLine("Пожалуйста, введите id существующего читателя");
            int readerId = GetPositiveInt("Пожалуйста, введите id существующего читателя");
            Reader reader;
            reader = db.Readers.Find(readerId);
            while (reader == null)
            {
                readerId = GetPositiveInt("Пожалуйста, введите id существующего читателя");
                reader = db.Readers.Find(readerId);
            }
            borrowing.ReaderNr = reader.ReaderId;
            borrowing.Reader = reader;

            Console.WriteLine($"\nВ настоящее время существует {db.Copies.Count()} копий:");
            ReadCopies(db);
            Console.WriteLine("Пожалуйста, введите номер копии");
            int copyNbr = GetPositiveInt("Пожалуйста, введите валидный номер копии");
            Copy copy;
            copy = db.Copies.Find(copyNbr);
            while (copy == null)
            {
                copyNbr = GetPositiveInt("Пожалуйста, введите валидный номер копии");
                copy = db.Copies.Find(copyNbr);
            }
            borrowing.CopyNumber = copyNbr;
            borrowing.Copy = copy;
            borrowing.ISBN = copy.ISBN;


            Console.WriteLine("Пожалуйста, введите год возврата");
            var yearStr = Console.ReadLine();
            Console.WriteLine("Пожалуйста, введите месяц возврата");
            var monthStr = Console.ReadLine();
            Console.WriteLine("Пожалуйста, введите день возврата");
            var dayStr = Console.ReadLine();
            int year, month, day;
            while (!int.TryParse(yearStr, out year) || year < 0 ||
                !int.TryParse(monthStr, out month) || month < 0 || month > 12 ||
                !int.TryParse(dayStr, out day) || day < 0 || day > 31)
            {
                Console.WriteLine("Пожалуйста, введите валидные данные");
                Console.WriteLine("Пожалуйста, введите год возврата");
                yearStr = Console.ReadLine();
                Console.WriteLine("Пожалуйста, введите месяц возврата");
                monthStr = Console.ReadLine();
                Console.WriteLine("Пожалуйста, введите день возврата");
                dayStr = Console.ReadLine();
            }
            borrowing.ReturnDate = new DateTime(year, month, day);

            copy.Borrowings.Add(borrowing);
            reader.Borrowings.Add(borrowing);
        }

        private static int GetUniqueBorrowingId(AppContext db)
        {
            var idStr = Console.ReadLine();
            int id;
            while (!int.TryParse(idStr, out id) || id <= 0 || (db.Borrowings.Find(id) != null))
            {
                Console.WriteLine("Пожалуйста, введите валидный (положительный и уникальный) номер брони");
                idStr = Console.ReadLine();
            }

            return id;
        }
    }
}
