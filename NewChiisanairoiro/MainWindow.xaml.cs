using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Puru;
using Puru.Wpf;

namespace Chiisanairoiro {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            InitializePlugins();
        }

        void InitializePlugins() {
            String pluginsRootDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
            IList<String> pluginDirs = Directory.EnumerateDirectories(pluginsRootDir).ToList();
            IList<FeatureDropdownItem> infos = pluginDirs
                .SelectMany(dir =>
                    new DynamicTypeLoader(dir)
                        .LoadAssemblies()
                        .GetTypesInheritedBy<IViewablePlugin>()
                        .Where(type => !type.IsAbstract && !type.IsInterface)
                        .Select(pluginType => (IViewablePlugin) Activator.CreateInstance(pluginType))
                )
                .Select(plugin => new FeatureDropdownItem {
                    Name = $"[{plugin.ComponentName}]{(String.IsNullOrEmpty(plugin.ComponentDesc) ? String.Empty : " " + plugin.ComponentDesc)}",
                    Value = plugin
                })
                .OrderBy(plugin => plugin.Name)
                .ToList();

            AvailableFeaturesDropdownList.ItemsSource = infos;
            AvailableFeaturesDropdownList.DisplayMemberPath = "Name";
        }

        void AvailableFeaturesDropdownList_SelectionChanged(Object sender, SelectionChangedEventArgs e) {
            var selected = AvailableFeaturesDropdownList.SelectedValue as FeatureDropdownItem;
            if (selected != null) {
                ComponentHost.Children.Clear();
                ComponentHost.Children.Add(selected.Value.View);
            }
        }
    }
}