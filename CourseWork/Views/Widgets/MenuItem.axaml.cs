using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace CourseWork.Views.Widgets
{
    public class MenuItem : TemplatedControl
    {
        public static readonly AvaloniaProperty<IImage?> IconSourceProperty =
            AvaloniaProperty.RegisterDirect<MenuItem, IImage?>(nameof(IconSource), o => o.IconSource,
                (o, v) => o.IconSource = v);

        public static readonly AvaloniaProperty<int> IconSizeProperty =
            AvaloniaProperty.RegisterDirect<MenuItem, int>(nameof(IconSource), o => o.IconSize,
                (o, v) => o.IconSize = v);

        public static readonly AvaloniaProperty<string?> ItemNameProperty =
            AvaloniaProperty.RegisterDirect<MenuItem, string?>(nameof(ItemName), o => o.ItemName,
                (o, v) => o.ItemName = v);

        public static readonly AvaloniaProperty<bool> IsSelectedProperty =
            AvaloniaProperty.RegisterDirect<MenuItem, bool>(nameof(IsSelected), o => o.IsSelected,
                (o, v) => o.IsSelected = v);

        private int _iconSize = 32;
        private IImage? _iconSource;
        private bool _isSelected;
        private string? _itemName;

        public bool IsSelected
        {
            get => _isSelected;
            set => SetAndRaise(IsSelectedProperty, ref _isSelected, value);
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

        public string? ItemName
        {
            get => _itemName;
            set => SetAndRaise(ItemNameProperty, ref _itemName, value);
        }
    }
}