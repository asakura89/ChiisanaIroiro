using System;
using Ayumi.Plugin;

namespace DefaultPlugin {
    public class ExtractDelimitedText : IPlugin {
        public String ComponentName => "Extract Delimited Text";

        public String ComponentDesc => "Extract spesific column from delimited text";

        public Object Process(Object processArgs) => throw new NotImplementedException();
    }
}
