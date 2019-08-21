using System;
using System.Collections.Generic;
using System.Linq;
using Ayumi.Plugin;

namespace DefaultPlugin {
    public class ExtractDirectoryName : IPlugin {
        public String Name => "Extract Directory Name";

        public String Desc => "Get directory name with configurable depth";

        public Object Process(Object processArgs) {
            String input = Convert.ToString(processArgs);
            IList<String> splitted = input
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                .ToList();

            //IList<String> extracted = 

            return ""; //output;
        }
    }
}
