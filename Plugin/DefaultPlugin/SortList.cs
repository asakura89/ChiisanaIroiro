using System;
using System.Linq;
using Ayumi.Plugin;

namespace DefaultPlugin {
    public class SortList : IPlugin {
        public String Name => "Sort List";

        public String Desc => "Sort list of strings";

        public Object Process(Object processArgs) {
            String input = Convert.ToString(processArgs);
            String output = String.Join(Environment.NewLine, input
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                .OrderBy(str => str));

            return output;
        }
    }
}
