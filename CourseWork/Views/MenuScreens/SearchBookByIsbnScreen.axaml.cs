using System.Reactive;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using CourseWork.Models;
using CourseWork.ViewModels;

namespace CourseWork.Views.MenuScreens
{
    public class SearchBookByIsbnScreen : InteractiveScreen, ISearchAgent<Book>
    {
        public SearchBookByIsbnScreen()
        {
            InitializeComponent();
            SubscribeToSearchResult();
        }

        public event ISearchAgent<Book>.SearchSucceededHandler? SearchSucceeded;
        public event ISearchAgent<Book>.SearchFailedHandler? SearchFailed;
        public event ISearchAgent<Book>.SearchResultIsEmptyHandler? SearchResultIsEmpty;

        private void SubscribeToSearchResult()
        {
            var viewModel = (SearchBookByIsbnScreenViewModel) DataContext!;
            viewModel.SearchResult
                .Subscribe(Observer.Create<SearchBookByIsbnScreenViewModel.BookSearchResponse>(HandleBookSearchResult));
        }

        private void HandleBookSearchResult(SearchBookByIsbnScreenViewModel.BookSearchResponse result)
        {
            // the order is important
            if (result.IsFailed)
                SearchFailed?.Invoke(new ISearchAgent<Book>.SearchFailedEventArgs(result.ExceptionDetails!));
            else if (result.IsEmpty)
                SearchResultIsEmpty?.Invoke();
            else
                SearchSucceeded?.Invoke(new ISearchAgent<Book>.SearchSucceededEventArgs(result.Response!));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void AddABookManually_OnPointerDown(object? sender, PointerPressedEventArgs e) =>
            NavigateToScreen(IInteractiveScreen.CommonLocations.BookViewScreen);
    }
}