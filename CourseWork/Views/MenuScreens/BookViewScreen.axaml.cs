using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CourseWork.Models;
using CourseWork.ViewModels;

namespace CourseWork.Views.MenuScreens
{
    public class BookViewScreen : UserControl
    {
        public BookViewScreen()
        {
            InitializeComponent();
        }

        public Book Book
        {
            get => ((BookViewScreenViewModel) DataContext!).Book;
            set => ((BookViewScreenViewModel) DataContext!).Book = value;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}