using System;
using Ayumi.Plugin;

namespace DefaultPlugin {
    public class HtmlEncode : IPlugin {
        public String ComponentName => "Html Encode";

        public String ComponentDesc => "Encode String to Html Entities";

        public Object Process(Object processArgs) => throw new NotImplementedException();
    }
}
