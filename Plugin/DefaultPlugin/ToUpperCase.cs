using System;
using System.Globalization;
using Ayumi.Plugin;

namespace DefaultPlugin {
    public class ToUpperCase : IPlugin {
        public String Name => "To Upper Case";

        public String Desc => "Change case to upper case";

        public Object Process(Object processArgs) {
            String input = Convert.ToString(processArgs);
            TextInfo currentTextInfo = CultureInfo.CurrentCulture.TextInfo;
            return currentTextInfo.ToUpper(input);
        }
    }
}
