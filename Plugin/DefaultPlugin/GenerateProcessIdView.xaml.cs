using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ayumi.ViewablePlugin;

namespace DefaultPlugin {
    public partial class GenerateProcessIdView : UserControl, IViewablePlugin {
        public String ComponentName => "Generate ProcessId";

        public String ComponentDesc => "Generate sequential number-based id";

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        Int32 StartAt {
            get {
                String startAt = String.IsNullOrEmpty(StartAtTextbox.Text) ? "0" : StartAtTextbox.Text;
                return Convert.ToInt32(startAt);
            }
        }

        Int32 Iterate {
            get {
                String iterate = String.IsNullOrEmpty(IterateCountTextbox.Text) ? "0" : IterateCountTextbox.Text;
                return Convert.ToInt32(iterate);
            }
        }

        public GenerateProcessIdView() {
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
            Int32 start = StartAt;
            Int32 end = start + Iterate;
            while (start < end)
                builder.AppendLine(DateTime.Now.ToString("yyyyMMdd") + start++.ToString().PadLeft(4, '0'));

            CommonView.Output = builder.ToString();
        }

        void NumericTextbox_KeyUp(Object sender, KeyEventArgs e) {

        }
    }
}
