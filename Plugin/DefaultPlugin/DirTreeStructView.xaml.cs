using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Ayumi.ViewablePlugin;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace DefaultPlugin {
    public partial class DirTreeStructView : UserControl, IViewablePlugin {
        public String ComponentName => "Directory Tree Struct";

        public String ComponentDesc => "Generate tree structure of a directory";

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        Boolean Unicode => UnicodeCheckbox.IsChecked ?? false;

        public DirTreeStructView() {
            InitializeComponent();
            CommonView.ConfigButtonAccesssor.Visibility = Visibility.Collapsed;
            CommonView.ProcessButtonAccesssor.Click += ProcessButton_Click;
        }

        void Browse(ref TextBox pathTextBox) {
            try {
                if (!CommonFileDialog.IsPlatformSupported)
                    throw "Platform is not supported".AsNaiseuErrorMessage();

                var browseDialog = new CommonOpenFileDialog {
                    InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                    Title = "Select Folder",
                    Multiselect = false,
                    EnsurePathExists = true,
                    AllowNonFileSystemItems = false,
                    IsFolderPicker = true
                };

                if (browseDialog.ShowDialog() == CommonFileDialogResult.Ok) {
                    pathTextBox.Text = browseDialog.FileName;
                    pathTextBox.CaretIndex = browseDialog.FileName.Length -1;
                    pathTextBox.Focus();
                }
            }
            catch (Exception ex) {
                ex.ShowInTaskDialog();
            }
        }

        void BrowseButton_Click(Object sender, RoutedEventArgs e) => Browse(ref PathTextBox);

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            if (!String.IsNullOrEmpty(PathTextBox.Text)) {
                Task.Factory
                    .StartNew(() => {
                        StreamWriter consoleWriter = null;
                        StreamReader consoleReader = null;

                        try {
                            var process = new Process {
                                StartInfo = new ProcessStartInfo("cmd") {
                                    UseShellExecute = false,
                                    ErrorDialog = false,
                                    CreateNoWindow = true,
                                    RedirectStandardError = true,
                                    RedirectStandardInput = true,
                                    RedirectStandardOutput = true,
                                    StandardOutputEncoding = Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage)
                                }
                            };
                            process.Start();

                            consoleWriter = process.StandardInput;
                            consoleReader = process.StandardOutput;

                            String command = $@"tree {(Unicode ? String.Empty : "/A ")}/F ""{PathTextBox.Text}""";
                            consoleWriter.WriteLine("@echo off");
                            consoleWriter.WriteLine(command);
                            consoleWriter.WriteLine("exit");

                            String output = consoleReader.ReadToEnd();
                            IEnumerable<String> splitted = output
                                .Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                                .Skip(7);

                            CommonView.Output = String.Join(Environment.NewLine, splitted.Take(splitted.Count() -3));
                        }
                        catch (Exception ex) {
                            Console.WriteLine(ex.Message);
                        }
                        finally {
                            if (consoleWriter != null)
                                consoleWriter.Close();
                            if (consoleReader != null)
                                consoleReader.Close();
                        }
                    }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
    }
}
