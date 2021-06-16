using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace CourseWork.Input
{
    public class IntIsEqualConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool) || value is not int) return BindingNotification.UnsetValue;
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return BindingNotification.UnsetValue;
        }
    }
}