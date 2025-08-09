using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Puru.Wpf;

namespace TextProcessingPlugin {
    public partial class TextStatisticsView : UserControl, IViewablePlugin {
        public String ComponentName => "Text Statistics";

        public String ComponentDesc => "Count lines, words, and characters";

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        public TextStatisticsView() {
            InitializeComponent();
            CommonView.ConfigButtonAccesssor.Visibility = Visibility.Collapsed;
            CommonView.ProcessButtonAccesssor.Click += ProcessButton_Click;
        }

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            String[] lines = CommonView.Input
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            Int32 charsFull = 0, charsStripped = 0;
            for (Int32 idx = 0; idx < lines.Length; idx++) {
                Int32 full = lines[idx].Length;
                Int32 stripped = lines[idx].Trim().Replace(" ", String.Empty).Length;
                charsFull += full;
                charsStripped += stripped;
            }

            Int32 words = 0;
            for (Int32 idx = 0; idx < lines.Length; idx++) {
                Int32 inline = lines[idx].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
                words += inline;
            }

            CommonView.Output = new StringBuilder()
                .AppendLine($"Lines: {lines.Length}")
                .AppendLine($"Words: {words}")
                .AppendLine($"Chars full: {charsFull}")
                .AppendLine($"Chars w/o spaces: {charsStripped}")
                .ToString();
        }
    }
}
