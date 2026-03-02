using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Puru.Wpf;
using Serena;

namespace TextProcessingPlugin {
    public partial class EncodeDecodeView : UserControl, IViewablePlugin {
        public String ComponentName => "Encode / Decode";

        public String ComponentDesc => "Html / Url Encoder and Decoder";

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        public EncodeDecodeView() {
            InitializeComponent();
            CommonView.HideAllButton();
        }

        void ClipboardButton_Click(Object sender, RoutedEventArgs e) {
            if (CommonView.Output != String.Empty) {
                Clipboard.Clear();
                Clipboard.SetText(CommonView.Output);
            }
        }

        static readonly IDictionary<Int32, Func<String[], IEnumerable<String>>> Operations = new Dictionary<Int32, Func<String[], IEnumerable<String>>> {
            [1] = inputs => inputs.Select(input => WebUtility.HtmlDecode(input)),
            [2] = inputs => inputs.Select(input => WebUtility.HtmlDecode(input)),
            [3] = inputs => inputs.Select(input => WebUtility.UrlEncode(input).Replace("+", "%20")),
            [4] = inputs => inputs.Select(input => WebUtility.UrlDecode(input)),
            [5] = inputs => inputs.Select(input => Convert.ToBase64String(Encoding.UTF8.GetBytes(input))),
            [6] = inputs => inputs.Select(input => Encoding.UTF8.GetString(Convert.FromBase64String(input))),
            [7] = inputs => inputs.Select(SecurityExt.EncodeBase64Url),
            [8] = inputs => inputs.Select(SecurityExt.DecodeBase64Url)
        };

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            try {
                String[] inputs = CommonView.Input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                if (Operations.ContainsKey(EncodeDecodeComboBox.SelectedIndex)) {
                    CommonView.Output = String.Join(
                        Environment.NewLine,
                        Operations[EncodeDecodeComboBox.SelectedIndex].Invoke(inputs)
                    );
                }
            }
            catch {
                // NOTE: to catch nonbase64 entered
                CommonView.Output = "Invalid input.";
            }
        }
    }
}
