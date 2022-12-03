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
            IDefaultAssemblyResolver asmResolver = new DefaultAssemblyResolver();
            AppDomain.CurrentDomain.AssemblyResolve += asmResolver.Resolve;
            // ^ there is an error here because it uses Assembly.Location internally which is not reliable in .NET 6 😔

            String pluginsRootDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
            IList<String> pluginDirs = Directory.EnumerateDirectories(pluginsRootDir).ToList();
            IAssemblyLoader asmLoader = new AssemblyLoader();
            foreach (String dir in pluginDirs)
                asmLoader.LoadFromPath(dir);

            IList<FeatureDropdownItem> infos = new AssemblyHelper(new TypeHelper())
                .GetTypesInheritedBy<IViewablePlugin>(
                    AppDomain
                        .CurrentDomain
                        .GetAssemblies())
                .Where(type => !type.IsAbstract && !type.IsInterface)
                .Select(pluginType => (IViewablePlugin) Activator.CreateInstance(pluginType))
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