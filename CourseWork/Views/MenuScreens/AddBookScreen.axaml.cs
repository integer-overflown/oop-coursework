using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CourseWork.Views.MenuScreens
{
    public class AddBookScreen : UserControl
    {
        public AddBookScreen()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}