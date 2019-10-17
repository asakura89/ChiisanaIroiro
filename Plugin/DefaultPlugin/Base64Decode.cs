using System;
using Ayumi.Plugin;

namespace DefaultPlugin {
    public class Base64Decode : IPlugin {
        public String ComponentName => "Base64 Decode";

        public String ComponentDesc => "Decode Base64 to String";

        public Object Process(Object processArgs) => throw new NotImplementedException();
    }
}
