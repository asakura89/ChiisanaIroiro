using System;
using System.Linq;
using Ayumi.Plugin;

namespace DefaultPlugin {
    public class SortListDesc : IPlugin {
        public String Name => "Sort List Desc";

        public String Desc => "Sort list of strings descending";

        public Object Process(Object processArgs) {
            String input = Convert.ToString(processArgs);
            String output = String.Join(Environment.NewLine, input
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                .OrderByDescending(str => str));

            return output;
        }
    }
}
