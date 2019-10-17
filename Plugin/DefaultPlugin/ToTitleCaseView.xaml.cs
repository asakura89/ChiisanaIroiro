using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Ayumi.ViewablePlugin;

namespace DefaultPlugin {
    public partial class ToTitleCaseView : UserControl, IViewablePlugin {
        public String ComponentName => "To Title Case";

        public String ComponentDesc => String.Empty;

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        public ToTitleCaseView() {
            InitializeComponent();
            CommonView.ConfigButtonAccesssor.Visibility = Visibility.Collapsed;
            CommonView.ProcessButtonAccesssor.Click += ProcessButton_Click;
        }

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            TextInfo currentTextInfo = CultureInfo.CurrentCulture.TextInfo;
            String output = currentTextInfo.ToTitleCase(CommonView.Input);
            CommonView.Output = output;
        }
    }
}
