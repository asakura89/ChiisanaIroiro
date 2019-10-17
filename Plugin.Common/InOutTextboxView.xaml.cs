using System;
using System.Windows;
using System.Windows.Controls;
using FastColoredTextBoxNS;

namespace Plugin.Common {
    public partial class InOutTextboxView : UserControl {
        readonly FastColoredTextBox inputTextbox = new FastColoredTextBox();
        readonly FastColoredTextBox outputTextbox = new FastColoredTextBox();

        public String Input {
            get {
                return inputTextbox.Text;
            }
            set {
                inputTextbox.Text = value;
            }
        }

        public String Output {
            get {
                return outputTextbox.Text;
            }
            set {
                outputTextbox.Text = value;
            }
        }

        public void HideAllButton() {
            ConfigButton.Visibility = Visibility.Collapsed;
            ClipboardButton.Visibility = Visibility.Collapsed;
            ProcessButton.Visibility = Visibility.Collapsed;
            ButtonPanel.Visibility = Visibility.Collapsed;
        }

        public Button ConfigButtonAccesssor => ConfigButton;
        public Button ClipboardButtonAccesssor => ClipboardButton;
        public Button ProcessButtonAccesssor => ProcessButton;

        public InOutTextboxView() {
            InitializeComponent();
            InitializeInternalComponent();
        }

        void InitializeInternalComponent() {
            TextEditorHelper.Initialize(inputTextbox);
            TextEditorHelper.Initialize(outputTextbox);

            InputTextBoxHost.Child = inputTextbox;
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
