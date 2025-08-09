using System;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Puru.Wpf;

namespace TextProcessingPlugin {
    public partial class FormatJsonView : UserControl, IViewablePlugin {
        public String ComponentName => "Format JSON";

        public String ComponentDesc => "Beautify JSON String";

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        public FormatJsonView() {
            InitializeComponent();
            CommonView.ConfigButtonAccesssor.Visibility = Visibility.Collapsed;
            CommonView.ProcessButtonAccesssor.Click += ProcessButton_Click;
        }

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            String output = JToken.Parse(CommonView.Input).ToString(Formatting.Indented);
            CommonView.Output = output;
        }
    }
}
