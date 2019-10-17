using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Ayumi.ViewablePlugin;

namespace DefaultPlugin {
    public partial class SortListView : UserControl, IViewablePlugin {
        public String ComponentName => "Sort List";

        public String ComponentDesc => "Sort list of strings";

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        public SortListView() {
            InitializeComponent();
            CommonView.ConfigButtonAccesssor.Visibility = Visibility.Collapsed;
            CommonView.ProcessButtonAccesssor.Click += ProcessButton_Click;
        }

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            String output = String.Join(Environment.NewLine,
                CommonView.Input
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                    .OrderBy(str => str));

            CommonView.Output = output;
        }
    }
}
