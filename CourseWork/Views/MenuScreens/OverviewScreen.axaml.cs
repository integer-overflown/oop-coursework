using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CourseWork.Views.MenuScreens
{
    public class OverviewScreen : UserControl
    {
        public OverviewScreen()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}