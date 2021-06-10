using System;
using System.Reactive;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace CourseWork.Views.Widgets
{
    public class PlaceholderImageButton : Button, IStyleable
    {
        public static readonly StyledProperty<object?> ActualContentProperty =
            AvaloniaProperty.Register<PlaceholderImageButton, object?>(nameof(ActualContent));

        private object? _actualContent;
        private object? _placeholder;

        public PlaceholderImageButton()
        {
            InitializeComponent();
            IDisposable? subscription = null;
            subscription = ContentProperty.Changed.Subscribe(Observer.Create<object?>(_ =>
            {
                _placeholder = Content;
                // ReSharper disable once AccessToModifiedClosure
                subscription!.Dispose();
            }));
        }

        public object? ActualContent
        {
            get => _actualContent;
            set
            {
                Content = value ?? _placeholder;
                SetAndRaise(ActualContentProperty, ref _actualContent, value);
            }
        }

        // inherit parent's style for children to be displayed properly
        Type IStyleable.StyleKey => typeof(Button);

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}