using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Puru.Wpf;

namespace TextProcessingPlugin {
    public partial class GroupByFirstLetterView : UserControl, IViewablePlugin {
        public String ComponentName => "Group by first {x} letter";

        public String ComponentDesc => String.Empty;

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        public GroupByFirstLetterView() {
            InitializeComponent();
            CommonView.ConfigButtonAccesssor.Visibility = Visibility.Collapsed;
            CommonView.ProcessButtonAccesssor.Click += ProcessButton_Click;
        }

        Int32 XLetter {
            get {
                String count = String.IsNullOrEmpty(XLetterTextbox.Text) ? "0" : XLetterTextbox.Text;
                return Convert.ToInt32(count);
            }
        }

        String GetLookupKey(String input) {
            Int32 length = XLetter;
            if (XLetter < 0)
                length = 0;
            else if (XLetter > input.Length)
                length = input.Length;

            return input
                .Substring(0, length)
                .ToUpperInvariant();
        }

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            var lookup = (Lookup<String, String>) CommonView.Input
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .ToLookup(GetLookupKey, str => str);

            var outputBuilder = new StringBuilder();
            foreach (IGrouping<String, String> lookupGroup in lookup) {
                outputBuilder.AppendLine(lookupGroup.Key);
                foreach (String lookupItem in lookupGroup)
                    outputBuilder.AppendLine($"  {lookupItem}");

                outputBuilder.AppendLine();
            }

            CommonView.Output = outputBuilder.ToString();
        }

        void NumericTextbox_KeyUp(Object sender, KeyEventArgs e) {

        }
    }
}
