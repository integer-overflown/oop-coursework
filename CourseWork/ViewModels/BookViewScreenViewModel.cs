using CourseWork.Models;
using ReactiveUI;

namespace CourseWork.ViewModels
{
    public class BookViewScreenViewModel : ViewModelBase
    {
        private Book _book = new();

        public Book Book
        {
            get => _book;
            set => this.RaiseAndSetIfChanged(ref _book, value, nameof(Book));
        }
    }
}