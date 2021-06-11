using System.Collections.ObjectModel;
using System.Linq;
using CourseWork.Database;
using CourseWork.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.ViewModels
{
    public class OverviewScreenViewModel : ViewModelBase
    {
        public OverviewScreenViewModel()
        {
            Refresh();
            BookContext.Notifier.DataAppended += Refresh;
        }

        public ObservableCollection<BookDisplayItem> Books { get; } = new();

        private void Refresh()
        {
            var context = new BookContext();
            Books.Clear();
            context.Books
                .Select(entity => new BookDisplayItem(entity))
                .ForEachAsync(Books.Add);
        }

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