using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Puru.Wpf;

namespace TextProcessingPlugin {
    public partial class CountCharacterView : UserControl, IViewablePlugin {
        public String ComponentName => "Count Character";

        public String ComponentDesc => "Count character per line and total";

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        public CountCharacterView() {
            InitializeComponent();
            CommonView.ConfigButtonAccesssor.Visibility = Visibility.Collapsed;
            CommonView.ProcessButtonAccesssor.Click += ProcessButton_Click;
        }

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            IList<String> lines = CommonView.Input
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                .ToList();

            Int32 digits = Digits(lines.Count);
            digits = digits < 3 ? 3 : digits;

            var outputBuilder = new StringBuilder();
            Int32 totalFull = 0, totalStripped = 0;
            for (Int32 idx = 0; idx < lines.Count; idx++) {
                Int32 full = lines[idx].Length;
                Int32 stripped = lines[idx].Trim().Replace(" ", String.Empty).Length;
                totalFull += full;
                totalStripped += stripped;

                outputBuilder.AppendLine($"[{(idx +1).ToString().PadLeft(digits, ' ')}] {full} (full), {stripped} (w/o spaces)");
            }

            /*
            Test with these

            ( ╯°□°)╯ ┻━━┻
            (╮°-°)╮┳━━┳
            (╮°-°)╮┳━━┳
            (╯°益°)╯彡┻━┻
            ԅ(≖‿≖ԅ)
            TheDragonQuest

            */

            CommonView.Output = outputBuilder
                .Append($"[All] {totalFull} (full), {totalStripped} (w/o spaces)")
                .ToString();
        }

        Int32 Digits(Int32 n) {
            n = Math.Abs(n);
            if (n < 10) return 1;
            if (n < 100) return 2;
            if (n < 1000) return 3;
            if (n < 10000) return 4;
            if (n < 100000) return 5;
            if (n < 1000000) return 6;
            if (n < 10000000) return 7;
            if (n < 100000000) return 8;
            if (n < 1000000000) return 9;
            return 10;
        }
    }
}
