using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Keywielder {
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

    public class Keywielder {
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
            [AlphaType.UpperSymbol] =  UppercaseAlphabet + " " + Symbol,
            [AlphaType.Upper] = UppercaseAlphabet,

            [AlphaType.LowerNumeric] =  LowercaseAlphabet + " " + Numeric,
            [AlphaType.LowerSymbol] =  LowercaseAlphabet + " " + Symbol,
            [AlphaType.Lower] = LowercaseAlphabet,

            [AlphaType.NumericSymbol] =  Numeric + " " + Symbol,
            [AlphaType.Numeric] = Numeric,

            [AlphaType.Symbol] = Symbol
        };

        readonly StringBuilder keyBuilder = new StringBuilder();

        Keywielder() { }
        public static Keywielder New() => new Keywielder();

        Int32 GetRandomNumber(Int32 lowerBound, Int32 upperBound) {
            Int32 seed = Guid.NewGuid().GetHashCode() % 50001;
            var rnd = new Random(seed);
            return rnd.Next(lowerBound, upperBound);
        }

        public Keywielder AddRandomString(Int32 valueLength) => AddRandomString(valueLength, AlphaType.Upper);

        public Keywielder AddRandomString(Int32 valueLength, AlphaType type) => AddRandomString(valueLength, type, String.Empty);

        public Keywielder AddRandomString(Int32 valueLength, AlphaType type, String backSeparator) => AddRandom(valueLength, alphaTypeDict[type].Split(' '), backSeparator);

        public Keywielder AddRandomNumber(Int32 valueLength) => AddRandomNumber(valueLength, String.Empty);

        public Keywielder AddRandomNumber(Int32 valueLength, String backSeparator) => AddRandomString(valueLength, AlphaType.Numeric, backSeparator);

        public Keywielder AddRandomAlphaNumeric(Int32 valueLength) => AddRandomAlphaNumeric(valueLength, true);

        public Keywielder AddRandomAlphaNumeric(Int32 valueLength, Boolean uppercase) => AddRandomAlphaNumeric(valueLength, uppercase, String.Empty);

        public Keywielder AddRandomAlphaNumeric(Int32 valueLength, Boolean uppercase, String backSeparator) =>
            uppercase ? AddRandomString(valueLength, AlphaType.Upper, backSeparator) : AddRandomString(valueLength, AlphaType.Lower, backSeparator);

        Keywielder AddRandom(Int32 valueLength, String[] charCombination, String backSeparator) {
            var randomString = new StringBuilder();
            for (Int32 i = 0; i < valueLength; i++) {
                Int32 randomIdx = GetRandomNumber(0, charCombination.Length - 1);
                randomString.Append(charCombination[randomIdx]);
            }

            keyBuilder.Append(randomString + backSeparator);
            return this;
        }

        public Keywielder AddGUIDString() => AddGUIDString(String.Empty);

        public Keywielder AddGUIDString(String backSeparator) {
            keyBuilder.Append(Guid.NewGuid().ToString("N") + backSeparator);
            return this;
        }

        public Keywielder AddString(String value, Int32 valueLength) => AddString(value, valueLength, String.Empty);

        public Keywielder AddString(String value, Int32 valueLength, String backSeparator) {
            String strWithLength = value.Substring(0, valueLength).ToUpper();
            keyBuilder.Append(strWithLength + backSeparator);
            return this;
        }

        public Keywielder AddShortYear() => AddYear(2, String.Empty);

        public Keywielder AddShortYear(String backSeparator) => AddYear(2, backSeparator);

        public Keywielder AddLongYear() => AddYear(4, String.Empty);

        public Keywielder AddLongYear(String backSeparator) => AddYear(4, backSeparator);

        Keywielder AddYear(Int32 valueLength, String backSeparator) {
            Int32 currentYear = DateTime.Now.Year;
            String yearWithLength = valueLength == 4 ? currentYear.ToString(CultureInfo.InvariantCulture) : currentYear.ToString(CultureInfo.InvariantCulture).Substring(2, 2);
            keyBuilder.Append(yearWithLength + backSeparator);
            return this;
        }

        public Keywielder AddShortMonth() => AddMonth(3, String.Empty);

        public Keywielder AddShortMonth(String backSeparator) => AddMonth(3, backSeparator);

        public Keywielder AddShortMonth(IList<String> customMonthList) => AddMonth(3, customMonthList, String.Empty);

        public Keywielder AddShortMonth(IList<String> customMonthList, String backSeparator) => AddMonth(3, customMonthList, backSeparator);

        public Keywielder AddLongMonth() => AddMonth(4, String.Empty);

        public Keywielder AddLongMonth(String backSeparator) => AddMonth(4, backSeparator);

        public Keywielder AddLongMonth(IList<String> customMonthList) => AddMonth(4, customMonthList, String.Empty);

        public Keywielder AddLongMonth(IList<String> customMonthList, String backSeparator) => AddMonth(4, customMonthList, backSeparator);

        public Keywielder AddNumericMonth() => AddMonth(2, String.Empty);

        public Keywielder AddNumericMonth(String backSeparator) => AddMonth(2, backSeparator);

        Keywielder AddMonth(Int32 valueLength, String backSeparator) {
            String[] defaultMonthList = { "", "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" };
            return AddMonth(valueLength, defaultMonthList, backSeparator);
        }

        Keywielder AddMonth(Int32 valueLength, IList<String> monthList, String backSeparator) {
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

        public Keywielder AddDate() => AddDate(String.Empty);

        public Keywielder AddDate(String backSeparator) => AddDate(0, backSeparator);

        public Keywielder AddDate(Int32 valueLength, String backSeparator) {
            keyBuilder.Append(DateTime.Now.Day.ToString().PadLeft(valueLength, '0') + backSeparator);
            return this;
        }

        public Keywielder AddShortDay() => AddDay(3, String.Empty);

        public Keywielder AddShortDay(String backSeparator) => AddDay(3, backSeparator);

        public Keywielder AddShortDay(IList<String> customDayList) => AddDay(3, customDayList, String.Empty);

        public Keywielder AddShortDay(IList<String> customDayList, String backSeparator) => AddDay(3, customDayList, backSeparator);

        public Keywielder AddLongDay() => AddDay(4, String.Empty);

        public Keywielder AddLongDay(String backSeparator) => AddDay(4, backSeparator);

        public Keywielder AddLongDay(IList<String> customDayList) => AddDay(4, customDayList, String.Empty);

        public Keywielder AddLongDay(IList<String> customDayList, String backSeparator) => AddDay(4, customDayList, backSeparator);

        public Keywielder AddNumericDay() => AddDay(2, String.Empty);

        public Keywielder AddNumericDay(String backSeparator) => AddDay(2, backSeparator);

        Keywielder AddDay(Int32 valueLength, String backSeparator) {
            String[] defaultDayList = { "", "SUNDAY", "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY" };
            return AddDay(valueLength, defaultDayList, backSeparator);
        }

        Keywielder AddDay(Int32 valueLength, IList<String> dayList, String backSeparator) {
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

        public Keywielder AddCounter(Int32 currentCounter, Int32 valueLength) => AddCounter(currentCounter, 1, valueLength, String.Empty);

        public Keywielder AddCounter(Int32 currentCounter, Int32 increment, Int32 valueLength) => AddCounter(currentCounter, increment, valueLength, String.Empty);

        public Keywielder AddCounter(Int32 currentCounter, Int32 valueLength, String backSeparator) => AddCounter(currentCounter, 1, valueLength, backSeparator);

        public Keywielder AddCounter(Int32 currentCounter, Int32 increment, Int32 valueLength, String backSeparator) {
            String counter = (currentCounter + increment).ToString(CultureInfo.InvariantCulture).PadLeft(valueLength, '0');
            keyBuilder.Append(counter + backSeparator);
            return this;
        }

        public String BuildKey() => keyBuilder.ToString();
    }
}