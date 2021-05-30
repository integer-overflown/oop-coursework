using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CourseWork.Views.MenuScreens
{
    public class SearchScreen : UserControl
    {
        public SearchScreen()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}