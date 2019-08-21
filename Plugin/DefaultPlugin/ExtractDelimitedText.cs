using System;
using Ayumi.Plugin;

namespace DefaultPlugin {
    public class ExtractDelimitedText : IPlugin {
        public String Name => "Extract Delimited Text";

        public String Desc => "Extract spesific column from delimited text";

        public Object Process(Object processArgs) => throw new NotImplementedException();
    }
}
