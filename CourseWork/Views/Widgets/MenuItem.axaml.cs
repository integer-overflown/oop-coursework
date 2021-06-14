using Avalonia;

namespace CourseWork.Views.Widgets
{
    public class MenuItem : IllustratedTextControlTemplate
    {
        private const int DefaultIconSize = 32;

        public static readonly StyledProperty<bool> IsSelectedProperty =
            AvaloniaProperty.Register<MenuItem, bool>(nameof(IsSelected));

        public MenuItem()
        {
            IconSize = DefaultIconSize;
        }

        public bool IsSelected
        {
            get => GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }
    }
}