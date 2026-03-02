using System;
using System.Globalization;
using System.Windows.Data;
using SysConvert = System.Convert;

namespace Ayumi.Desktop {
    public class PercentageConverter : IValueConverter {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture) => SysConvert.ToDouble(value) * (SysConvert.ToDouble(parameter) / 100);

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture) => null;
    }
}