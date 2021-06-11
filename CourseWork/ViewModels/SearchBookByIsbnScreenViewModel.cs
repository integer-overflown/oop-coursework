using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using System.Windows.Input;
using CourseWork.Input;
using CourseWork.Models;
using CourseWork.Networking;
using ReactiveUI;

namespace CourseWork.ViewModels
{
    public class SearchBookByIsbnScreenViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly Subject<BookSearchResponse> _searchResponse = new();
        private bool _isAvailable;
        private string _isbn = "";
        private string? _isbnValidationError;
        private bool _isSearchPending;

        public SearchBookByIsbnScreenViewModel()
        {
            this.WhenAnyValue(o => o.Isbn).Subscribe(_ => ValidateAll());
            ErrorsChanged += (_, _) => this.RaisePropertyChanged(nameof(HasErrors));

            var searchCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                try
                {
                    IsSearchPending = true;
                    _searchResponse.OnNext(await SearchBook(_isbn));
                }
                catch (ApiClientException e)
                {
                    // using onError here terminates observable value flow
                    _searchResponse.OnNext(new BookSearchResponse(e.Message));
                }
                finally
                {
                    IsSearchPending = false;
                }
            });
            searchCommand.ThrownExceptions.Subscribe(exception =>
            {
                if (exception is not ApiClientException) throw exception;
            });
            PerformSearch = searchCommand;
        }

        public bool IsSearchPending
        {
            get => _isSearchPending;
            private set
            {
                IsAvailable = !value;
                this.RaiseAndSetIfChanged(ref _isSearchPending, value);
            }
        }

        public IObservable<BookSearchResponse> SearchResult => _searchResponse;

        public ICommand PerformSearch { get; }

        public string Isbn
        {
            get => _isbn;
            set => this.RaiseAndSetIfChanged(ref _isbn, value);
        }

        public bool IsAvailable
        {
            get => _isAvailable;
            set => this.RaiseAndSetIfChanged(ref _isAvailable, value);
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
            var isValid = Validators.Isbn.IsValid(_isbn);
            IsAvailable = isValid;
            var isbnError = isValid ? null : "Given ISBN is malformed";
            if (_isbnValidationError == isbnError) return;
            _isbnValidationError = isbnError;
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Isbn)));
        }

        private static async Task<Book?> SearchBook(string isbnString)
        {
            var isbn = InputHelpers.FilterIsbnDashes(isbnString);
            var client = new ApiClient();
            return await client.QueryBookByIsbnAsync(isbn);
        }

        public readonly struct BookSearchResponse
        {
            public Book? Response { get; }
            public string? ExceptionDetails { get; }

            public bool IsEmpty => Response is null;
            public bool IsFailed => ExceptionDetails is not null;

            private BookSearchResponse(Book? response)
            {
                Response = response;
                ExceptionDetails = null;
            }

            public BookSearchResponse(string? error)
            {
                Response = null;
                ExceptionDetails = error;
            }

            public static implicit operator BookSearchResponse(Book? response)
            {
                return new(response);
            }
        }
    }
}