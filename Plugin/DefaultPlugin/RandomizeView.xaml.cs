using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ayumi.ViewablePlugin;

namespace DefaultPlugin {
    public partial class RandomizeView : UserControl, IViewablePlugin {
        public String ComponentName => "Randomize";

        public String ComponentDesc => "Randomize then take a few";

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        Boolean TakeAll => TakeAllCheckbox.IsChecked ?? false;

        Int32 Take {
            get {
                String iterate = String.IsNullOrEmpty(TakeTextbox.Text) ? "0" : TakeTextbox.Text;
                return Convert.ToInt32(iterate);
            }
        }

        public RandomizeView() {
            InitializeComponent();
            CommonView.HideAllButton();
            TakeAllCheckbox.IsChecked = true;
            TakeAllCheckbox_OnClick(TakeAllCheckbox, new RoutedEventArgs(MouseUpEvent));
        }

        void ClipboardButton_Click(Object sender, RoutedEventArgs e) {
            if (CommonView.Output != String.Empty) {
                Clipboard.Clear();
                Clipboard.SetText(CommonView.Output);
            }
        }

        Int32 GetRandomNumber(Int32 lowerBound, Int32 upperBound) {
            Int32 seed = Guid.NewGuid().GetHashCode() % 50001;
            var rnd = new Random(seed);
            return rnd.Next(lowerBound, upperBound);
        }

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            String[] strings = CommonView.Input.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            Int32 ctr = 0;
            while (ctr < strings.Length) {
                Int32 randIdx = GetRandomNumber(0, ctr +1);

                String temp = strings[randIdx];
                strings[randIdx] = strings[ctr];
                strings[ctr] = temp;

                ctr++;
            }

            var newList = new List<String>();
            if (!TakeAll)
                strings = strings.Take(Take).ToArray();

            newList.AddRange(strings);
            CommonView.Output = String.Join(Environment.NewLine, newList);
        }

        void NumericTextbox_KeyUp(Object sender, KeyEventArgs e) {

        }

        void TakeAllCheckbox_OnClick(Object sender, RoutedEventArgs e) => TakeTextbox.IsEnabled = !TakeAll;
    }
}
