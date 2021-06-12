using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Subjects;
using CourseWork.Database;
using CourseWork.Models;
using DynamicData;
using DynamicData.Alias;
using DynamicData.Binding;

namespace CourseWork.ViewModels
{
    public class OverviewScreenViewModel : ViewModelBase
    {
        private readonly ReadOnlyObservableCollection<string> _autoCompleteItems;
        private readonly ReadOnlyObservableCollection<BookDisplayItem> _books;
        private readonly SourceCache<Book, long> _bookSource = new(item => item.Id);

        private readonly Subject<Func<Book, bool>> _filterSubject = new();

        public OverviewScreenViewModel()
        {
            _bookSource
                .Connect()
                .Filter(_filterSubject)
                .Transform(book => new BookDisplayItem(book))
                .Sort(SortExpressionComparer<BookDisplayItem>.Ascending(book => book.Content.Name!))
                .Bind(out _books)
                .DisposeMany()
                .Subscribe();
            _bookSource
                .Connect()
                .Select(book => book.Name!)
                .Bind(out _autoCompleteItems)
                .Subscribe();
            BookContext.Notifier.DataAppended += Refresh;
            Refresh();
            _filterSubject.OnNext(DummyFilter); // forces displaying all items
        }

        public ReadOnlyObservableCollection<BookDisplayItem> Books => _books;

        public ReadOnlyObservableCollection<string> AutoCompleteItems => _autoCompleteItems;

        // ReSharper disable once UnusedParameter.Local
        private static bool DummyFilter(Book book) => true;

        private void Refresh()
        {
            var context = new BookContext();
            _bookSource.AddOrUpdate(context.Books);
        }

        public void DisplayNameMatches(string name)
        {
            _filterSubject.OnNext(book => book.Name == name);
        }

        public void ClearNameFilters()
        {
            _filterSubject.OnNext(DummyFilter);
        }

        public Book GetItem(long id) => _bookSource.Lookup(id).Value;

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