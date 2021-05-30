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