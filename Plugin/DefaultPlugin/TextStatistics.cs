using System;
using Ayumi.Plugin;

namespace DefaultPlugin {
    public class TextStatistics : IPlugin {
        public String ComponentName => "Text Statistics";

        public String ComponentDesc => "Display text statistics"; // Words, Chars, Sentences

        public Object Process(Object processArgs) => throw new NotImplementedException();
    }
}
