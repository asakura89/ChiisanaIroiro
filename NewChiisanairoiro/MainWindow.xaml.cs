using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Ayumi.Plugin;
using Ayumi.ViewablePlugin;

namespace Chiisanairoiro {
    public partial class MainWindow : Window {
        public FeatureDropdownItem SelectedFeature { get; set; }

        public MainWindow() {
            InitializeComponent();
            InitializePlugins();
        }

        void InitializePlugins() {
            var loader = new DynamicTypeLoader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins"));
            IList<FeatureDropdownItem> infos = loader
                .GetTypesInheritedBy<IViewablePlugin>()
                .Select(pluginType => (IViewablePlugin) Activator.CreateInstance(pluginType))
                .Select(plugin => new FeatureDropdownItem { Name = $"{plugin.ComponentName}{(String.IsNullOrEmpty(plugin.ComponentDesc) ? String.Empty : " - " + plugin.ComponentDesc)}", Value = plugin })
                .OrderBy(plugin => plugin.Name)
                .ToList();

            AvailableFeaturesDropdownList.ItemsSource = infos;
            AvailableFeaturesDropdownList.DisplayMemberPath = "Name";
        }

        void AvailableFeaturesDropdownList_SelectionChanged(Object sender, SelectionChangedEventArgs e) {
            SelectedFeature = AvailableFeaturesDropdownList.SelectedValue as FeatureDropdownItem;
            if (SelectedFeature != null) {
                ComponentHost.Children.Clear();
                ComponentHost.Children.Add(SelectedFeature.Value.View);
            }
        }
    }
}
