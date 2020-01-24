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

        public Double OutputWidth {
            get {
                return (Double) GetValue(OutputWidthProperty);
            }
            set {
                SetValue(OutputWidthProperty, value);
            }
        }

        public static readonly DependencyProperty OutputWidthProperty =
            DependencyProperty.Register(nameof(OutputWidth), typeof(Double), typeof(OutTextboxView),
                new PropertyMetadata(new PropertyChangedCallback(OutputWidthPropertyChanged)));

        static void OutputWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var control = d as InOutTextboxView;
            if (control != null)
                control.OutputTextBoxHost.Width = Convert.ToInt32((Double) e.NewValue);
        }

        public Double OutputHeight {
            get {
                return (Double) GetValue(OutputHeightProperty);
            }
            set {
                SetValue(OutputHeightProperty, value);
            }
        }

        public static readonly DependencyProperty OutputHeightProperty =
            DependencyProperty.Register(nameof(OutputHeight), typeof(Double), typeof(OutTextboxView),
                new PropertyMetadata(new PropertyChangedCallback(OutputHeightPropertyChanged)));

        static void OutputHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var control = d as InOutTextboxView;
            if (control != null)
                control.OutputTextBoxHost.Height = Convert.ToInt32((Double) e.NewValue);
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
