using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Puru;
using Puru.Wpf;

namespace GenerateRandomPlugin {
    public partial class GenerateFakeDataView : UserControl, IViewablePlugin {
        public String ComponentName => "Generate Dake Data";

        public String ComponentDesc => "Generate various fake data";

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        String Pattern => PatternTextBox.Text;

        public GenerateFakeDataView() {
            InitializeComponent();
            CommonView.HideAllButton();
        }

        void ClipboardButton_Click(Object sender, RoutedEventArgs e) {
            if (CommonView.Output != String.Empty) {
                Clipboard.Clear();
                Clipboard.SetText(CommonView.Output);
            }
        }

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            //IEnumerable<Type> generatorTypes = AppDomain
            //    .CurrentDomain
            //    .GetAssemblies()
            //    .GetTypesInheritedBy<IDataGenerator>()
            //    .Where(type => !type.IsAbstract && !type.IsInterface);

            //IEnumerable<DataGeneratorAttribute> keywords = generatorTypes
            //    .GetDecorators<DataGeneratorAttribute>()
            //    .Select(pluginType => (IDataGenerator) Activator.CreateInstance(pluginType));

        }

        void PatternListButton_Click(Object sender, RoutedEventArgs e) {

        }
    }
}
