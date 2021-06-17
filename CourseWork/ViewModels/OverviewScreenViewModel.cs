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
using ReactiveUI;

namespace CourseWork.ViewModels
{
    public class OverviewScreenViewModel : ViewModelBase
    {
        private readonly ReadOnlyObservableCollection<string> _autoCompleteItems;
        private readonly ReadOnlyObservableCollection<BookDisplayItem> _books;
        private readonly SourceCache<Book, long> _bookSource = new(item => item.Id);
        private readonly Subject<Func<Book, bool>> _filterSubject = new();

        // we need to distinguish these filters, because they may be applied simultaneously
        private readonly Subject<Func<Book, bool>> _nameMatchSubject = new();
        private readonly Subject<IComparer<BookDisplayItem>> _sortSubject = new();
        private string? _nameSearchText;

        public OverviewScreenViewModel()
        {
            _bookSource
                .Connect()
                .Filter(_nameMatchSubject)
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
            // apply no filter on startup
            _nameMatchSubject.OnNext(DummyFilter);
            _filterSubject.OnNext(DummyFilter);
            // default sorting by name
            _sortSubject.OnNext(SortExpressionComparer<BookDisplayItem>.Ascending(book => book.Content.Name!));
        }

        public string? NameSearchText
        {
            get => _nameSearchText;
            set => this.RaiseAndSetIfChanged(ref _nameSearchText, value, nameof(NameSearchText));
        }

        public ReadOnlyObservableCollection<BookDisplayItem> Books => _books;

        public ReadOnlyObservableCollection<string> AutoCompleteItems => _autoCompleteItems;

        private static bool AuthorFilter(Book book) => book.Authors.Count > 0;
        private static bool CoverFilter(Book book) => book.Cover != null;

        public void FilterEmptyAuthors() => _filterSubject.OnNext(AuthorFilter);
        public void FilterUnknownCovers() => _filterSubject.OnNext(CoverFilter);
        public void ResetFilters() => _filterSubject.OnNext(DummyFilter);
        public void SetFilter(Func<Book, bool> filter) => _filterSubject.OnNext(filter);

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

        public void DisplayNameMatches(string name) => _nameMatchSubject.OnNext(book => book.Name == name);
        public void ClearNameFilters() => _nameMatchSubject.OnNext(DummyFilter);

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