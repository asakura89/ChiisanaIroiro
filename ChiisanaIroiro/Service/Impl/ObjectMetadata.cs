using System;
using System.Collections.Generic;

namespace ChiisanaIroiro.Service.Impl {
    public class ObjectMetadata {
        public String Name { get; set; }
        public IEnumerable<String> Properties { get; set; } = new List<String>();
    }
}