using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Plugin.Dialog;
using Puru.Wpf;
using SysProcess = System.Diagnostics.Process;

namespace FileProcessingPlugin {
    public partial class MergeFilesView : UserControl, IViewablePlugin {
        public String ComponentName => "Merge Files";

        public String ComponentDesc => "Merge multiple files";

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        String SourceRootDir {
            get {
                if (String.IsNullOrEmpty(SourceRootDirTextBox.Text))
                    return String.Empty;

                return SourceRootDirTextBox.Text.Trim();
            }
        }

        String OutputDir {
            get {
                if (String.IsNullOrEmpty(OutputDirTextBox.Text))
                    return String.Empty;

                return OutputDirTextBox.Text.Trim();
            }
        }

        public MergeFilesView() {
            InitializeComponent();
        }

        void BrowseSourceRootDirButton_Click(Object sender, RoutedEventArgs e) => SourceRootDirTextBox.Browse();

        void BrowseOutputDirButton_Click(Object sender, RoutedEventArgs e) => OutputDirTextBox.Browse();

        void Open(String path) {
            if (!String.IsNullOrEmpty(path) && Directory.Exists(path))
                SysProcess.Start(path);
        }

        void OpenSourceRootDirButton_Click(Object sender, RoutedEventArgs e) => Open(SourceRootDir);

        void OpenOutputDirButton_Click(Object sender, RoutedEventArgs e) => Open(OutputDir);

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            if (!String.IsNullOrEmpty(SourceRootDir) && !String.IsNullOrEmpty(OutputDir)) {
                Task.Factory
                    .StartNew(() => {
                        try {
                            IList<String> Source = Directory.EnumerateFiles(SourceRootDir).ToList();
                            Boolean PrintFileDelimiter = true;

                            //Console.WriteLine("Start.");

                            String outputFile = Path.Combine(OutputDir, "combined.txt");
                            const Int32 MaxCharacterColumn = 80;
                            using (var sw = new StreamWriter(new FileStream(outputFile, FileMode.Create, FileAccess.ReadWrite), Encoding.UTF8)) {
                                for (Int32 idx = 0; idx < Source.Count; idx++) {
                                    String filepath = Source[idx];
                                    using (var sr = new StreamReader(filepath, Encoding.UTF8)) {
                                        if (PrintFileDelimiter)
                                            sw.WriteLine(
                                                new StringBuilder()
                                                    .AppendLine()
                                                    .AppendLine(
                                                        String.Join(
                                                            String.Empty,
                                                            Enumerable.Repeat("=-", MaxCharacterColumn / 2)
                                                        )
                                                    )
                                                    .AppendLine(filepath)
                                                    .AppendLine(
                                                        String.Join(
                                                            String.Empty,
                                                            Enumerable.Repeat("-=", MaxCharacterColumn / 2)
                                                        )
                                                    )
                                                    .AppendLine()
                                            );

                                        sw.WriteLine(sr.ReadToEnd());
                                    }
                                }
                            }

                            //Console.WriteLine("Done.");
                        }
                        catch (Exception ex) {
                            ex.ShowInTaskDialog();
                        }
                    }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
    }
}
