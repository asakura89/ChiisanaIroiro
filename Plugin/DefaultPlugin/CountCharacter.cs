using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ayumi.Plugin;

namespace DefaultPlugin {
    public class CountCharacter : IPlugin {
        public String Name => "Count Character";

        public String Desc => "Count character per line and total";

        public Object Process(Object processArgs) {
            String input = Convert.ToString(processArgs);
            IList<String> lines = input
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

            return outputBuilder
                .Append($"[All] {totalFull} (full), {totalStripped} (w/o spaces)")
                .ToString();

            /*
            Test with these

            ( ╯°□°)╯ ┻━━┻
            (╮°-°)╮┳━━┳
            (╮°-°)╮┳━━┳
            (╯°益°)╯彡┻━┻
            ԅ(≖‿≖ԅ)
            TheDragonQuest

            */
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
