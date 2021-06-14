using System.Windows.Input;
using Avalonia;

namespace CourseWork.Views.Widgets
{
    public class ImageButton : IllustratedTextControlTemplate
    {
        private const int DefaultIconSize = 24;

        public static readonly AvaloniaProperty<ICommand?> CommandProperty =
            AvaloniaProperty.RegisterDirect<ImageButton, ICommand?>(nameof(Command), o => o.Command,
                (o, v) => o.Command = v);

        private ICommand? _command;

        public ImageButton()
        {
            IconSize = DefaultIconSize;
        }

        public ICommand? Command
        {
            get => _command;
            set => SetAndRaise(CommandProperty, ref _command, value);
        }
    }
}