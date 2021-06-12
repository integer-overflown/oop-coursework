using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using CourseWork.Utils;

namespace CourseWork.Views.Widgets
{
    public class MenuItem : UserControl
    {
        public static readonly DirectProperty<MenuItem, string?> IconSourceProperty =
            AvaloniaProperty.RegisterDirect<MenuItem, string?>(nameof(IconSource), o => o.IconSource,
                (o, v) => o.IconSource = v);

        public static readonly DirectProperty<MenuItem, int> IconSizeProperty =
            AvaloniaProperty.RegisterDirect<MenuItem, int>(nameof(IconSource), o => o.IconSize,
                (o, v) => o.IconSize = v);

        public static readonly DirectProperty<MenuItem, string?> ItemNameProperty =
            AvaloniaProperty.RegisterDirect<MenuItem, string?>(nameof(ItemName), o => o.ItemName,
                (o, v) => o.ItemName = v);

        private readonly Image _icon;
        private readonly TextBlock _name;
        private int _iconSize = 32;

        private string? _iconSource;
        private string? _itemName;

        public MenuItem()
        {
            InitializeComponent();
            _icon = this.FindControlStrict<Image>("Icon");
            _name = this.FindControlStrict<TextBlock>("ItemName");
        }

        public string? IconSource
        {
            get => _iconSource;
            set
            {
                if (value is null)
                {
                    _icon.Source = null;
                    return;
                }

                _icon.Source = new Bitmap(AssetLoader.GetResourceAsStream(value));
                _icon.Width = IconSize;
                _icon.Height = IconSize;
                SetAndRaise(IconSourceProperty, ref _iconSource, value);
            }
        }

        public int IconSize
        {
            get => _iconSize;
            set
            {
                _icon.Width = value;
                _icon.Height = value;
                SetAndRaise(IconSizeProperty, ref _iconSize, value);
            }
        }

        public string? ItemName
        {
            get => _itemName;
            set
            {
                _name.Text = value;
                SetAndRaise(ItemNameProperty, ref _itemName, value);
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}