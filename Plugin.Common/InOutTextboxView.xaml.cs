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

        public Double InputWidth {
            get {
                return (Double) GetValue(InputWidthProperty);
            }
            set {
                SetValue(InputWidthProperty, value);
            }
        }

        public static readonly DependencyProperty InputWidthProperty =
            DependencyProperty.Register(nameof(InputWidth), typeof(Double), typeof(InOutTextboxView),
                new PropertyMetadata(new PropertyChangedCallback(InputWidthPropertyChanged)));

        static void InputWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var control = d as InOutTextboxView;
            if (control != null)
                control.InputTextBoxHost.Width = Convert.ToInt32((Double) e.NewValue);
        }

        public Double InputHeight {
            get {
                return (Double) GetValue(InputHeightProperty);
            }
            set {
                SetValue(InputHeightProperty, value);
            }
        }

        public static readonly DependencyProperty InputHeightProperty =
            DependencyProperty.Register(nameof(InputHeight), typeof(Double), typeof(InOutTextboxView),
                new PropertyMetadata(new PropertyChangedCallback(InputHeightPropertyChanged)));

        static void InputHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var control = d as InOutTextboxView;
            if (control != null)
                control.InputTextBoxHost.Height = Convert.ToInt32((Double) e.NewValue);
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
            DependencyProperty.Register(nameof(OutputWidth), typeof(Double), typeof(InOutTextboxView),
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
            DependencyProperty.Register(nameof(OutputHeight), typeof(Double), typeof(InOutTextboxView),
                new PropertyMetadata(new PropertyChangedCallback(OutputHeightPropertyChanged)));

        static void OutputHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var control = d as InOutTextboxView;
            if (control != null)
                control.OutputTextBoxHost.Height = Convert.ToInt32((Double) e.NewValue);
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
