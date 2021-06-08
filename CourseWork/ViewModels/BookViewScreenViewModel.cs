using System.Collections.ObjectModel;
using System.Linq;
using CourseWork.Models;
using DynamicData;
using ReactiveUI;

namespace CourseWork.ViewModels
{
    public class BookViewScreenViewModel : ViewModelBase
    {
        private Book _book = new();

        public ObservableCollection<Author> Authors { get; } = new(Enumerable.Repeat(new Author(), 1));

        public Book Book
        {
            get => _book;
            set
            {
                if (_book == value) return;
                _book = value;
                Authors.Clear();
                Authors.AddRange(value.Authors);
                this.RaisePropertyChanged(nameof(Book));
            }
        }
    }
}