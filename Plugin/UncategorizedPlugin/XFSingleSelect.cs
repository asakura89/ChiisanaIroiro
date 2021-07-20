using System;
using System.Collections.Generic;

namespace UncategorizedPlugin {
    public class XFSingleSelect : XFControl {
        public IList<NameValueItem> Children { get; set; }
        public String Value { get; set; }
    }
}