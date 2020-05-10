using System;
using System.Windows;
using System.Windows.Controls;
using Puru.Wpf;

namespace FileProcessingPlugin {
    public partial class MergeFilesView : UserControl, IViewablePlugin {
        public String ComponentName => "Merge Files";

        public String ComponentDesc => "Merge multiple files";

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        public MergeFilesView() {
            InitializeComponent();
        }

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            
        }
    }
}
