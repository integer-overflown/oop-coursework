using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace CourseWork.Views
{
    public partial class MainWindow : Window
    {
        private Carousel _screens;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _screens = this.FindControl<Carousel>("Screens");
        }
        
        
        public void OnPreviousClicked()
        {
            _screens.Previous();
        }

        private void Next_OnClick(object? sender, RoutedEventArgs e)
        {
            _screens.Next();
        }

        private void Previous_OnClick(object? sender, RoutedEventArgs e)
        {
            _screens.Previous();
        }
    }
}