using System;
using System.Collections.Generic;
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
        private readonly Subject<IComparer<BookDisplayItem>> _sortSubject = new();

        public OverviewScreenViewModel()
        {
            _bookSource
                .Connect()
                .Filter(_filterSubject)
                .Transform(book => new BookDisplayItem(book))
                .Sort(_sortSubject)
                .Bind(out _books)
                .DisposeMany()
                .Subscribe();
            _bookSource
                .Connect()
                .Select(book => book.Name!)
                .Bind(out _autoCompleteItems)
                .Subscribe();
            BookContext.Notifier.DataUpdated += RefreshUpdated;
            BookContext.Notifier.DataRemoved += RefreshRemoved;
            LoadData();
            // forces displaying all items
            _filterSubject.OnNext(DummyFilter);
            // default sorting by name
            _sortSubject.OnNext(SortExpressionComparer<BookDisplayItem>.Ascending(book => book.Content.Name!));
        }

        public ReadOnlyObservableCollection<BookDisplayItem> Books => _books;

        public ReadOnlyObservableCollection<string> AutoCompleteItems => _autoCompleteItems;

        // ReSharper disable once UnusedParameter.Local
        private static bool DummyFilter(Book book) => true;

        private void LoadData()
        {
            var context = new BookContext();
            _bookSource.AddOrUpdate(context.Books);
        }

        private void RefreshUpdated(DataChangesNotifier<Book>.DataChangedEventArgs args)
        {
            _bookSource.AddOrUpdate(args.Data);
        }

        private void RefreshRemoved(DataChangesNotifier<Book>.DataChangedEventArgs args)
        {
            _bookSource.Edit(editor => editor.Remove(args.Data));
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