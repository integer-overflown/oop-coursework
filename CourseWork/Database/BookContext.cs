using System;
using System.IO;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CourseWork.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CourseWork.Database
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        private const string DatabaseName = "eveRead.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), DatabaseName);
            Console.WriteLine($"Located SQLite database at {dbPath}");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var byteArrayToBitmap = new ValueConverter<IBitmap?, byte[]>(v => Conversion.FromBitmapToByteArray(v),
                v => Conversion.FromByteArrayToBitmap(v));
            modelBuilder
                .Entity<Book>()
                .Property(book => book.Cover)
                .HasConversion(byteArrayToBitmap);
        }
    }
}