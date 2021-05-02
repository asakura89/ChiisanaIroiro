using System;
using System.Windows.Media;

namespace Plugin.Common {
    public static class ColorGenerator {
        static String[] Colors = new[] {
            "#70c2b4",
            "#b3dbbf",
            "#f3ffbd",
            "#ff1453",
            "#00a897",
            "#02c59b",
            "#f1f3be",
            "#2ec2b3",
            "#e71d34",
            "#ff9f1a",
            "#a7dadc",
            "#b3d88c",
            "#00a950",
            "#1ab7ea",
            "#f0b310",
            "#ffd24f",
            "#c41230",
            "#e51937",
            "#f9aa89",
            "#5ea8de",
            "#8dbf35",
            "#b8d544",
            "#d8c967",
            "#f18a2b",
            "#f7866a",
            "#ed7099",
            "#7b4f9d",
            "#ce4039"
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