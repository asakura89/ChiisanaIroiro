using System.Windows.Controls;
using Puru;

namespace Puru.Wpf {
    public interface IViewablePlugin : IPlugin {
        UserControl View { get; }
    }
}
