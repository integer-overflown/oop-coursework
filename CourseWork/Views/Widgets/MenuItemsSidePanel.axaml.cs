using System.Collections.Specialized;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace CourseWork.Views.Widgets
{
    public class MenuItemsSidePanel : StackPanel
    {
        private MenuItem? _selected;
        private int _selectedIndex;

        public MenuItemsSidePanel()
        {
            InitializeComponent();
            Children.CollectionChanged += ChildAdded;
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (value >= Children.Count) return;
                var child = (MenuItem) Children[value];
                if (_selected != null) _selected.IsSelected = false;
                child.IsSelected = true;
                _selected = child;
                _selectedIndex = value;
            }
        }

        private void ChildAdded(object? sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action != NotifyCollectionChangedAction.Add || args.NewItems is null ||
                args.NewItems.Count < 1) return;
            _selected ??= (MenuItem?) args.NewItems[0];
            foreach (var child in args.NewItems.OfType<MenuItem>())
            {
                if (child.IsSelected) _selected = child;
                child.PointerPressed += ChildClicked;
            }
        }

        private void ChildClicked(object? sender, PointerPressedEventArgs args)
        {
            if (sender is null) return;
            var item = (MenuItem) sender;
            if (_selected != null) _selected.IsSelected = false;
            item.IsSelected = true;
            _selected = item;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}