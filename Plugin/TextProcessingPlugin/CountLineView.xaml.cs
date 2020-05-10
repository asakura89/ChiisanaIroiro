using System;
using System.Windows;
using System.Windows.Controls;
using Puru.Wpf;

namespace TextProcessingPlugin {
    public partial class CountLineView : UserControl, IViewablePlugin {
        public String ComponentName => "Count Line";

        public String ComponentDesc => "Count lines of list";

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        public CountLineView() {
            InitializeComponent();
            CommonView.ConfigButtonAccesssor.Visibility = Visibility.Collapsed;
            CommonView.ProcessButtonAccesssor.Click += ProcessButton_Click;
        }

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            String[] lines = CommonView.Input
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            CommonView.Output = lines.Length.ToString();
        }
    }
}
