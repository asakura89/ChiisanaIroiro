using System;
using Ayumi.Plugin;

namespace DefaultPlugin {
    public class HtmlDecode : IPlugin {
        public String Name => "Html Decode";

        public String Desc => "Decode Html Entities to String";

        public Object Process(Object processArgs) => throw new NotImplementedException();
    }
}
