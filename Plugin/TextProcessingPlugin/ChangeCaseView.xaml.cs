using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Puru.Wpf;

namespace TextProcessingPlugin {
    public partial class ChangeCaseView : UserControl, IViewablePlugin {
        public String ComponentName => "Change Case";

        public String ComponentDesc => "Change text case to upper, lower, or etc";

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        Boolean Titlecase => TitlecaseRadio.IsChecked ?? false;

        Boolean Uppercase => UppercaseRadio.IsChecked ?? false;

        Boolean Lowercase => LowercaseRadio.IsChecked ?? false;

        public ChangeCaseView() {
            InitializeComponent();
            CommonView.HideAllButton();
            TitlecaseRadio.IsChecked = true;
        }

        void ClipboardButton_Click(Object sender, RoutedEventArgs e) {
            if (CommonView.Output != String.Empty) {
                Clipboard.Clear();
                Clipboard.SetText(CommonView.Output);
            }
        }

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            TextInfo currentTextInfo = CultureInfo.CurrentCulture.TextInfo;
            if (Titlecase)
                CommonView.Output = currentTextInfo.ToTitleCase(CommonView.Input);
            else if (Uppercase)
                CommonView.Output = currentTextInfo.ToUpper(CommonView.Input);
            else if (Lowercase)
                CommonView.Output = currentTextInfo.ToLower(CommonView.Input);
        }
    }
}
