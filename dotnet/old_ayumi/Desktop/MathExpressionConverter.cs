using System;
using System.Globalization;
using System.Windows.Data;
using SysConvert = System.Convert;

namespace Ayumi.Desktop {
    public class MathExpressionConverter : IValueConverter {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture) {
            Object res = new Object();

            String[] xpr = SysConvert.ToString(parameter).Split('|');

            switch (xpr[0]) {
                case "+":
                    res = SysConvert.ToDouble(value) + SysConvert.ToDouble(xpr[1]);
                    break;
                case "-":
                    res = SysConvert.ToDouble(value) - SysConvert.ToDouble(xpr[1]);
                    break;
                case "*":
                    res = SysConvert.ToDouble(value) * SysConvert.ToDouble(xpr[1]);
                    break;
                case "/":
                    res = SysConvert.ToDouble(value) / SysConvert.ToDouble(xpr[1]);
                    break;
            }

            return res;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture) => null;
    }
}