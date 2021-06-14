using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace CourseWork.Views.Widgets
{
    public class IllustratedTextControlTemplate : TemplatedControl
    {
        public static readonly AvaloniaProperty<IImage?> IconSourceProperty =
            AvaloniaProperty.RegisterDirect<IllustratedTextControlTemplate, IImage?>(nameof(IconSource),
                o => o.IconSource,
                (o, v) => o.IconSource = v);

        public static readonly AvaloniaProperty<int> IconSizeProperty =
            AvaloniaProperty.RegisterDirect<IllustratedTextControlTemplate, int>(nameof(IconSource), o => o.IconSize,
                (o, v) => o.IconSize = v);

        public static readonly AvaloniaProperty<string?> TextProperty =
            AvaloniaProperty.RegisterDirect<IllustratedTextControlTemplate, string?>(nameof(Text), o => o.Text,
                (o, v) => o.Text = v);

        private int _iconSize;

        private IImage? _iconSource;
        private string? _text;

        public IImage? IconSource
        {
            get => _iconSource;
            set => SetAndRaise(IconSourceProperty, ref _iconSource, value);
        }

        public int IconSize
        {
            get => _iconSize;
            set => SetAndRaise(IconSizeProperty, ref _iconSize, value);
        }

        public string? Text
        {
            get => _text;
            set => SetAndRaise(TextProperty, ref _text, value);
        }
    }
}