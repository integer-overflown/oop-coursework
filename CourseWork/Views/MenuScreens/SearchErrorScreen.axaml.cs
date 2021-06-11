using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace CourseWork.Views.MenuScreens
{
    public class SearchErrorScreen : RoutedScreen
    {
        public static readonly DirectProperty<SearchErrorScreen, string?> ReasonProperty =
            AvaloniaProperty.RegisterDirect<SearchErrorScreen, string?>(nameof(Reason), o => o.Reason,
                (o, v) => o.Reason = v);

        private readonly TextBlock _reason;

        public SearchErrorScreen()
        {
            InitializeComponent();
            _reason = this.FindControlStrict<TextBlock>("Reason");
        }

        public string? Reason
        {
            get => _reason.Text;
            set => _reason.Text = value;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void Back_OnClick(object? sender, RoutedEventArgs e) => NavigateBack();
    }
}