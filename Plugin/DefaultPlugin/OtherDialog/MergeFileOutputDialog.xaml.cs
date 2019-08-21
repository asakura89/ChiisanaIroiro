using System;
using System.Windows;
using Microsoft.Win32;

namespace DefaultPlugin.OtherDialog {
    public partial class MergeFileOutputDialog : Window {
        void BrowseButton_Click(Object sender, RoutedEventArgs e) {
            var browseDialog = new OpenFileDialog();
            if (browseDialog.ShowDialog() == true)
                OutputFileTextBox.Text = browseDialog.FileName;
        }
    }
}
