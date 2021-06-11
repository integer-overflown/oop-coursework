using System;
using System.Collections.ObjectModel;
using System.Linq;
using CourseWork.Database;
using CourseWork.Models;
using DynamicData;
using DynamicData.Binding;

namespace CourseWork.ViewModels
{
    public class OverviewScreenViewModel : ViewModelBase
    {
        private readonly ReadOnlyObservableCollection<BookDisplayItem> _books;
        private readonly SourceCache<Book, long> _bookSource = new(item => item.Id);

        public OverviewScreenViewModel()
        {
            _bookSource
                .Connect()
                .Transform(book => new BookDisplayItem(book))
                .Sort(SortExpressionComparer<BookDisplayItem>.Ascending(book => book.Content.Name!))
                .Bind(out _books)
                .DisposeMany()
                .Subscribe();
            BookContext.Notifier.DataAppended += Refresh;
            Refresh();
        }

        public ReadOnlyObservableCollection<BookDisplayItem> Books => _books;

        private void Refresh()
        {
            var context = new BookContext();
            _bookSource.AddOrUpdate(context.Books);
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