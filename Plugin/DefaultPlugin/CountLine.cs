using System;
using Ayumi.Plugin;

namespace DefaultPlugin {
    public class CountLine : IPlugin {
        public String Name => "Count Line";

        public String Desc => "Count lines of list";

        public Object Process(Object processArgs) {
            String input = Convert.ToString(processArgs);
            String[] lines = input
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            return lines.Length;
        }
    }
}
