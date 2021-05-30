using System;
using System.Diagnostics.CodeAnalysis;
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
    }
}