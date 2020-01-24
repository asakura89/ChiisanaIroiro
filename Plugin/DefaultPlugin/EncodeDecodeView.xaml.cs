using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security.AntiXss;
using System.Windows;
using System.Windows.Controls;
using Ayumi.ViewablePlugin;

namespace DefaultPlugin {
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
            [1] = new Func<String[], IEnumerable<String>>(inputs => inputs.Select(input => AntiXssEncoder.HtmlEncode(input, true))),
            [2] = new Func<String[], IEnumerable<String>>(inputs => inputs.Select(input => HttpUtility.HtmlDecode(input))),
            [3] = new Func<String[], IEnumerable<String>>(inputs => inputs.Select(input => AntiXssEncoder.UrlEncode(input).Replace("+", "%20"))),
            [4] = new Func<String[], IEnumerable<String>>(inputs => inputs.Select(input => HttpUtility.UrlDecode(input))),
            [5] = new Func<String[], IEnumerable<String>>(inputs => inputs.Select(input => Convert.ToBase64String(Encoding.UTF8.GetBytes(input)))),
            [6] = new Func<String[], IEnumerable<String>>(inputs => inputs.Select(input => Encoding.UTF8.GetString(Convert.FromBase64String(input)))),
            [7] = new Func<String[], IEnumerable<String>>(inputs => inputs.Select(Base64UrlEncode)),
            [8] = new Func<String[], IEnumerable<String>>(inputs => inputs.Select(Base64UrlDecode))
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
            catch (Exception ex) {
                // NOTE: to catch nonbase64 entered
                CommonView.Output = "Invalid input.";
            }
        }

        static String Base64UrlEncode(String text) =>
            Convert
                .ToBase64String(Encoding.UTF8.GetBytes(text))
                .TrimEnd('=')
                .Replace("+", "-")
                .Replace("/", "_");

        static String Base64UrlDecode(String base64Url) {
            String base64 = base64Url
                .Replace("-", "+")
                .Replace("_", "/");

            return Encoding.UTF8.GetString(
                Convert.FromBase64String(
                    base64.Length % 4 == 2 ?
                        base64 + "==" :
                        base64.Length % 4 == 3 ?
                            base64 + "=" : base64
                )
            );
        }
    }
}
