using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Ayumi.ViewablePlugin;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace DefaultPlugin {
    public partial class DirListView : UserControl, IViewablePlugin {
        public String ComponentName => "Directory List";

        public String ComponentDesc => "Content list of a directory";

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        Boolean JustDir => JustDirRadio.IsChecked ?? false;

        Boolean JustFile => JustFileRadio.IsChecked ?? false;

        public DirListView() {
            InitializeComponent();
            CommonView.HideAllButton();
            BothRadio.IsChecked = true;
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

        void ClipboardButton_Click(Object sender, RoutedEventArgs e) {
            if (CommonView.Output != String.Empty) {
                Clipboard.Clear();
                Clipboard.SetText(CommonView.Output);
            }
        }

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

                            String command = $@"dir /S /B ""{PathTextBox.Text}""";
                            consoleWriter.WriteLine("@echo off");
                            consoleWriter.WriteLine(command);
                            consoleWriter.WriteLine("exit");

                            String output = consoleReader.ReadToEnd();
                            IEnumerable<String> splitted = output
                                .Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                                .Skip(5);

                            splitted = splitted.Take(splitted.Count() -2);

                            var regex = new Regex(@"^(.+)\\([^\\]+).+\.[^.+]{2,}$", RegexOptions.Compiled);
                            if (JustDir)
                                splitted = splitted.Where(line => !regex.IsMatch(line));
                            else if (JustFile)
                                splitted = splitted.Where(line => regex.IsMatch(line));

                            CommonView.Output = String.Join(Environment.NewLine, splitted);
                        }
                        catch (Exception ex) {
                            ex.ShowInTaskDialog();
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
