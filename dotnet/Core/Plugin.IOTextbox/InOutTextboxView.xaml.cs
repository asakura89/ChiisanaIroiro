using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Plugin.Common;

namespace Plugin.IOTextbox;

public partial class InOutTextboxView : UserControl {
    public InOutTextboxView() {
        InitializeComponent();
        InitializeInternalComponent();
        IsVisibleChanged += OnIsVisibleChanged;
    }

    public string Input {
        get => InputTextBox.Text;
        set => InputTextBox.Text = value;
    }

    public string Output {
        get => OutputTextBox.Text;
        set => OutputTextBox.Text = value;
    }

    public bool ReadOnlyInput {
        get => OutputTextBox.IsReadOnly;
        set => OutputTextBox.IsReadOnly = value;
    }

    public bool ReadOnlyOutput {
        get => OutputTextBox.IsReadOnly;
        set => OutputTextBox.IsReadOnly = value;
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

    void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) {
        InputBorder.BorderBrush = new SolidColorBrush(ColorGenerator.GetColor());
        OutputBorder.BorderBrush = new SolidColorBrush(ColorGenerator.GetColor());
    }

    void InitializeInternalComponent() {
        TextEditorHelper.Initialize(InputTextBox);
        TextEditorHelper.Initialize(OutputTextBox);
    }

    void ClipboardButton_Click(object sender, RoutedEventArgs e) {
        if (Output != string.Empty) {
            Clipboard.Clear();
            Clipboard.SetText(Output);
        }
    }
}