using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using CourseWork.Models;
using CourseWork.ViewModels;
using CourseWork.Views.MenuScreens;

namespace CourseWork.Views
{
    public partial class MainWindow : Window
    {
        private const string SearchBookByIsbnScreenKey = "SearchBookByIsbnScreen";
        private const string OverviewScreenKey = "OverviewScreen";
        private readonly Carousel _screens;

        public MainWindow()
        {
            InitializeComponent();
            _screens = this.FindControlStrict<Carousel>("Pages");
#if DEBUG
            this.AttachDevTools();
#endif
            SetupPageChangeListeners();
        }

        private void SetupPageChangeListeners()
        {
            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.Changed.Subscribe(args =>
                {
                    if (args.PropertyName != nameof(viewModel.CurrentScreenIndex)) return;
                    _screens.SelectedIndex = viewModel.CurrentScreenIndex;
                });
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            var menuItems = this.FindControlStrict<StackPanel>("MenuItemsSizeBar");
            for (var i = 0; i < menuItems.Children.Count; ++i)
            {
                AttachIndexUpdateOnClick(menuItems.Children[i], i);
            }
        }

        private void AttachIndexUpdateOnClick(IInputElement control, int index)
        {
            if (DataContext is MainWindowViewModel viewModel)
            {
                control.PointerPressed += (_, _) => { viewModel.CurrentScreenIndex = index; };
            }
        }

        private void SearchBookByIsbnScreen_OnSearchSucceeded(ISearchAgent<Book>.SearchSucceededEventArgs args)
        {
            NavigateToBookView(SearchBookByIsbnScreenKey, args.Result);
        }

        private void NavigateToBookView(string previous, Book? book = null, bool editable = true)
        {
            var screen = _screens.FindControlStrict<BookViewScreen>("BookViewScreen");
            screen.IsEditable = editable;
            screen.Previous = previous;
            if (book is not null) screen.Book = book;
            _screens.SelectedItem = screen;
        }

        private void SearchBookByIsbnScreen_OnSearchFailed(ISearchAgent<Book>.SearchFailedEventArgs args)
        {
            var screen = _screens.FindControlStrict<SearchErrorScreen>("SearchErrorScreen");
            screen.Reason = args.Reason;
            _screens.SelectedItem = screen;
        }

        public void RoutedScreen_NavigatedBack(object sender, IRoutedScreen.ScreenNavigatedEventArgs args)
        {
            var screen = _screens.FindControl<UserControl>((string?) args.Destination);
            if (screen is null) return;
            _screens.SelectedItem = screen;
        }

        private void SearchBookByIsbnScreen_OnSearchResultIsEmpty()
        {
            _screens.SelectScreenByName("NotFoundScreen");
        }

        private void SearchBookByIsbnScreen_OnNavigationToScreenRequested(IInteractiveScreen.NavigateToScreenArgs args)
        {
            if (args.Location != IInteractiveScreen.CommonLocations.BookViewScreen) return;
            NavigateToBookView(SearchBookByIsbnScreenKey);
        }

        private void OverviewScreen_OnNavigationToScreenRequested(IInteractiveScreen.NavigateToScreenArgs args)
        {
            if (args.Location != IInteractiveScreen.CommonLocations.BookViewScreen) return;
            NavigateToBookView(OverviewScreenKey, (Book?) args.Argument, false);
        }
    }
}