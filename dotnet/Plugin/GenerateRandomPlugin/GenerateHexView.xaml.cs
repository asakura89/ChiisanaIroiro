using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KeywielderCore;
using Puru.Wpf;

namespace GenerateRandomPlugin {
    public partial class GenerateHexView : UserControl, IViewablePlugin {
        public String ComponentName => "Generate Random Hex";

        public String ComponentDesc => String.Empty;

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        Int32 Count {
            get {
                String count = String.IsNullOrEmpty(CountTextbox.Text) ? "0" : CountTextbox.Text;
                return Convert.ToInt32(count);
            }
        }

        Int32 Length {
            get {
                String length = String.IsNullOrEmpty(LengthTextbox.Text) ? "0" : LengthTextbox.Text;
                return Convert.ToInt32(length);
            }
        }

        Boolean Uppercase => UpperCheckbox.IsChecked ?? false;

        public GenerateHexView() {
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
                builder.AppendLine(
                    Wielder.New()
                        .AddRandomHex(Length, Uppercase)
                        .BuildKey());

                counter++;
            }

            CommonView.Output = builder.ToString();
        }

        void NumericTextbox_KeyUp(Object sender, KeyEventArgs e) {

        }
    }
}
