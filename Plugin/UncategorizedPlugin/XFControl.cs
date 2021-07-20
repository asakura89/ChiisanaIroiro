using System;

namespace UncategorizedPlugin {
    public abstract class XFControl {
        public String Type { get; set; }
        public String Id { get; set; }
        public String Label { get; set; }
        public String DefaultValue { get; set; }
        public Boolean Persist { get; set; }
        public String Value { get; set; }
    }
}