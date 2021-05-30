using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using Bitmap = Avalonia.Media.Imaging.Bitmap;
using Image = Avalonia.Controls.Image;

namespace CourseWork.Views.Widgets
{
    public class MenuItem : UserControl
    {
        private readonly Image _icon;
        private readonly TextBlock _name;
        
        private string _iconSource;
        private string _itemName = "";
        private int _iconSize = 32;
        
        private const string RootDirectory = "CourseWork";

        public MenuItem()
        {
            InitializeComponent();
            _icon = this.FindControlStrict<Image>("Icon");
            _name = this.FindControlStrict<TextBlock>("ItemName");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public static readonly DirectProperty<MenuItem, string> IconSourceProperty =
            AvaloniaProperty.RegisterDirect<MenuItem, string>(nameof(IconSource), o => o.IconSource,
                (o, v) => o.IconSource = v);

        public static readonly DirectProperty<MenuItem, int> IconSizeProperty =
            AvaloniaProperty.RegisterDirect<MenuItem, int>(nameof(IconSource), o => o.IconSize,
                (o, v) => o.IconSize = v);

        public static readonly DirectProperty<MenuItem, string> ItemNameProperty =
            AvaloniaProperty.RegisterDirect<MenuItem, string>(nameof(ItemName), o => o.ItemName,
                (o, v) => o.ItemName = v);
        
        public string IconSource
        {
            get => _iconSource;
            set
            {
                _icon.Source = new Bitmap(GetAssetStream(value));
                _icon.Width = IconSize;
                _icon.Height = IconSize;
                SetAndRaise(IconSourceProperty, ref _iconSource, value);
            }
        }

        private static System.IO.Stream GetAssetStream(string name)
        {
            var assetLoader = AvaloniaLocator.Current.GetService<IAssetLoader>();
            var source = $"avares://{RootDirectory}/Assets/{name}";
            Console.WriteLine(source);
            return assetLoader.Open(new Uri(source));
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

        public string ItemName
        {
            get => _itemName;
            set
            {
                _name.Text = value;
                SetAndRaise(ItemNameProperty, ref _itemName, value);
            }
        }
    }
}