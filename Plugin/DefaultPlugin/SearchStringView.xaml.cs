using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Ayumi.ViewablePlugin;

namespace DefaultPlugin {
    public partial class SearchStringView : UserControl, IViewablePlugin {
        public String ComponentName => "Search String";

        public String ComponentDesc => "Search within string with regex";

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        public SearchStringView() {
            InitializeComponent();
            CommonView.ConfigButtonAccesssor.Visibility = Visibility.Collapsed;
            CommonView.ProcessButtonAccesssor.Click += ProcessButton_Click;
        }

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            if (!String.IsNullOrEmpty(RegexTextbox.Text)) {
                MatchCollection collection = Regex.Matches(CommonView.Input, RegexTextbox.Text, RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Multiline);

                var outputBuilder = new StringBuilder();
                foreach (Match match in collection) {
                    outputBuilder.AppendLine(match.Value);
                }

                CommonView.Output = outputBuilder.ToString(); 
            }
        }
    }
}
