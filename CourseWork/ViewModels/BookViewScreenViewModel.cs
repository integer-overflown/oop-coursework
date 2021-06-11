using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using CourseWork.Database;
using CourseWork.Models;
using CourseWork.Utils;
using DynamicData;
using ReactiveUI;

namespace CourseWork.ViewModels
{
    public class BookViewScreenViewModel : ViewModelBase
    {
        private Book _book = new();
        private bool _isEditable;
        private string? _name;
        private double _numberOfPages;
        private string? _publisher;
        private string? _publishingYear;

        public bool IsEditable
        {
            get => _isEditable;
            set => this.RaiseAndSetIfChanged(ref _isEditable, value, nameof(IsEditable));
        }

        public string? Name
        {
            get => _name;
            set
            {
                _book.Name = value;
                this.RaiseAndSetIfChanged(ref _name, value, nameof(Name));
            }
        }

        public string? Publisher
        {
            get => _publisher;
            set
            {
                _book.Publisher = value;
                this.RaiseAndSetIfChanged(ref _publisher, value, nameof(Publisher));
            }
        }

        public ObservableCollection<Author> Authors { get; } = new(Enumerable.Repeat(new Author(), 1));
        public ObservableCollection<Subject> Subjects { get; } = new(Enumerable.Repeat(new Subject(), 1));

        public double NumberOfPages
        {
            get => _numberOfPages;
            set
            {
                _book.NumberOfPages = (int) value;
                this.RaiseAndSetIfChanged(ref _numberOfPages, value, nameof(NumberOfPages));
            }
        }

        public string? PublishingYear
        {
            get => _publishingYear;
            set
            {
                _book.PublishingYear = string.IsNullOrEmpty(value) ? 0 : int.Parse(value);
                this.RaiseAndSetIfChanged(ref _publishingYear, value, nameof(PublishingYear));
            }
        }

        public IBitmap? Cover
        {
            get => _book.Cover;
            set
            {
                _book.Cover = value;
                this.RaisePropertyChanged(nameof(Cover));
            }
        }

        public Book Book
        {
            get => _book;
            set
            {
                if (_book == value) return;
                ResetBook(value);
                this.RaisePropertyChanged(nameof(Book));
            }
        }

        private void ResetBook(Book value)
        {
            _book = value;
            Name = value.Name;
            Publisher = value.Publisher;
            RefillOrDefault(Authors, _book.Authors);
            RefillOrDefault(Subjects, _book.Subjects);
            NumberOfPages = value.NumberOfPages;
            PublishingYear = value.PublishingYear.ToString();
            Cover = value.Cover;
        }

        private static void RefillOrDefault<T>(IList<T> target, IEnumerable<T> source) where T : new()
        {
            target.Clear();
            var items = source.ToList();
            if (items.Any())
                target.AddRange(items);
            else
                target.Add(new T());
        }

        public void Reset()
        {
            ResetBook(new Book());
        }

        public async Task Save()
        {
            await using var context = new BookContext();
            // map items from observable collections to model's ones
            _book.Authors.SetContent(Authors);
            _book.Subjects.SetContent(Subjects);

            await context.AddAsync(_book);
            var saved = await context.SaveChangesAsync();
            BookContext.Notifier.NotifyDataAppended();
            Console.WriteLine($"INFO: saved {saved} records");
        }

        public bool ContainsValidItem()
        {
            return !string.IsNullOrWhiteSpace(_book.Name);
        }
    }
}