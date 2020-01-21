using System;
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

        Boolean HtmlEncode => HtmlEncodeRadio.IsChecked ?? false;

        Boolean HtmlDecode => HtmlDecodeRadio.IsChecked ?? false;

        Boolean UrlEncode => UrlEncodeRadio.IsChecked ?? false;

        Boolean UrlDecode => UrlDecodeRadio.IsChecked ?? false;

        public EncodeDecodeView() {
            InitializeComponent();
            CommonView.HideAllButton();
            HtmlEncodeRadio.IsChecked = true;
        }

        void ClipboardButton_Click(Object sender, RoutedEventArgs e) {
            if (CommonView.Output != String.Empty) {
                Clipboard.Clear();
                Clipboard.SetText(CommonView.Output);
            }
        }

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            String input = CommonView.Input;
            if (HtmlEncode)
                CommonView.Output = AntiXssEncoder.HtmlEncode(input, true).Replace("+", "%20");
            else if (HtmlDecode)
                CommonView.Output = HttpUtility.HtmlDecode(input);
            else if (UrlEncode)
                CommonView.Output = AntiXssEncoder.UrlEncode(input);
            else if (UrlDecode)
                CommonView.Output = HttpUtility.UrlDecode(input);
        }
    }
}
