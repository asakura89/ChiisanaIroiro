using System;
using System.Globalization;
using System.Windows.Data;

namespace Ayumi.Desktop {
    public class BooleanInverter : IValueConverter {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture) => !(Boolean) value;

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture) => !(Boolean) value;
    }
}