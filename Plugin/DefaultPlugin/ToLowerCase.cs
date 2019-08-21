using System;
using System.Globalization;
using Ayumi.Plugin;

namespace DefaultPlugin {
    public class ToLowerCase : IPlugin {
        public String Name => "To Lower Case";

        public String Desc => "Change case to lower case";

        public Object Process(Object processArgs) {
            String input = Convert.ToString(processArgs);
            TextInfo currentTextInfo = CultureInfo.CurrentCulture.TextInfo;
            return currentTextInfo.ToLower(input);
        }
    }
}
