using System;
using System.Windows.Media;

namespace Plugin.Common {
    public static class ColorGenerator {
        static String[] Colors = new[] {
            "#0068b3",
            "#f9aa89",
            "#e51937",
            "#c41230",
            "#ffd24f",
            "#f0b310",
            "#1ab7ea",
            "#005581",
            "#00704a",
            "#00a950",
            "#b3d88c"
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