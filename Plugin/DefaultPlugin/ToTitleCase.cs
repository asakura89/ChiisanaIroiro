using System;
using System.Globalization;
using Ayumi.Plugin;

namespace DefaultPlugin {
    public class ToTitleCase : IPlugin {
        public String Name => "To Title Case";

        public String Desc => "Change case to title case";

        public Object Process(Object processArgs) {
            String input = Convert.ToString(processArgs);
            TextInfo currentTextInfo = CultureInfo.CurrentCulture.TextInfo;
            return currentTextInfo.ToTitleCase(input);
        }
    }
}
