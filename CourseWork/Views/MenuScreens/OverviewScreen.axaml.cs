using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using CourseWork.Models;
using CourseWork.ViewModels;
using CourseWork.Views.Widgets;

namespace CourseWork.Views.MenuScreens
{
    public class OverviewScreen : InteractiveScreen
    {
        private readonly TogglePanel _filterPanel;
        private readonly OverviewScreenViewModel _viewModel;
        private object? _activeFilter;

        public OverviewScreen()
        {
            InitializeComponent();
            _viewModel = (OverviewScreenViewModel) DataContext!;
            var nameSearch = this.FindControlStrict<AutoCompleteBox>("AutoCompleteNameSearch");
            nameSearch.KeyUp += (_, _) =>
            {
                if (nameSearch.Text.Length == 0) _viewModel.ClearNameFilters();
            };
            _filterPanel = this.FindControlStrict<TogglePanel>("FilterPanel");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void AutoCompleteBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            var it = e.AddedItems;
            string? value;
            if (it is null || it.Count < 1 || (value = (string?) it[0]) is null) return;
            _viewModel.DisplayNameMatches(value);
        }

        private void BookItem_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (sender is null) return;
            var item = (BookItem) sender;
            var book = _viewModel.GetItem(item.Id);
            NavigateToScreen(IInteractiveScreen.CommonLocations.BookViewScreen, book);
        }

        private void FilterEmptyAuthors_OnChecked(object? sender, RoutedEventArgs e)
        {
            _viewModel.FilterEmptyAuthors();
            _activeFilter = sender;
        }

        private void FilterUnknownCovers_OnChecked(object? sender, RoutedEventArgs e)
        {
            _viewModel.FilterUnknownCovers();
            _activeFilter = sender;
        }

        private void Filter_OnUnchecked(object? sender, RoutedEventArgs e)
        {
            if (ReferenceEquals(_activeFilter, sender)) _viewModel.ResetFilters();
        }

        public void SetFilter(Func<Book, bool> filter)
        {
            _filterPanel.UncheckAll();
            _viewModel.SetFilter(filter);
        }

        public void SetNameFilter(Func<Book, bool> filter)
        {
            _viewModel.SetNameFilter(filter);
        }

        public void SetNameQuery(string? name)
        {
            _viewModel.NameSearchText = name;
        }
    }
}