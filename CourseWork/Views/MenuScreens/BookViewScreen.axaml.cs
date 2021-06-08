using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using CourseWork.Input;
using CourseWork.Models;
using CourseWork.ViewModels;

namespace CourseWork.Views.MenuScreens
{
    public class BookViewScreen : UserControl
    {
        private const int MaxAuthorsCount = 5;
        private readonly BookViewScreenViewModel _viewModel;
        private int _isBusy;

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

        private async void IconPlaceholder_Click(object? sender, RoutedEventArgs e)
        {
            if (sender is null || Interlocked.Exchange(ref _isBusy, 1) == 1) return;

            var dialog = new OpenFileDialog
            {
                AllowMultiple = false,
                Directory = Environment.SpecialFolder.CommonPictures.ToString(),
                Filters = FileExtensionFilters.Images,
                Title = "Choose a book cover"
            };

            var result = await ShowFileDialog(dialog);
            if (result is not null)
            {
                var fileName = result[0];
                var bitmap = ReadImage(fileName);
                _viewModel.Book.Cover = bitmap;

                var emitter = (Button) sender;
                emitter.Content = new Image {Source = bitmap};
            }

            _isBusy = 0;
        }

        private static async Task<string[]?> ShowFileDialog(OpenFileDialog dialog)
        {
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                return await dialog.ShowAsync(desktop.MainWindow);
            }

            Console.WriteLine("Unsupported lifetime");
            return null;
        }

        private static Bitmap ReadImage(string fileName)
        {
            using var stream = File.Open(fileName, FileMode.Open);
            return new Bitmap(stream);
        }
    }
}