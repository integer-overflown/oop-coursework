using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using CourseWork.ViewModels;

namespace CourseWork.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            SetupPageChangeListeners();
        }

        private void SetupPageChangeListeners()
        {
            var pages = this.FindControlStrict<Carousel>("Pages");
            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.Changed.Subscribe(args =>
                {
                    if (args.PropertyName != nameof(viewModel.CurrentScreenIndex)) return;
                    pages.SelectedIndex = viewModel.CurrentScreenIndex;
                });
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            var menuItems = this.FindControlStrict<StackPanel>("MenuItemsSizeBar");
            for (var i = 0; i < menuItems.Children.Count; ++i)
            {
                AttachIndexUpdateOnClick(menuItems.Children[i], i);
            }
        }

        private void AttachIndexUpdateOnClick(IInputElement control, int index)
        {
            if (DataContext is MainWindowViewModel viewModel)
            {
                control.PointerPressed += (_, _) => { viewModel.CurrentScreenIndex = index; };
            }
        }
    }
}