using System.IO;
using System.Reflection;
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
            AppDomain.CurrentDomain.AssemblyResolve += Resolve;

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

        Assembly Resolve(Object sender, ResolveEventArgs args) {
            try {
                if (args.Name.Contains(".resources"))
                    return null;

                Assembly asm = AppDomain
                    .CurrentDomain
                    .GetAssemblies()
                    .FirstOrDefault(a => a.FullName == args.Name);

                System.Diagnostics.Debug.WriteLine(asm == null ? $"'{args.Name}' is not found." : $"'{args.Name}' is found.");

                if (asm != null)
                    return asm;
            }
            catch {
                return null;
            }

            String[] parts = args.Name.Split(',');
            String fileName = parts[0].Trim().Replace(".dll", String.Empty) + ".dll";
            String fileDir = Path.GetDirectoryName(args.RequestingAssembly.Location) ?? "\\";
            // ^ in .NET 6, Assembly.Location is always empty string if the assembly loaded dynamically 😔
            String fullFilePath = Path.Combine(fileDir, fileName);
            System.Diagnostics.Debug.WriteLine($"Loading '{args.Name}' from '{fullFilePath}'.");
            if (!File.Exists(fullFilePath)) {
                System.Diagnostics.Debug.WriteLine($"'{args.Name}' still not found in '{fullFilePath}'.");
                return null;
            }

            return Assembly.Load(fullFilePath);
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