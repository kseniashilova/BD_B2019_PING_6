using System;
using System.Collections.Generic;
using System.Linq;

namespace HW_8
{
    class Program
    {
        public static Random rnd;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static void Main(string[] args)
        {
            bool flag = true;
            rnd = new Random();
            Console.WriteLine("Здравствуйте!\nДанная программа добавляет новые записи в базу данных.\n");
            using (AppContext db = new AppContext())
            {
                while (flag)
                {
                    Console.WriteLine("[1] Просмотр всех таблиц.\n[2] Просмотр меню книг.\n[3] Просмотр меню копий.\n[4] Просмотр меню бронирования.\n[5] Выход из программы");
                    var command = Console.ReadKey();
                    if (db.Books.Count() == 0)
                    {
                        CreateDatabase(db);
                    }

                    switch (command.Key)
                    {
                        case ConsoleKey.D1:
                            Console.WriteLine();
                            MainRepository.ShowAllTables(db);
                            break;

                        case ConsoleKey.D2:
                            command = OpenBookMenu(db);
                            break;
                        case ConsoleKey.D3:
                            command = OpenCopyMenu(db);
                            break;
                        case ConsoleKey.D4:
                            command = OpenBorrowingMenu(db);
                            break;
                        case ConsoleKey.D5:
                            flag = false;
                            break;
                        default:
                            Console.WriteLine("Неизвестная комманда\n");
                            break;
                    }

                }
            }
        }

        private static ConsoleKeyInfo OpenBookMenu(AppContext db)
        {
            ConsoleKeyInfo command;
            Console.WriteLine("\n[1] Создание книги.\n[2] Вывод всех книг.\n[3] Вывод книги по ISBN.\n" +
                    "[4] Обновление книги по ISBN.\n[5] Удаление книги по ISBN.\n");
            Book book;
            command = Console.ReadKey();
            switch (command.Key)
            {
                case ConsoleKey.D1:
                    BookRepository.CreateBook(db);
                    break;

                case ConsoleKey.D2:
                    MainRepository.ReadBooks(db);
                    break;

                case ConsoleKey.D3:
                    Console.WriteLine("Введите ISBN книги, которую хотите увидеть");
                    book = db.Books.Find(MainRepository.GetPositiveInt("Введите валидный ISBN"));
                    if (book != null)
                    {
                        BookRepository.ReadBook(db, book);
                    }
                    else
                    {
                        Console.WriteLine("Книги с данной ISBN в базе данных не найдено.");
                    }
                    break;
                case ConsoleKey.D4:
                    Console.WriteLine("Введите ISBN книги, которую хотите обновить");
                    book = db.Books.Find(MainRepository.GetPositiveInt("Введите валидный ISBN"));
                    if (book != null)
                    {
                        BookRepository.UpdateBook(db, book);
                    }
                    else
                    {
                        Console.WriteLine("Книги с данной ISBN в базе данных не найдено.");
                    }
                    break;

                case ConsoleKey.D5:
                    Console.WriteLine("Введите ISBN книги, которую хотите удалить");
                    book = db.Books.Find(MainRepository.GetPositiveInt("Введите валидный ISBN"));
                    if (book != null)
                    {
                        BookRepository.DeleteBook(db, book);
                    }
                    else
                    {
                        Console.WriteLine("Книги с данной ISBN в базе данных не найдено.");
                    }
                    break;
                default:
                    Console.WriteLine("Неизвестная комманда\n");
                    break;
            }

            return command;
        }


        private static ConsoleKeyInfo OpenCopyMenu(AppContext db)
        {
            ConsoleKeyInfo command;
            Console.WriteLine("\n[1] Создание копии.\n[2] Вывод всех копий.\n[3] Вывод копии по номеру копии.\n" +
                    "[4] Обновление копии по номеру копии.\n[5] Удаление копии по номеру копии.\n");
            Copy copy;
            command = Console.ReadKey();
            switch (command.Key)
            {
                case ConsoleKey.D1:
                    CopyRepository.CreateCopy(db);
                    break;

                case ConsoleKey.D2:
                    MainRepository.ReadCopies(db);
                    break;

                case ConsoleKey.D3:
                    Console.WriteLine("Введите номер копии, которую хотите увидеть");
                    copy = db.Copies.Find(MainRepository.GetPositiveInt("Введите валидный ISBN"));
                    if (copy != null)
                    {
                        CopyRepository.ReadCopy(db, copy);
                    }
                    else
                    {
                        Console.WriteLine("Копии с данным номером в базе данных не найдено.");
                    }
                    break;
                case ConsoleKey.D4:
                    Console.WriteLine("Введите номеру копии, которую хотите обновить");
                    copy = db.Copies.Find(MainRepository.GetPositiveInt("Введите валидный номер копии"));
                    if (copy != null)
                    {
                        CopyRepository.UpdateCopy(db, copy);
                    }
                    else
                    {
                        Console.WriteLine("Копии с данной номером в базе данных не найдено.");
                    }
                    break;

                case ConsoleKey.D5:
                    Console.WriteLine("Введите номер копии копии, которую хотите удалить");
                    copy = db.Copies.Find(MainRepository.GetPositiveInt("Введите валидный номер копии"));
                    if (copy != null)
                    {
                        CopyRepository.DeleteCopy(db, copy);
                    }
                    else
                    {
                        Console.WriteLine("Копии с данным номером в базе данных не найдено.");
                    }
                    break;
                default:
                    Console.WriteLine("Неизвестная комманда\n");
                    break;
            }

            return command;
        }

