using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CourseWork.ViewModels;

namespace CourseWork.Views.MenuScreens
{
    public class OverviewScreen : UserControl
    {
        private readonly OverviewScreenViewModel _viewModel = new();

        public OverviewScreen()
        {
            InitializeComponent();
            DataContext = _viewModel;
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
            _viewModel.DisplayNameMatches(value!);
        }
    }
}