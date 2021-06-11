using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace CourseWork.Views.MenuScreens
{
    public class NotFoundScreen : RoutedScreen
    {
        public NotFoundScreen()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void Back_OnClick(object? sender, RoutedEventArgs e)
        {
            NavigateBack();
        }
    }
}