        private static ConsoleKeyInfo OpenBorrowingMenu(AppContext db)
        {
            ConsoleKeyInfo command;
            Console.WriteLine("\n[1] Создание брони.\n[2] Вывод всех броней.\n[3] Вывод брони по id.\n" +
                    "[4] Обновление брони по ISBN.\n[5] Удаление брони по id.\n");
            Borrowing borrowing;
            command = Console.ReadKey();
            switch (command.Key)
            {
                case ConsoleKey.D1:
                    BorrowingRepository.CreateBorrowing(db);
                    break;

                case ConsoleKey.D2:
                    MainRepository.ReadBorrowings(db);
                    break;

                case ConsoleKey.D3:
                    Console.WriteLine("Введите id брони, которую хотите увидеть");
                    borrowing = db.Borrowings.Find(MainRepository.GetPositiveInt("Введите валидный id"));
                    if (borrowing != null)
                    {
                        BorrowingRepository.ReadBorrowing(db, borrowing);
                    }
                    else
                    {
                        Console.WriteLine("Брони с данным id в базе данных не найдено.");
                    }
                    break;
                case ConsoleKey.D4:
                    Console.WriteLine("Введите id брони, которую хотите обновить");
                    borrowing = db.Borrowings.Find(MainRepository.GetPositiveInt("Введите валидный id"));
                    if (borrowing != null)
                    {
                        BorrowingRepository.UpdateBorrowing(db, borrowing);
                    }
                    else
                    {
                        Console.WriteLine("Брони с данным id в базе данных не найдено.");
                    }
                    break;

                case ConsoleKey.D5:
                    Console.WriteLine("Введите id брони, которую хотите удалить");
                    borrowing = db.Borrowings.Find(MainRepository.GetPositiveInt("Введите валидный id"));
                    if (borrowing != null)
                    {
                        BorrowingRepository.DeleteBorrowing(db, borrowing);
                    }
                    else
                    {
                        Console.WriteLine("Брони с данным id в базе данных не найдено.");
                    }
                    break;
                default:
                    Console.WriteLine("Неизвестная комманда\n");
                    break;
            }

            return command;
        }

        private static void CreateDatabase(AppContext db)
        {
            var category = new Category();
            category.CategoryName = "Poem";

            var publisher = new Publisher();
            publisher.PubAddress = "20 Myasnitskaya ulitsa";
            publisher.PubName = "HSE Publication";

            var book = new Book();
            book.ISBN = 1;
            book.Title = "Following Cain's path.";
            book.Author = "M.A.Voloshin";
            book.PagesNum = 42;
            book.PubYear = new DateTime(1926, 01, 01);
            book.PubName = publisher.PubName;
            book.Publisher = publisher;

            publisher.Books.Add(book);

            var bookCat = new BookCat();
            bookCat.BookCatId = 1;
            bookCat.ISBN = book.ISBN;
            bookCat.CategoryName = category.CategoryName;
            bookCat.Category = category;
            bookCat.Book = book;

            book.BookCats.Add(bookCat);
            category.BookCats.Add(bookCat);

            var copy = new Copy();
            copy.ISBN = book.ISBN;
            copy.CopyNumber = 1;
            copy.ShelfPosition = 1;
            copy.Book = book;

            book.Copies.Add(copy);

            var reader = new Reader();
            reader.ReaderId = 1;
            reader.LastName = "Kras";
            reader.FirstName = "Mazov";
            reader.Address = "Vniisok";
            reader.BirthDate = new DateTime(2001, 7, 13);

            var borrowing = new Borrowing();
            borrowing.BorrowingId = 1;
            borrowing.ISBN = book.ISBN;
            borrowing.ReaderNr = reader.ReaderId;
            borrowing.Reader = reader;
            borrowing.ReturnDate = new DateTime(2021, 12, 17);
            borrowing.CopyNumber = copy.CopyNumber;
            borrowing.Copy = copy;

            reader.Borrowings.Add(borrowing);
            copy.Borrowings.Add(borrowing);

            db.Books.Add(book);
            db.BookCats.Add(bookCat);
            db.Borrowings.Add(borrowing);
            db.Categories.Add(category);
            db.Copies.Add(copy);
            db.Publishers.Add(publisher);
            db.Readers.Add(reader);
            db.SaveChanges();
        }



        public static string GetRandomString(int len, string source)
        {
            return new string(Enumerable.Repeat(source, len)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
        public static string GetName()
        {
            var nameLength = rnd.Next(5, 10);
            var name = GetRandomString(1, chars).ToUpper();
            name += GetRandomString(nameLength, chars);
            return name;
        }
        public static DateTime GetRandomDate(DateTime date)
        {
            DateTime start = new DateTime(1900, 1, 1);
            int range = (date - start).Days;
            return start.AddDays(rnd.Next(range));
        }
    }
}
