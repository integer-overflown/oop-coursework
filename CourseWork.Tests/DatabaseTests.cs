using System.Data.Common;
using CourseWork.Database;
using CourseWork.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CourseWork.Tests
{
    public class InMemoryBookContext : BookContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(GetInMemoryConnection());
        }

        private static DbConnection GetInMemoryConnection()
        {
            return new SqliteConnection("Filename=:memory:");
        }
    }

    public class DatabaseTests
    {
        private BookContext _context;

        [SetUp]
        public void Setup()
        {
            _context = new BookContext();
            _context.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public void BookModelCreate_AddingBitmap_HandlesNullability()
        {
            var book = new Book
            {
                Name = "",
                Cover = null,
            };
            _context.Add(book);
            Assert.That(_context.SaveChanges, Throws.Nothing);
        }

        [Test]
        public void BookModelCreate_AddingNewName_ForbidsNull()
        {
            var book = new Book(); // "name" left uninitialized
            _context.Add(book);
            Assert.That(_context.SaveChanges, Throws.Exception.InstanceOf<DbUpdateException>());
        }
    }
}