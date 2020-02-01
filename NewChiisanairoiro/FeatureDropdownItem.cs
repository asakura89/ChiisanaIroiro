using System;
using Ayumi.ViewablePlugin;

namespace Chiisanairoiro {
    public sealed class FeatureDropdownItem {
        public String Name { get; set; }
        public IViewablePlugin Value { get; set; }
    }
}
