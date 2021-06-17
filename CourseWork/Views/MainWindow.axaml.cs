using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using CourseWork.Models;
using CourseWork.ViewModels;
using CourseWork.Views.MenuScreens;
using CourseWork.Views.Widgets;

namespace CourseWork.Views
{
    public partial class MainWindow : Window
    {
        private const string SearchBookByIsbnScreenKey = "SearchBookByIsbnScreen";
        private const string OverviewScreenKey = "OverviewScreen";
        private const int OverviewScreenIndex = 0;
        private readonly MenuItemsSidePanel _menuItems;

        private readonly Carousel _screens;
        private readonly MainWindowViewModel _viewModel;


        public MainWindow()
        {
            InitializeComponent();
            _viewModel = (MainWindowViewModel) DataContext!;
            _screens = this.FindControlStrict<Carousel>("Pages");
            _menuItems = this.FindControlStrict<MenuItemsSidePanel>("MenuItemsSizeBar");
            AttachListeners();
#if DEBUG
            this.AttachDevTools();
#endif
            SetupPageChangeListeners();
        }

        private void SetupPageChangeListeners()
        {
            _viewModel.Changed.Subscribe(args =>
            {
                if (args.PropertyName != nameof(_viewModel.CurrentScreenIndex)) return;
                _screens.SelectedIndex = _viewModel.CurrentScreenIndex;
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void AttachListeners()
        {
            for (var i = 0; i < _menuItems.Children.Count; ++i)
            {
                AttachIndexUpdateOnClick(_menuItems.Children[i], i);
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

        private void SearchScreen_OnFiltersSet(SearchScreen.FiltersSetEventArgs args)
        {
            var overview = _screens.FindControlStrict<OverviewScreen>(OverviewScreenKey);
            overview.SetFilter(args.GenericFilter);
            overview.SetNameFilter(args.NameFilter);
            overview.SetNameQuery(args.TargetName);
            _viewModel.CurrentScreenIndex = OverviewScreenIndex;
            _menuItems.SelectedIndex = OverviewScreenIndex;
        }
    }
}