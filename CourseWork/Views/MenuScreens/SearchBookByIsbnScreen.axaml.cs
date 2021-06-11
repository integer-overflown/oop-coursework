using System;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CourseWork.Models;
using CourseWork.ViewModels;

namespace CourseWork.Views.MenuScreens
{
    public class SearchBookByIsbnScreen : UserControl, ISearchAgent<Book>
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
            viewModel.SearchResult.Subscribe(Observer.Create<Book?>(HandleBookSearchResult, HandleBookSearchError));
        }

        private void HandleBookSearchResult(Book? result)
        {
            if (result is null)
                SearchResultIsEmpty?.Invoke();
            else
                SearchSucceeded?.Invoke(new ISearchAgent<Book>.SearchSucceededEventArgs(result));
        }

        private void HandleBookSearchError(Exception e)
        {
            SearchFailed?.Invoke(new ISearchAgent<Book>.SearchFailedEventArgs(e.Message));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}