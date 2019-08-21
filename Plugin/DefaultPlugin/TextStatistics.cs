using System;
using Ayumi.Plugin;

namespace DefaultPlugin {
    public class TextStatistics : IPlugin {
        public String Name => "Text Statistics";

        public String Desc => "Display text statistics"; // Words, Chars, Sentences

        public Object Process(Object processArgs) => throw new NotImplementedException();
    }
}
