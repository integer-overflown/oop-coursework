using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Media;

namespace CourseWork.Views.Widgets
{
    public class DecoratedTextBox : TemplatedControl
    {
        private const int DefaultIconSize = 24;

        public static readonly StyledProperty<string?> WatermarkProperty =
            TextBox.WatermarkProperty.AddOwner<DecoratedTextBox>();

        public static readonly DirectProperty<DecoratedTextBox, string?> TextProperty =
            AvaloniaProperty.RegisterDirect<DecoratedTextBox, string?>(nameof(Text), o => o.Text, (o, v) => o.Text = v,
                defaultBindingMode: BindingMode.TwoWay);

        public static readonly AvaloniaProperty<IImage?> IconSourceProperty =
            AvaloniaProperty.RegisterDirect<DecoratedTextBox, IImage?>(nameof(IconSource),
                o => o.IconSource,
                (o, v) => o.IconSource = v);

        public static readonly AvaloniaProperty<int> IconSizeProperty =
            AvaloniaProperty.RegisterDirect<DecoratedTextBox, int>(nameof(IconSource), o => o.IconSize,
                (o, v) => o.IconSize = v);

        private int _iconSize;

        private IImage? _iconSource;

        private string? _text;


        public DecoratedTextBox()
        {
            IconSize = DefaultIconSize;
        }

        public string? Watermark
        {
            get => GetValue(WatermarkProperty);
            set => SetValue(WatermarkProperty, value);
        }

        public string? Text
        {
            get => _text;
            set => SetAndRaise(TextProperty, ref _text, value);
        }

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
    }
}