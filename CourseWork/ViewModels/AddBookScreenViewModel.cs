using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using CourseWork.Input;
using CourseWork.Models;
using ReactiveUI;

namespace CourseWork.ViewModels
{
    public class AddBookScreenViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly Subject<Book> _searchResponse = new();

        private string _isbn = "";
        private string? _isbnValidationError;

        public AddBookScreenViewModel()
        {
            this.WhenAnyValue(o => o.Isbn).Subscribe(_ => ValidateAll());
            ErrorsChanged += (_, _) => this.RaisePropertyChanged(nameof(HasErrors));
        }

        public bool IsSearchPending { get; private set; } = false;
        public IObservable<Book> SearchResult => _searchResponse;

        public string Isbn
        {
            get => _isbn;
            set => this.RaiseAndSetIfChanged(ref _isbn, value);
        }

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            yield return propertyName switch
            {
                nameof(Isbn) => _isbnValidationError!,
                _ => Enumerable.Empty<string>()
            };
        }

        public bool HasErrors => _isbnValidationError != null;

        private void ValidateAll()
        {
            var isbnError = Validators.Isbn.IsValid(_isbn) ? null : "Given ISBN is malformed";
            if (_isbnValidationError == isbnError) return;
            _isbnValidationError = isbnError;
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Isbn)));
        }

        public async Task<Book?> SearchBook()
        {
            await Task.Delay(1000);
            return new Book {Name = "Awesome"};
        }
    }
}