using System.IO;
using System.Reflection;
using System.Runtime.Loader;
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
            IAssemblyLoader asmLoader = new AssemblyLoader();
            foreach (String dir in pluginDirs)
                asmLoader.LoadFromPath(dir);

            IAssemblyHelper asmHelper = new AssemblyHelper(new TypeHelper());
            IEnumerable<IViewablePlugin> plugins = AssemblyLoadContext.All
                .SelectMany(ctx => asmHelper
                    .GetTypesInheritedBy<IViewablePlugin>(ctx.Assemblies)
                    .Where(type => !type.IsAbstract && !type.IsInterface)
                    .Select(pluginType => (IViewablePlugin) Activator.CreateInstance(pluginType)))
                    .ToList();

            // ^ always empty because of IViewablePlugin is in Default LoadContext while the plugin is in theirs 😔
            /*
            AssemblyLoadContext.All.Take(2).Skip(1).First().Assemblies.Take(2).Skip(1).First()
            {DirectoryProcessingPlugin, Version=2.0.2021.7522, Culture=neutral, PublicKeyToken=null}

            a.ExportedTypes.First().GetType()
            {System.RuntimeType}

            a.ExportedTypes.First()
            {DirectoryProcessingPlugin.DirListView}

            AssemblyLoadContext.GetLoadContext(a.ExportedTypes.First().Assembly)
            {"DirectoryProcessingPlugin" Reflx.DynamicAssemblyLoadContext #1}

            AssemblyLoadContext.GetLoadContext(typeof(IViewablePlugin).Assembly)
            {"Default" System.Runtime.Loader.DefaultAssemblyLoadContext #0}

            typeof(IViewablePlugin).IsAssignableFrom(a.ExportedTypes.First())
            false

            a.ExportedTypes.First() as IViewablePlugin
            null

            AssemblyLoadContext.All.Take(2).Skip(1).First().Assemblies
            {System.Runtime.Loader.AssemblyLoadContext.<get_Assemblies>d__57}

            AssemblyLoadContext.All.Take(2).Skip(1).First().Assemblies.Last().ExportedTypes.First().IsAssignableFrom(a.ExportedTypes.First())
            true
            */

            IList<FeatureDropdownItem> infos = plugins
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