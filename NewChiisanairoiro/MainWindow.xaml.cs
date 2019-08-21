using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Ayumi.Plugin;
using FastColoredTextBoxNS;

namespace NewChiisanairoiro {
    public partial class MainWindow : Window {
        readonly FastColoredTextBox inputTextbox = new FastColoredTextBox();
        readonly FastColoredTextBox outputTextbox = new FastColoredTextBox();

        public FeatureDropdownItem SelectedFeature { get; set; }

        public MainWindow() {
            InitializeComponent();
            InitializeInternalComponent();
            InitializePlugins();
        }

        void InitializeInternalComponent() {
            TextEditorHelper.Initialize(inputTextbox);
            TextEditorHelper.Initialize(outputTextbox);

            InputTextBoxHost.Child = inputTextbox;
            OutputTextBoxHost.Child = outputTextbox;
        }

        void InitializePlugins() {
            var loader = new DynamicTypeLoader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins"));
            IList<FeatureDropdownItem> infos = loader
                .GetTypesInheritedBy<IPlugin>()
                .Select(pluginType => (IPlugin) Activator.CreateInstance(pluginType))
                .Select(plugin => new FeatureDropdownItem { Name = $"{plugin.Name}{(String.IsNullOrEmpty(plugin.Desc) ? String.Empty : " - " + plugin.Desc)}", Value = plugin })
                .OrderBy(plugin => plugin.Name)
                .ToList();

            AvailableFeaturesDropdownList.ItemsSource = infos;
            AvailableFeaturesDropdownList.DisplayMemberPath = "Name";
        }

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            SelectedFeature = AvailableFeaturesDropdownList.SelectedValue as FeatureDropdownItem;
            if (SelectedFeature != null)
                outputTextbox.Text = SelectedFeature.Value.Process(inputTextbox.Text).ToString();
        }

        private void ConfigButton_Click(Object sender, RoutedEventArgs e) {

        }
    }
}
