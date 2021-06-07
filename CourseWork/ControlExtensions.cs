using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Avalonia.Controls;

namespace CourseWork
{
    public static class ControlExtensions
    {
        [return: NotNull]
        public static T FindControlStrict<T>(this IControl control, string name) where T : class, IControl
        {
            return control.FindControl<T>(name) ??
                   throw new ArgumentException($"Failed to find the control with name {name}");
        }

        public static void SelectScreenByName(this Carousel carousel, string name)
        {
            carousel.SelectedItem = carousel.Items
                .OfType<IControl>()
                .FirstOrDefault(control => control.Name == name);
        }
    }
}