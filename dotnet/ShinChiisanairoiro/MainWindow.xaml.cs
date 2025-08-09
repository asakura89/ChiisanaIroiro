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
        IList<FeatureDropdownItem> features;

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