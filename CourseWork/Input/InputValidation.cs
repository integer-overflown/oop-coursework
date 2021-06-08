using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace CourseWork.Input
{
    public class InputValidation : AvaloniaObject
    {
        public enum Filter
        {
            Digits,
            Isbn
        }

        public static readonly AvaloniaProperty<Filter?> InputFilterProperty =
            AvaloniaProperty.RegisterAttached<InputValidation, TextBox, Filter?>("InputFilter", null,
                coerce: InstallFilter);

        public static Filter? InstallFilter(IAvaloniaObject element, Filter? filter)
        {
            if (filter is null) return null;
            var textBox = (TextBox) element;
            IValidator<string> textValidator = MapTextValidatorToFilter(filter);

            textBox.AddHandler(InputElement.TextInputEvent, (_, args) =>
            {
                Console.WriteLine($"Got {args.Text}");
                if (args.Text != null && !textValidator.IsPermitted(args.Text))
                {
                    args.Handled = true; // consume the event
                }
            }, RoutingStrategies.Tunnel);

            return filter;
        }

        private static IValidator<string> MapTextValidatorToFilter(Filter? filter)
        {
            return filter switch
            {
                Filter.Isbn => Validators.Isbn,
                Filter.Digits => Validators.Digits,
                _ => throw new ArgumentException("Cannot map validator to the filter", nameof(filter), null)
            };
        }

        public static void SetInputFilter(AvaloniaObject element, Filter? commandValue)
        {
            element.SetValue(InputFilterProperty, commandValue);
        }

        public static Filter? GetInputFilter(AvaloniaObject element)
        {
            return (Filter?) element.GetValue(InputFilterProperty);
        }
    }
}