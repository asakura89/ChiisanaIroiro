using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using RyaNG;

namespace KeywielderCore {
    public enum AlphaType {
        UpperLowerNumericSymbol,
        UpperLowerNumeric,
        UpperLowerSymbol,
        UpperLower,
        UpperNumeric,
        UpperSymbol,
        Upper,

        LowerNumeric,
        LowerSymbol,
        Lower,

        NumericSymbol,
        Numeric,
        Symbol
    }

    public class Wielder {
        const String UppercaseAlphabet = "A B C D E F G H I J K L M N O P Q R S T U V W X Y Z";
        const String LowercaseAlphabet = "a b c d e f g h i j k l m n o p q r s t u v w x y z";
        const String Numeric = "1 2 3 4 5 6 7 8 9 0";
        const String Symbol = "~ ! @ # $ % ^ & * _ - + = ` | \\ ( ) { } [ ] : ; < > . ? /";

        static readonly IDictionary<AlphaType, String> alphaTypeDict = new Dictionary<AlphaType, String> {
            [AlphaType.UpperLowerNumericSymbol] = UppercaseAlphabet + " " + LowercaseAlphabet + " " + Numeric + " " + Symbol,
            [AlphaType.UpperLowerNumeric] = UppercaseAlphabet + " " + LowercaseAlphabet + " " + Numeric,
            [AlphaType.UpperLowerSymbol] = UppercaseAlphabet + " " + LowercaseAlphabet + " " + Symbol,
            [AlphaType.UpperLower] = UppercaseAlphabet + " " + LowercaseAlphabet,
            [AlphaType.UpperNumeric] = UppercaseAlphabet + " " + Numeric,
            [AlphaType.UpperSymbol] = UppercaseAlphabet + " " + Symbol,
            [AlphaType.Upper] = UppercaseAlphabet,

            [AlphaType.LowerNumeric] = LowercaseAlphabet + " " + Numeric,
            [AlphaType.LowerSymbol] = LowercaseAlphabet + " " + Symbol,
            [AlphaType.Lower] = LowercaseAlphabet,

            [AlphaType.NumericSymbol] = Numeric + " " + Symbol,
            [AlphaType.Numeric] = Numeric,

            [AlphaType.Symbol] = Symbol
        };

        readonly StringBuilder keyBuilder = new StringBuilder();

        Wielder() { }
        public static Wielder New() => new Wielder();

        public Wielder AddRandomString(Int32 valueLength) => AddRandomString(valueLength, AlphaType.Upper);

        public Wielder AddRandomString(Int32 valueLength, AlphaType type) => AddRandomString(valueLength, type, String.Empty);

        public Wielder AddRandomString(Int32 valueLength, AlphaType type, String backSeparator) => AddRandom(valueLength, alphaTypeDict[type].Split(' '), backSeparator);

        public Wielder AddRandomNumber(Int32 valueLength) => AddRandomNumber(valueLength, String.Empty);

        public Wielder AddRandomNumber(Int32 valueLength, String backSeparator) => AddRandomString(valueLength, AlphaType.Numeric, backSeparator);

        public Wielder AddRandomAlphaNumeric(Int32 valueLength) => AddRandomAlphaNumeric(valueLength, true);

        public Wielder AddRandomAlphaNumeric(Int32 valueLength, Boolean uppercase) => AddRandomAlphaNumeric(valueLength, uppercase, String.Empty);

        public Wielder AddRandomAlphaNumeric(Int32 valueLength, Boolean uppercase, String backSeparator) =>
            uppercase ? AddRandomString(valueLength, AlphaType.Upper, backSeparator) : AddRandomString(valueLength, AlphaType.Lower, backSeparator);

        Wielder AddRandom(Int32 valueLength, String[] charCombination, String backSeparator) {
            var randomString = new StringBuilder();
            for (Int32 i = 0; i < valueLength; i++) {
                Int32 randomIdx = 0.TurnToRyandom(charCombination.Length -1);
                randomString.Append(charCombination[randomIdx]);
            }

            keyBuilder.Append(randomString + backSeparator);
            return this;
        }

        public Wielder AddGUIDString() => AddGUIDString(String.Empty);

        public Wielder AddGUIDString(String backSeparator) {
            keyBuilder.Append(Guid.NewGuid().ToString("N") + backSeparator);
            return this;
        }

        public Wielder AddString(String value, Int32 valueLength) => AddString(value, valueLength, String.Empty);

        public Wielder AddString(String value, Int32 valueLength, String backSeparator) {
            String strWithLength = value.Substring(0, valueLength).ToUpper();
            keyBuilder.Append(strWithLength + backSeparator);
            return this;
        }

        public Wielder AddShortYear() => AddYear(2, String.Empty);

        public Wielder AddShortYear(String backSeparator) => AddYear(2, backSeparator);

        public Wielder AddLongYear() => AddYear(4, String.Empty);

        public Wielder AddLongYear(String backSeparator) => AddYear(4, backSeparator);

