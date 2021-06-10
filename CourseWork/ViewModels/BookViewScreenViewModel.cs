using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Media.Imaging;
using CourseWork.Models;
using DynamicData;
using ReactiveUI;

namespace CourseWork.ViewModels
{
    public class BookViewScreenViewModel : ViewModelBase
    {
        private Book _book = new();
        private double _numberOfPages;
        private string? _publishingYear;

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
            Authors.Clear();
            Authors.AddRange(value.Authors);
            Subjects.Clear();
            Subjects.AddRange(value.Subjects);
            NumberOfPages = value.NumberOfPages;
            PublishingYear = value.PublishingYear.ToString();
            Cover = value.Cover;
        }
    }
}