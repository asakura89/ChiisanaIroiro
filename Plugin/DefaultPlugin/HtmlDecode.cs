using System;
using Ayumi.Plugin;

namespace DefaultPlugin {
    public class HtmlDecode : IPlugin {
        public String ComponentName => "Html Decode";

        public String ComponentDesc => "Decode Html Entities to String";

        public Object Process(Object processArgs) => throw new NotImplementedException();
    }
}
