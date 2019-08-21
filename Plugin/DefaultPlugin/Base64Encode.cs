using System;
using Ayumi.Plugin;

namespace DefaultPlugin {
    public class Base64Encode : IPlugin {
        public String Name => "Base64 Encode";

        public String Desc => "Encode String to Base64";

        public Object Process(Object processArgs) => throw new NotImplementedException();
    }
}
