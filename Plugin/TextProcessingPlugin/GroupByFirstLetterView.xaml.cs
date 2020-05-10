using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Puru.Wpf;

namespace TextProcessingPlugin {
    public partial class GroupByFirstLetterView : UserControl, IViewablePlugin {
        public String ComponentName => "Group by First Letter";

        public String ComponentDesc => String.Empty;

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        public GroupByFirstLetterView() {
            InitializeComponent();
            CommonView.ConfigButtonAccesssor.Visibility = Visibility.Collapsed;
            CommonView.ProcessButtonAccesssor.Click += ProcessButton_Click;
        }

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            var lookup = (Lookup<String, String>) CommonView.Input
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .ToLookup(str => str[0].ToString().ToUpperInvariant(), str => str);

            var outputBuilder = new StringBuilder();
            foreach (IGrouping<String, String> lookupGroup in lookup) {
                outputBuilder.AppendLine(lookupGroup.Key);
                foreach (String lookupItem in lookupGroup)
                    outputBuilder.AppendLine($"  {lookupItem}");

                outputBuilder.AppendLine();
            }

            CommonView.Output = outputBuilder.ToString();
        }
    }
}
