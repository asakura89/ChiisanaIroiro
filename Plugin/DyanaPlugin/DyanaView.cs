using System;
using System.Windows.Controls;
using Dyana.View;
using Puru.Wpf;

namespace DyanaPlugin {
    public class DyanaView : IViewablePlugin {
        public String ComponentName => "Dyana";

        public String ComponentDesc => "Calculate work hours";

        public UserControl View => new DyanaWindow();

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");
    }
}
