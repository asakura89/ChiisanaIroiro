using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Puru.Wpf;

namespace GenerateRandomPlugin {
    public partial class GenerateGuidView : UserControl, IViewablePlugin {
        public String ComponentName => "Generate Guid";

        public String ComponentDesc => String.Empty;

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        Int32 Count {
            get {
                String count = String.IsNullOrEmpty(CountTextbox.Text) ? "0" : CountTextbox.Text;
                return Convert.ToInt32(count);
            }
        }

        public GenerateGuidView() {
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
            var builder = new StringBuilder();
            Int32 counter = 0;
            while (counter < Count) {
                var guid = Guid.NewGuid();
                builder
                    .AppendLine(guid.ToString("D"))
                    .AppendLine(guid.ToString("N"))
                    .AppendLine(guid.ToString("P"))
                    .AppendLine(guid.ToString("B"))
                    .AppendLine(guid.ToString("X"))
                    .AppendLine(guid.ToString("D").ToUpperInvariant())
                    .AppendLine(guid.ToString("N").ToUpperInvariant())
                    .AppendLine(guid.ToString("P").ToUpperInvariant())
                    .AppendLine(guid.ToString("B").ToUpperInvariant())
                    .AppendLine();

                counter++;
            }

            CommonView.Output = builder.ToString();
        }

        void NumericTextbox_KeyUp(Object sender, KeyEventArgs e) {

        }
    }
}
