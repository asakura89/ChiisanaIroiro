using System;

namespace DefaultPlugin.Poco {
    public sealed class StripTextConfig {
        public Boolean RemoveExtraSpaces { get; set; }
        public Boolean RemoveSpaces { get; set; }
        public Boolean RemoveTabs { get; set; }
        public Boolean RemoveEmptyLines { get; set; }
        public Boolean RemoveDulicateLines { get; set; }
    }
}
