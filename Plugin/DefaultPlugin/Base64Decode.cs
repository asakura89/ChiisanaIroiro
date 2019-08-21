using System;
using Ayumi.Plugin;

namespace DefaultPlugin {
    public class Base64Decode : IPlugin {
        public String Name => "Base64 Decode";

        public String Desc => "Decode Base64 to String";

        public Object Process(Object processArgs) => throw new NotImplementedException();
    }
}
