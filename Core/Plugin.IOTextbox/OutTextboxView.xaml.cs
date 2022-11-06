using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Plugin.Common;

namespace Plugin.IOTextbox {
    public partial class OutTextboxView : UserControl {
        public String Output {
            get {
                return OutputTextBox.Text;
            }
            set {
                OutputTextBox.Text = value;
            }
        }

        public Boolean ReadOnlyOutput {
            get {
                return OutputTextBox.IsReadOnly;
            }
            set {
                OutputTextBox.IsReadOnly = value;
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
            IsVisibleChanged += OnIsVisibleChanged;
        }

        void OnIsVisibleChanged(Object sender, DependencyPropertyChangedEventArgs e) =>
            OutputBorder.BorderBrush = new SolidColorBrush(ColorGenerator.GetColor());

        void InitializeInternalComponent() => TextEditorHelper.Initialize(OutputTextBox);

        void ClipboardButton_Click(Object sender, RoutedEventArgs e) {
            if (Output != String.Empty) {
                Clipboard.Clear();
                Clipboard.SetText(Output);
            }
        }
    }
}
