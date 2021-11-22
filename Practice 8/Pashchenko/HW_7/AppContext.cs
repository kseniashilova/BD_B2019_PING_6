using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW_8
{
    class AppContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCat> BookCats { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Copy> Copies { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Reader> Readers { get; set; }

        public AppContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=hw_8;Username=postgres;Password=admin");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reader>().HasKey(k => k.ReaderId);
            modelBuilder.Entity<Book>().HasKey(k => k.ISBN);
            modelBuilder.Entity<Publisher>().HasKey(k => k.PubName);
            modelBuilder.Entity<Category>().HasKey(k => k.CategoryName);
            modelBuilder.Entity<Copy>().HasKey(k => k.CopyNumber);
            modelBuilder.Entity<Borrowing>().HasKey(k => k.BorrowingId);
            modelBuilder.Entity<BookCat>().HasKey(k => k.BookCatId);

            modelBuilder.Entity<Reader>().HasMany(o => o.Borrowings).WithOne(c => c.Reader).IsRequired();

            modelBuilder.Entity<Book>().HasMany(o => o.Copies).WithOne(c => c.Book).IsRequired();
            modelBuilder.Entity<Book>().HasMany(o => o.BookCats).WithOne(c => c.Book).IsRequired();

            modelBuilder.Entity<Category>().HasMany(o => o.BookCats).WithOne(c => c.Category).IsRequired();

            modelBuilder.Entity<Copy>().HasMany(o => o.Borrowings).WithOne(c => c.Copy).IsRequired();

            modelBuilder.Entity<Publisher>().HasMany(o => o.Books).WithOne(c => c.Publisher).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
