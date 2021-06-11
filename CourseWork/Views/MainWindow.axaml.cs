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
            var screen = _screens.FindControlStrict<BookViewScreen>("BookViewScreen");
            screen.Book = args.Result;
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
            switch (args.Location)
            {
                case IInteractiveScreen.CommonLocations.BookViewScreen:
                    _screens.SelectScreenByName("BookViewScreen");
                    break;
            }
        }
    }
}