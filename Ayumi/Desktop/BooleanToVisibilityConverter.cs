using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Ayumi.Desktop {
    public class BooleanToVisibilityConverter : IValueConverter {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture) => (Boolean) value ? Visibility.Visible : Visibility.Collapsed;

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture) => null;
    }
}