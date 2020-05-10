using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Puru.Wpf;

namespace TextProcessingPlugin {
    public partial class SortListView : UserControl, IViewablePlugin {
        public String ComponentName => "Sort List";

        public String ComponentDesc => "Sort list of strings";

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        Boolean Desc => DescCheckbox.IsChecked ?? false;

        public SortListView() {
            InitializeComponent();
            CommonView.ConfigButtonAccesssor.Visibility = Visibility.Collapsed;
            CommonView.ProcessButtonAccesssor.Click += ProcessButton_Click;
        }

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            String[] splitted = CommonView.Input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            String output = String.Join(
                Environment.NewLine,
                Desc ?
                    splitted.OrderByDescending(str => str) :
                    splitted.OrderBy(str => str)
            );

            CommonView.Output = output;
        }
    }
}
