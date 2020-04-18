using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Ayumi.ViewablePlugin;

namespace DefaultPlugin {
    public partial class TimeSpanExplainerView : UserControl, IViewablePlugin {
        public String ComponentName => "Timespan Explainer";

        public String ComponentDesc => String.Empty;

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        public TimeSpanExplainerView() {
            InitializeComponent();
            CommonView.ConfigButtonAccesssor.Visibility = Visibility.Collapsed;
            CommonView.ProcessButtonAccesssor.Click += ProcessButton_Click;
            CommonView.Input = String.Join(Environment.NewLine, new[] {
                "457872ms",
                "5m",
                "422s",
                "23h",
                "1d"
            });
        }

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            String[] inputs = CommonView
                .Input
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            IList<String> outputs = inputs
                .Select(ProcessInput)
                .Select(ConvertToTimeSpan)
                .Select(ExplainTimespan)
                .ToList();

            CommonView.Output = String.Join(Environment.NewLine, outputs);
        }

        static readonly Regex TimeSpanRgx = new Regex("(?<digit>\\d{1,})(?<type>[Dd]|[Hh]|[Mm][Ss]?|[Ss])", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Multiline);

        String ProcessInput(String input) {
            Match match = TimeSpanRgx.Match(input);
            if (match.Success) {
                String digit = match.Groups["digit"].Value;
                String type = match.Groups["type"].Value.ToLowerInvariant();
                if (String.IsNullOrEmpty(digit)) {
                    return "00:00:00:00:00";
                }

                if (String.IsNullOrEmpty(type)) {
                    return "00:00:00:00:00";
                }

                String duration = digit.PadLeft(2, '0');
                switch (type) {
                    case "d":
                        return $"{duration}:00:00:00:00";
                    case "h":
                        return $"00:{duration}:00:00:00";
                    case "m":
                        return $"00:00:{duration}:00:00";
                    case "s":
                        return $"00:00:00:{duration}:00";
                    case "ms":
                        return $"00:00:00:00:{duration}";
                    default:
                        return "00:00:00:00:00";
                }
            }

            return "00:00:00:00:00";
        }

        TimeSpan ConvertToTimeSpan(String timespanString) {
            String[] splitted = timespanString.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            return new TimeSpan(Convert.ToInt32(splitted[0]), Convert.ToInt32(splitted[1]), Convert.ToInt32(splitted[2]), Convert.ToInt32(splitted[3]), Convert.ToInt32(splitted[4]));
        }

        String ExplainTimespan(TimeSpan timespan) =>
            String.Join(Environment.NewLine, new[] {
                timespan.TotalDays.ToString(CultureInfo.InvariantCulture) + " days",
                timespan.TotalHours.ToString(CultureInfo.InvariantCulture) + " hours",
                timespan.TotalMinutes.ToString(CultureInfo.InvariantCulture) + " minutes",
                timespan.TotalSeconds.ToString(CultureInfo.InvariantCulture) + " seconds",
                timespan.TotalMilliseconds.ToString(CultureInfo.InvariantCulture) + " milliseconds",
                Environment.NewLine
            });
    }
}
