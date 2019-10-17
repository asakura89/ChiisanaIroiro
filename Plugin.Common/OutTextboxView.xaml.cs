using System;
using System.Windows;
using System.Windows.Controls;
using FastColoredTextBoxNS;

namespace Plugin.Common {
    public partial class OutTextboxView : UserControl {
        readonly FastColoredTextBox outputTextbox = new FastColoredTextBox();

        public String Output {
            get {
                return outputTextbox.Text;
            }
            set {
                outputTextbox.Text = value;
            }
        }

        public Button ConfigButtonAccesssor => ConfigButton;
        public Button ClipboardButtonAccesssor => ClipboardButton;
        public Button ProcessButtonAccesssor => ProcessButton;

        public void HideAllButton() {
            ConfigButton.Visibility = Visibility.Collapsed;
            ClipboardButton.Visibility = Visibility.Collapsed;
            ProcessButton.Visibility = Visibility.Collapsed;
            ButtonPanel.Visibility = Visibility.Collapsed;
        }

        public OutTextboxView() {
            InitializeComponent();
            InitializeInternalComponent();
        }

        void InitializeInternalComponent() {
            TextEditorHelper.Initialize(outputTextbox);

            OutputTextBoxHost.Child = outputTextbox;
        }

        void ClipboardButton_Click(Object sender, RoutedEventArgs e) {
            if (Output != String.Empty) {
                Clipboard.Clear();
                Clipboard.SetText(Output);
            }
        }
    }
}
