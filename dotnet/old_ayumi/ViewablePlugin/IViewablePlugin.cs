using System.Windows.Controls;
using Ayumi.Plugin;

namespace Ayumi.ViewablePlugin {
    public interface IViewablePlugin : IPlugin {
        UserControl View { get; }
    }
}
