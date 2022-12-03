using System.IO;
using System.Windows;
using System.Windows.Controls;
using Puru.Wpf;
using Reflx;

namespace Chiisanairoiro {
    public partial class MainWindow : Window {
        IList<FeatureDropdownItem> features;

        public MainWindow() {
            InitializeComponent();
            InitializePlugins();
        }

        void InitializePlugins() {
            String pluginsRootDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
            IList<String> pluginDirs = Directory.EnumerateDirectories(pluginsRootDir).ToList();
            foreach (String dir in pluginDirs)
                new AssemblyLoader()
                        .LoadFromPath(dir, new[] { "*" });

            IList<FeatureDropdownItem> infos = new AssemblyHelper(new TypeHelper())
                .GetTypesInheritedBy<IViewablePlugin>(
                    AppDomain
                        .CurrentDomain
                        .GetAssemblies())
                .Where(type => !type.IsAbstract && !type.IsInterface)
                .Select(pluginType => (IViewablePlugin) Activator.CreateInstance(pluginType))
                // ^ don't know why this is always returns null IEnumerable 😔
                .Select(plugin => new FeatureDropdownItem {
                    Name = $"[{plugin.ComponentName}]{(String.IsNullOrEmpty(plugin.ComponentDesc) ? String.Empty : " " + plugin.ComponentDesc)}",
                    Value = plugin
                })
                .OrderBy(plugin => plugin.Name)
                .ToList();

            features = infos;

            AvailableFeaturesDropdownList.ItemsSource = features;
            AvailableFeaturesDropdownList.DisplayMemberPath = "Name";
        }

        void AvailableFeaturesDropdownList_SelectionChanged(Object sender, SelectionChangedEventArgs e) {
            var selected = AvailableFeaturesDropdownList.SelectedValue as FeatureDropdownItem;
            if (selected != null) {
                ComponentHost.Children.Clear();
                ComponentHost.Children.Add(selected.Value.View);
            }
        }

        void SearchTextbox_OnTextChanged(Object sender, TextChangedEventArgs e) {
            if (String.IsNullOrEmpty(SearchTextbox.Text)) {
                AvailableFeaturesDropdownList.ItemsSource = features;
                AvailableFeaturesDropdownList.SelectedIndex = -1;
                return;
            }

            AvailableFeaturesDropdownList.ItemsSource = features
                .Where(feature => feature.Name.ToLowerInvariant().Contains(SearchTextbox.Text.ToLowerInvariant()))
                .ToList();

            AvailableFeaturesDropdownList.SelectedIndex = 0;
        }

        void ClearButton_Click(Object sender, RoutedEventArgs e) => SearchTextbox.Text = String.Empty;
    }
}