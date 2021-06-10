using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.Database;
using CourseWork.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.ViewModels
{
    public class OverviewScreenViewModel : ViewModelBase, IAsyncInitialization
    {
        public OverviewScreenViewModel()
        {
            var context = new BookContext();
            Initialization = context.Books
                .Select(entity => new BookDisplayItem(entity))
                .ForEachAsync(Books.Add);
        }

        public ObservableCollection<BookDisplayItem> Books { get; } = new();

        public Task Initialization { get; }

        public readonly struct BookDisplayItem
        {
            public Book Content { get; }
            public string? FirstAuthorName { get; }

            public static implicit operator BookDisplayItem(Book book)
            {
                return new(book);
            }

            public BookDisplayItem(Book book)
            {
                Content = book;
                FirstAuthorName = book.Authors.FirstOrDefault()?.Name;
            }
        }
    }
}