        Wielder AddYear(Int32 valueLength, String backSeparator) {
            Int32 currentYear = DateTime.Now.Year;
            String yearWithLength = valueLength == 4 ? currentYear.ToString(CultureInfo.InvariantCulture) : currentYear.ToString(CultureInfo.InvariantCulture).Substring(2, 2);
            keyBuilder.Append(yearWithLength + backSeparator);
            return this;
        }

        public Wielder AddShortMonth() => AddMonth(3, String.Empty);

        public Wielder AddShortMonth(String backSeparator) => AddMonth(3, backSeparator);

        public Wielder AddShortMonth(IList<String> customMonthList) => AddMonth(3, customMonthList, String.Empty);

        public Wielder AddShortMonth(IList<String> customMonthList, String backSeparator) => AddMonth(3, customMonthList, backSeparator);

        public Wielder AddLongMonth() => AddMonth(4, String.Empty);

        public Wielder AddLongMonth(String backSeparator) => AddMonth(4, backSeparator);

        public Wielder AddLongMonth(IList<String> customMonthList) => AddMonth(4, customMonthList, String.Empty);

        public Wielder AddLongMonth(IList<String> customMonthList, String backSeparator) => AddMonth(4, customMonthList, backSeparator);

        public Wielder AddNumericMonth() => AddMonth(2, String.Empty);

        public Wielder AddNumericMonth(String backSeparator) => AddMonth(2, backSeparator);

        Wielder AddMonth(Int32 valueLength, String backSeparator) {
            String[] defaultMonthList = { "", "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" };
            return AddMonth(valueLength, defaultMonthList, backSeparator);
        }

        Wielder AddMonth(Int32 valueLength, IList<String> monthList, String backSeparator) {
            String month = String.Empty;
            Int32 currentMonth = DateTime.Now.Month;
            switch (valueLength) {
                case 4:
                    month = monthList[currentMonth];
                    break;
                case 3:
                    month = monthList[currentMonth].Substring(0, 3);
                    break;
                case 2:
                    month = currentMonth.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
                    break;
            }

            keyBuilder.Append(month + backSeparator);
            return this;
        }

        public Wielder AddDate() => AddDate(String.Empty);

        public Wielder AddDate(String backSeparator) => AddDate(0, backSeparator);

        public Wielder AddDate(Int32 valueLength, String backSeparator) {
            keyBuilder.Append(DateTime.Now.Day.ToString().PadLeft(valueLength, '0') + backSeparator);
            return this;
        }

        public Wielder AddShortDay() => AddDay(3, String.Empty);

        public Wielder AddShortDay(String backSeparator) => AddDay(3, backSeparator);

        public Wielder AddShortDay(IList<String> customDayList) => AddDay(3, customDayList, String.Empty);

        public Wielder AddShortDay(IList<String> customDayList, String backSeparator) => AddDay(3, customDayList, backSeparator);

        public Wielder AddLongDay() => AddDay(4, String.Empty);

        public Wielder AddLongDay(String backSeparator) => AddDay(4, backSeparator);

        public Wielder AddLongDay(IList<String> customDayList) => AddDay(4, customDayList, String.Empty);

        public Wielder AddLongDay(IList<String> customDayList, String backSeparator) => AddDay(4, customDayList, backSeparator);

        public Wielder AddNumericDay() => AddDay(2, String.Empty);

        public Wielder AddNumericDay(String backSeparator) => AddDay(2, backSeparator);

        Wielder AddDay(Int32 valueLength, String backSeparator) {
            String[] defaultDayList = { "", "SUNDAY", "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY" };
            return AddDay(valueLength, defaultDayList, backSeparator);
        }

        Wielder AddDay(Int32 valueLength, IList<String> dayList, String backSeparator) {
            String day = String.Empty;
            Int32 currentDayOfWeek = Convert.ToInt32(DateTime.Now.DayOfWeek) + 1;
            switch (valueLength) {
                case 4:
                    day = dayList[currentDayOfWeek];
                    break;
                case 3:
                    day = dayList[currentDayOfWeek].Substring(0, 3);
                    break;
                case 2:
                    day = currentDayOfWeek.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
                    break;
            }

            keyBuilder.Append(day + backSeparator);
            return this;
        }

        public Wielder AddCounter(Int32 currentCounter, Int32 valueLength) => AddCounter(currentCounter, 1, valueLength, String.Empty);

        public Wielder AddCounter(Int32 currentCounter, Int32 increment, Int32 valueLength) => AddCounter(currentCounter, increment, valueLength, String.Empty);

        public Wielder AddCounter(Int32 currentCounter, Int32 valueLength, String backSeparator) => AddCounter(currentCounter, 1, valueLength, backSeparator);

        public Wielder AddCounter(Int32 currentCounter, Int32 increment, Int32 valueLength, String backSeparator) {
            String counter = (currentCounter + increment).ToString(CultureInfo.InvariantCulture).PadLeft(valueLength, '0');
            keyBuilder.Append(counter + backSeparator);
            return this;
        }

        public String BuildKey() => keyBuilder.ToString();
    }
}