using Avalonia;

namespace CourseWork.Views.Widgets
{
    public class DecoratedTextBox : IllustratedTextControlTemplate
    {
        private const int DefaultIconSize = 24;

        public static readonly AvaloniaProperty<string?> WatermarkProperty =
            AvaloniaProperty.RegisterDirect<DecoratedTextBox, string?>(nameof(Watermark), o => o.Watermark,
                (o, v) => o.Watermark = v);

        public DecoratedTextBox()
        {
            IconSize = DefaultIconSize;
        }

        public string? Watermark { get; set; }
    }
}