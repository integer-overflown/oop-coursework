using System.Collections.ObjectModel;
using System.Linq;
using CourseWork.Models;
using ReactiveUI;

namespace CourseWork.ViewModels
{
    public class OverviewScreenViewModel : ViewModelBase
    {
        private ObservableCollection<BookDisplayItem> _books = new();

        public OverviewScreenViewModel()
        {
            _books.Add(new Book
            {
                Authors = new[] {new Author {Name = "Erich Maria Remarque"}},
                Name = "Three Comrades"
            });
        }

        public ObservableCollection<BookDisplayItem> Books
        {
            get => _books;
            set => this.RaiseAndSetIfChanged(ref _books, value, nameof(Books));
        }

        public readonly struct BookDisplayItem
        {
            public Book Content { get; }
            public string? FirstAuthorName { get; }

            public static implicit operator BookDisplayItem(Book book)
            {
                return new(book);
            }

            private BookDisplayItem(Book book)
            {
                Content = book;
                FirstAuthorName = book.Authors.FirstOrDefault()?.Name;
            }
        }
    }
}