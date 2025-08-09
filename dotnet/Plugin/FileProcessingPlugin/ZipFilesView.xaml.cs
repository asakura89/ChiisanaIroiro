using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Ionic.Zip;
using Plugin.Dialog;
using Puru.Wpf;

namespace FileProcessingPlugin {
    public partial class ZipFilesView : UserControl, IViewablePlugin {
        public String ComponentName => "Zip File";

        public String ComponentDesc => "View zip file contents";

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        public ZipFilesView() {
            InitializeComponent();
            CommonView.HideAllButton();
        }

        void BrowseZipFileButton_Click(Object sender, RoutedEventArgs e) => ZipFileTextBox.Browse();

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            if (!String.IsNullOrEmpty(ZipFileTextBox.Text)) {
                Task.Factory
                    .StartNew(() => {
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
                    }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
    }
}
