using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Ayumi.ViewablePlugin;
using Ionic.Zip;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace DefaultPlugin {
    public partial class ZipFilesView : UserControl, IViewablePlugin {
        public String ComponentName => "Zip File";

        public String ComponentDesc => "View zip file contents";

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        public ZipFilesView() {
            InitializeComponent();
            CommonView.HideAllButton();
        }

        void Browse(ref TextBox outputTextBox) {
            try {
                if (!CommonFileDialog.IsPlatformSupported)
                    throw "Platform is not supported".AsNaiseuErrorMessage();

                var browseDialog = new CommonOpenFileDialog {
                    InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                    Title = "Select Zip",
                    Multiselect = false,
                    EnsurePathExists = true,
                    AllowNonFileSystemItems = false,
                    DefaultExtension = ".zip"
                };

                if (browseDialog.ShowDialog() == CommonFileDialogResult.Ok) {
                    outputTextBox.Text = browseDialog.FileName;
                    outputTextBox.CaretIndex = browseDialog.FileName.Length -1;
                    outputTextBox.Focus();
                }
            }
            catch (Exception ex) {
                ex.ShowInTaskDialog();
            }
        }

        void BrowseZipFileButton_Click(Object sender, RoutedEventArgs e) => Browse(ref ZipFileTextBox);

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            try {
                var builder = new StringBuilder();
                var stream = (Stream) File.Open(ZipFileTextBox.Text, FileMode.Open, FileAccess.Read, FileShare.Read);
                using (var zip = ZipFile.Read(stream)) {
                    foreach (ZipEntry entry in zip) {
                        if (Path.GetExtension(entry.FileName).Equals(".zip", StringComparison.InvariantCultureIgnoreCase)) {
                            String izipName = entry.FileName + "/";
                            builder.AppendLine(izipName);

                            var contentStream = new MemoryStream();
                            entry.Extract(contentStream);
                            contentStream.Position = 0;
                            using (var izip = ZipFile.Read(contentStream))
                                foreach (ZipEntry izipEntry in izip)
                                    builder.AppendLine(izipName + izipEntry.FileName);
                        }
                        else
                            builder.AppendLine(entry.FileName);
                    }
                }

                CommonView.Output = builder.ToString();
            }
            catch (Exception ex) {
                ex.ShowInTaskDialog();
            }
        }
    }
}
