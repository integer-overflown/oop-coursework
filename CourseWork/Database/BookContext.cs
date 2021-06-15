using System;
using System.IO;
using Avalonia.Media.Imaging;
using CourseWork.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CourseWork.Database
{
    public class BookContext : DbContext
    {
        private const string DatabaseName = "main.db";
        private const string ApplicationName = "eveRead";
        public DbSet<Book> Books { get; set; } = null!; // suppress warning, since this property if auto-generated

        public static DataChangesNotifier<Book> Notifier { get; } = new();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var appFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                ApplicationName);
            Directory.CreateDirectory(appFolderPath);
            var dbPath = Path.Combine(appFolderPath, DatabaseName);
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