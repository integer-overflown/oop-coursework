using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using CourseWork.Models;
using CourseWork.ViewModels;

namespace CourseWork.Views.MenuScreens
{
    public class SearchScreen : UserControl
    {
        public delegate void FiltersSetEventHandler(FiltersSetEventArgs args);

        private readonly SearchScreenViewModel _viewModel;

        public SearchScreen()
        {
            InitializeComponent();
            _viewModel = (SearchScreenViewModel) DataContext!;
        }

        public event FiltersSetEventHandler? FiltersSet;

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void Search_OnClick(object? sender, RoutedEventArgs e)
        {
            FiltersSet?.Invoke(new FiltersSetEventArgs(_viewModel.Name, _viewModel.CreateFilter(),
                _viewModel.CreateNameFilter()));
        }

        public readonly struct FiltersSetEventArgs
        {
            public string? TargetName { get; }
            public Func<Book, bool> GenericFilter { get; }
            public Func<Book, bool> NameFilter { get; }

            public FiltersSetEventArgs(string? targetName, Func<Book, bool> genericFilter, Func<Book, bool> nameFilter)
            {
                TargetName = targetName;
                GenericFilter = genericFilter;
                NameFilter = nameFilter;
            }
        }
    }
}