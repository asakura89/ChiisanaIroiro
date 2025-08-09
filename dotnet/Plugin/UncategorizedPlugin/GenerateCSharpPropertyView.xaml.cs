using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Puru.Wpf;

namespace UncategorizedPlugin {
    public partial class GenerateCSharpPropertyView : UserControl, IViewablePlugin {
        public String ComponentName => "Generate CSharp Property";

        public String ComponentDesc => String.Empty;

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        public GenerateCSharpPropertyView() {
            InitializeComponent();
            CommonView.ConfigButtonAccesssor.Visibility = Visibility.Collapsed;
            CommonView.ProcessButtonAccesssor.Click += ProcessButton_Click;

            String example = new StringBuilder()
                .AppendLine("Name")
                .AppendLine("Address:String")
                .AppendLine("PostalCode:Int32")
                .ToString();

            CommonView.Input = example;
        }

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            IList<String> lines = CommonView.Input
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            String output = String.Join(
                Environment.NewLine,
                lines.Select(ConvertToPropertyString)
            );

            CommonView.Output = output;
        }

        String ConvertToPropertyString(String line) {
            const String template = "public {0} {1} {{ get; set; }}";

            String[] splittedLine = line.Split(':');
            if (splittedLine.Length == 1)
                return String.Format(template, "String", splittedLine[0]);

            if (splittedLine.Length == 2)
                return String.Format(template, splittedLine[1], splittedLine[0]);

            return String.Empty;
        }
    }
}
