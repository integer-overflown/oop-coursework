using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using CourseWork.Models;
using CourseWork.ViewModels;

namespace CourseWork.Views.MenuScreens
{
    public class BookViewScreen : UserControl
    {
        private const int MaxAuthorsCount = 5;
        private readonly BookViewScreenViewModel _viewModel;

        public BookViewScreen()
        {
            InitializeComponent();
            _viewModel = (BookViewScreenViewModel) DataContext!;
        }

        public Book Book
        {
            get => _viewModel.Book;
            set => _viewModel.Book = value;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void PopulatedTextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return) return;
            e.Handled = true;
            if (_viewModel.Authors.Count >= MaxAuthorsCount) return;
            _viewModel.Authors.Add(new Author()); // adds new text box
        }
    }
}