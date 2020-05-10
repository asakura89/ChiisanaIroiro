using System;
using System.Windows.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using Plugin.Common;

namespace Plugin.Dialog {
    public static class BrowseTextboxExt {
        public static void Browse(this TextBox pathTextBox) {
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
    }
}
