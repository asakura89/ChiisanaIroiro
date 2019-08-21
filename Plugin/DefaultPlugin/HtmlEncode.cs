using System;
using Ayumi.Plugin;

namespace DefaultPlugin {
    public class HtmlEncode : IPlugin {
        public String Name => "Html Encode";

        public String Desc => "Encode String to Html Entities";

        public Object Process(Object processArgs) => throw new NotImplementedException();
    }
}
