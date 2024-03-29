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

        Boolean OverwriteExisting => OverwriteExistingCheckBox.IsChecked ?? false;

        String SourceDir {
            get {
                if (String.IsNullOrEmpty(SourceDirectoryTextBox.Text))
                    return String.Empty;

                return SourceDirectoryTextBox.Text;
            }
        }

        String TargetDir {
            get {
                if (String.IsNullOrEmpty(TargetDirectoryTextBox.Text))
                    return String.Empty;

                return TargetDirectoryTextBox.Text;
            }
        }

        public CopyFilesView() {
            InitializeComponent();
            CommonView.HideAllButton();
        }

        void BrowseSourceButton_Click(Object sender, RoutedEventArgs e) => SourceDirectoryTextBox.Browse();

        void BrowseTargetButton_Click(Object sender, RoutedEventArgs e) => TargetDirectoryTextBox.Browse();

        void Open(String path) {
            if (!String.IsNullOrEmpty(path) && Directory.Exists(path))
                SysProcess.Start(path);
        }

        void OpenSourceButton_Click(Object sender, RoutedEventArgs e) => Open(SourceDir);

        void OpenTargetButton_Click(Object sender, RoutedEventArgs e) => Open(TargetDir);

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            try {
                // NOTE: Output here act as Input
                // because I use single textbox component
                String input = CommonView.Output;
                if (!String.IsNullOrEmpty(input)) {
                    IEnumerable<FileOpInfo> pathInfos = input
                        .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(path => path.Trim())
                        .Select(ConvertPathToFileOpInfo);

                    Boolean anyNonExistent = pathInfos.Any(info => !info.ExistsOnSource);
                    if (anyNonExistent) {
                        String nonExistentPaths = String.Join(Environment.NewLine,
                            pathInfos
                                .Where(info => !info.ExistsOnSource)
                                .Select(info => info.SourcePath));

                        throw $"Check these:{Environment.NewLine}{nonExistentPaths}".AsNaiseuErrorMessage();
                    }

                    foreach (FileOpInfo info in pathInfos) {
                        RecurseCreateDirectory(new DirectoryInfo(TargetDirectoryTextBox.Text), info.OriginalSourceDir);
                        Boolean exists = File.Exists(info.TargetPath);
                        if (!exists || (exists && OverwriteExisting))
                            File.Copy(info.SourcePath, info.TargetPath);
                    }
                }
            }
            catch (Exception ex) {
                ex.ShowInTaskDialog();
            }
        }

        class FileOpInfo {
            public String OriginalPath { get; set; }
            public String SourcePath { get; set; }
            public String OriginalSourceDir { get; set; }
            public Boolean ExistsOnSource { get; set; }
            public Boolean IsDirectory { get; set; }
            public String TargetPath { get; set; }
        }

        FileOpInfo ConvertPathToFileOpInfo(String path) {
            String sourceDir = SourceDir;
            String sourcePath = Path.Combine(sourceDir, path);
            Boolean isDirectory = File.GetAttributes(sourcePath).HasFlag(FileAttributes.Directory);

            return new FileOpInfo {
                OriginalPath = path,
                SourcePath = sourcePath,
                OriginalSourceDir = GetDirectoryInOriginalPathStyle(sourcePath, sourceDir),
                ExistsOnSource = isDirectory ? Directory.Exists(sourcePath) : File.Exists(sourcePath),
                IsDirectory = isDirectory,
                TargetPath = Path.Combine(TargetDir, path)
            };
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
