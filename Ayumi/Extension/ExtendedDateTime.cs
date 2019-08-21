using System;

namespace Ayumi.Extension {
    public static class ExtendedDateTime {
        public const String DefaultSqlDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        public static DateTime DefaultSqlDateTime = new DateTime(1753, 1, 1);

        public static Boolean IsMinSqlDateTime(this DateTime dateToConfirm) =>
            dateToConfirm.CompareTo(DefaultSqlDateTime) == 0;

        public static String ToSqlCompatibleFormatString(this DateTime dateToFormat) =>
            dateToFormat.ToString(DefaultSqlDateTimeFormat);
    }
}