using System;
using System.Windows.Controls;
using Dyandra.View;
using Puru.Wpf;

namespace DyandraPlugin {
    public class DyandraView : IViewablePlugin {
        public String ComponentName => "Dyandra";

        public String ComponentDesc => "Stopwatch";

        public UserControl View => new DyandraWindow();

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");
    }
}
