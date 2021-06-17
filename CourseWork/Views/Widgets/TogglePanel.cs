using System.Collections.Specialized;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace CourseWork.Views.Widgets
{
    public class TogglePanel : StackPanel
    {
        private ToggleButton? _checked;

        public TogglePanel()
        {
            Children.CollectionChanged += ChildrenOnCollectionChanged;
        }

        private void ChildrenOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Add || e.NewItems is null) return;
            foreach (var child in e.NewItems.OfType<ToggleButton>())
            {
                child.Checked += (_, _) =>
                {
                    if (_checked is not null)
                        _checked.IsChecked = false;
                    _checked = child;
                };
                child.Unchecked += (_, _) =>
                {
                    if (ReferenceEquals(child, _checked)) _checked = null;
                };
            }
        }

        public void UncheckAll()
        {
            if (_checked is null) return;
            _checked.IsChecked = false;
            _checked = null;
        }
    }
}