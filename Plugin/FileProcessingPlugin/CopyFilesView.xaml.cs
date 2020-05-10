using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Plugin.Common;
using Plugin.Dialog;
using Puru.Wpf;
using SysProcess = System.Diagnostics.Process;

namespace FileProcessingPlugin {
    public partial class CopyFilesView : UserControl, IViewablePlugin {
        public String ComponentName => "Copy Files";

        public String ComponentDesc => "Copy files from the list";

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        Boolean Zip => ZipCheckBox.IsChecked ?? false;

        Boolean Complain => ComplainCheckBox.IsChecked ?? false;

        public CopyFilesView() {
            InitializeComponent();
            CommonView.HideAllButton();
            ZipCheckBox.IsChecked = true;
        }

        void BrowseSourceButton_Click(Object sender, RoutedEventArgs e) => SourceDirectoryTextBox.Browse();

        void BrowseTargetButton_Click(Object sender, RoutedEventArgs e) => TargetDirectoryTextBox.Browse();

        void Open(String path) {
            if (!String.IsNullOrEmpty(path) && Directory.Exists(path))
                SysProcess.Start(path);
        }

        void OpenSourceButton_Click(Object sender, RoutedEventArgs e) => Open(SourceDirectoryTextBox.Text);

        void OpenTargetButton_Click(Object sender, RoutedEventArgs e) => Open(TargetDirectoryTextBox.Text);

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            try {
                // NOTE: Output here act as Input
                // because I use single textbox component
                String input = CommonView.Output;
                if (!String.IsNullOrEmpty(input)) {
                    IEnumerable<(String OriginalPath, String SourcePath, String OriginalSourceDir, Boolean ExistsOnSource, String TargetPath)> pathInfos = input
                        .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(path => path.Trim())
                        .Select(path => (
                            OriginalPath: path,
                            SourcePath: Path.Combine(SourceDirectoryTextBox.Text, path),
                            OriginalSourceDir: GetDirectoryInOriginalPathStyle(Path.Combine(SourceDirectoryTextBox.Text, path), SourceDirectoryTextBox.Text),
                            ExistsOnSource: File.Exists(Path.Combine(SourceDirectoryTextBox.Text, path)),
                            TargetPath: Path.Combine(TargetDirectoryTextBox.Text, path))
                        );

                    Boolean anyNonExistent = pathInfos.Any(info => !info.ExistsOnSource);
                    if (Complain && anyNonExistent) {
                        String nonExistentPaths = String.Join(Environment.NewLine,
                            pathInfos
                                .Where(info => !info.ExistsOnSource)
                                .Select(info => info.SourcePath));

                        throw $"Check these:{Environment.NewLine}{nonExistentPaths}".AsNaiseuErrorMessage();
                    }

                    foreach ((String OriginalPath, String SourcePath, String OriginalSourceDir, Boolean ExistsOnSource, String TargetPath) info in pathInfos) {
                        RecurseCreateDirectory(new DirectoryInfo(TargetDirectoryTextBox.Text), info.OriginalSourceDir);
                        if (!File.Exists(info.TargetPath))
                            File.Copy(info.SourcePath, info.TargetPath);
                    }
                }
            }
            catch (Exception ex) {
                ex.ShowInTaskDialog();
            }
        }

        String GetDirectoryInOriginalPathStyle(String filepath, String sourceDir) => GetDirectory(filepath).Replace(sourceDir, String.Empty).Trim(new[] { '\\' });

        String GetDirectory(String filepath) => IsDirectory(filepath) ? filepath : Path.GetDirectoryName(filepath);

        Boolean IsDirectory(String path) => File
            .GetAttributes(path)
            .HasFlag(FileAttributes.Directory);

        void RecurseCreateDirectory(DirectoryInfo dirInfo, String pathToBeCreated) {
            String[] splitted = pathToBeCreated.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            while (splitted.Length >= 1) {
                String currentPath = Path.Combine(dirInfo.FullName, splitted[0]);
                if (!Directory.Exists(currentPath))
                    Directory.CreateDirectory(currentPath);

                dirInfo = new DirectoryInfo(currentPath);
                splitted = splitted.Skip(1).ToArray();
                String newP = String.Join("\\", splitted);
                if (String.IsNullOrEmpty(newP))
                    return;
            }
        }
    }
}
