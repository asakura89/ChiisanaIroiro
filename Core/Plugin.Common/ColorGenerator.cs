using System;
using System.Windows.Media;

namespace Plugin.Common {
    public static class ColorGenerator {
        static String[] Colors = new[] {
            "#b8d6d8",
            "#bfe9ff",
            "#fdeacc",
            "#f7d6cf",
            "#f29da4"
        };

        public static Color GetColor() {
            Int32 index = Colors.Length.TurnToRyandom();
            String color = Colors[index];
            Object realColor = ColorConverter.ConvertFromString(color);
            if (realColor == null)
                return System.Windows.Media.Colors.Black;

            return (Color) realColor;
        }
    }
}