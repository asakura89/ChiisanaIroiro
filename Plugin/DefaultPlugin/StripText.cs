using System;
using Ayumi.Plugin;

namespace DefaultPlugin {
    public class StripText : IPlugin {
        public String Name => "Text Stripper";

        public String Desc => "Remove part of string";

        public Object Process(Object processArgs) {
            String input = Convert.ToString(processArgs);
            //StripTextConfig config = JsonConvert.SerializeObject(File.ReadAllText())
            return String.Empty;
        }
    }
}
