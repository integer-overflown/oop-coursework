using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using CourseWork.Input;
using CourseWork.Models;
using CourseWork.Utils;
using CourseWork.ViewModels;
using CourseWork.Views.Widgets;
using ReactiveUI;

namespace CourseWork.Views.MenuScreens
{
    public class BookViewScreen : RoutedScreen
    {
        private const int MaxPopulatedItemsCount = 5;

        public static readonly DirectProperty<BookViewScreen, bool> IsEditableProperty =
            AvaloniaProperty.RegisterDirect<BookViewScreen, bool>(nameof(IsEditable), o => o.IsEditable,
                (o, v) => o.IsEditable = v);

        private readonly PlaceholderImageButton _bookCover;
        private readonly BookViewScreenViewModel _viewModel;
        private int _isBusy;
        private int _isSaving;

        public BookViewScreen()
        {
            InitializeComponent();
            _viewModel = (BookViewScreenViewModel) DataContext!;
            _bookCover = this.FindControlStrict<PlaceholderImageButton>("BookCover");
            _viewModel.Changed.Subscribe(HandlePropertyChanged);
        }

        public bool IsEditable
        {
            get => _viewModel.IsEditable;
            set => _viewModel.IsEditable = value;
        }

        public Book Book
        {
            get => _viewModel.Book;
            set => _viewModel.Book = value;
        }

        private void HandlePropertyChanged(IReactivePropertyChangedEventArgs<IReactiveObject> args)
        {
            switch (args.PropertyName)
            {
                case nameof(_viewModel.Cover):
                    var cover = _viewModel.Cover;
                    _bookCover.ActualContent = cover is null ? null : new Image {Source = cover};
                    break;
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void PopulatedAuthorTextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            GenericPopulateTextBoxHandler(_viewModel.Authors, e);
        }

        private void PopulatedSubjectTextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            GenericPopulateTextBoxHandler(_viewModel.Subjects, e);
        }

        private static void GenericPopulateTextBoxHandler<T>(ICollection<T> items, KeyEventArgs e) where T : new()
        {
            if (e.Key != Key.Return) return;
            e.Handled = true;
            if (items.Count >= MaxPopulatedItemsCount) return;
            items.Add(new T()); // adds new text box
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
                var cover = Files.ReadBitmap(fileName);
                _viewModel.Cover = cover;
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

        private void AddAnother_Click(object? sender, RoutedEventArgs e)
        {
            _viewModel.Reset();
        }

        private async void FinishButton_Click(object? sender, RoutedEventArgs e)
        {
            if (Interlocked.Exchange(ref _isSaving, 1) == 1) return;
            if (_viewModel.ContainsValidItem())
            {
                await _viewModel.Save();
                _viewModel.Reset();
            }

            // TODO: show the error if the item is invalid
            _isSaving = 0;
        }

        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            _viewModel.Reset();
            NavigateBack();
        }
    }
}