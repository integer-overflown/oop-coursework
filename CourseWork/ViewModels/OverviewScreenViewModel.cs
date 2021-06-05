using System.Collections.ObjectModel;
using CourseWork.Models;
using ReactiveUI;

namespace CourseWork.ViewModels
{
    public class OverviewScreenViewModel : ViewModelBase
    {
        private ObservableCollection<Book> _books = new();

        public ObservableCollection<Book> Books
        {
            get => _books;
            set => this.RaiseAndSetIfChanged(ref _books, value, nameof(Books));
        }
    }
}