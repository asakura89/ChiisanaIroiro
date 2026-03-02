using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Keywielder;
using Puru.Wpf;

namespace GenerateRandomPlugin {
    public partial class GenerateAlphanumericView : UserControl, IViewablePlugin {
        public String ComponentName => "Generate Alphanumeric";

        public String ComponentDesc => "Generate alphanumeric character";

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        Boolean Uppercase => UpperCheckbox.IsChecked ?? false;

        Boolean Lowercase => LowerCheckbox.IsChecked ?? false;

        Boolean Numeric => NumericCheckbox.IsChecked ?? false;

        Boolean Symbol => SymbolCheckbox.IsChecked ?? false;

        Int32 Length {
            get {
                String length = String.IsNullOrEmpty(LengthTextbox.Text) ? "0" : LengthTextbox.Text;
                return Convert.ToInt32(length);
            }
        }

        Int32 Count {
            get {
                String count = String.IsNullOrEmpty(CountTextbox.Text) ? "0" : CountTextbox.Text;
                return Convert.ToInt32(count);
            }
        }

        AlphaType GeneratorType { get; set; } = AlphaType.Lower;

        public GenerateAlphanumericView() {
            InitializeComponent();
            CommonView.HideAllButton();
            LowerCheckbox.IsChecked = true;
        }

        void ClipboardButton_Click(Object sender, RoutedEventArgs e) {
            if (CommonView.Output != String.Empty) {
                Clipboard.Clear();
                Clipboard.SetText(CommonView.Output);
            }
        }

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            if (Count > 0) {
                var builder = new StringBuilder();
                for (Int32 counter = 0; counter < Count; counter++) {
                    if (Uppercase && Lowercase && Numeric && Symbol)
                        GeneratorType = AlphaType.UpperLowerNumericSymbol;
                    else if (Uppercase && Lowercase && Numeric)
                        GeneratorType = AlphaType.UpperLowerNumeric;
                    else if (Uppercase && Lowercase && Symbol)
                        GeneratorType = AlphaType.UpperLowerSymbol;
                    else if (Uppercase && Lowercase)
                        GeneratorType = AlphaType.UpperLower;
                    else if (Uppercase && Numeric)
                        GeneratorType = AlphaType.UpperNumeric;
                    else if (Uppercase && Symbol)
                        GeneratorType = AlphaType.UpperSymbol;
                    else if (Uppercase)
                        GeneratorType = AlphaType.Upper;
                    else if (Lowercase && Numeric)
                        GeneratorType = AlphaType.LowerNumeric;
                    else if (Lowercase && Symbol)
                        GeneratorType = AlphaType.LowerSymbol;
                    else if (Lowercase)
                        GeneratorType = AlphaType.Lower;
                    else if (Numeric && Symbol)
                        GeneratorType = AlphaType.NumericSymbol;
                    else if (Numeric)
                        GeneratorType = AlphaType.Numeric;
                    else if (Symbol)
                        GeneratorType = AlphaType.Symbol;

                    builder.AppendLine(
                        Wielder.New()
                            .AddRandomString(Length, GeneratorType)
                            .BuildKey());
                }

                CommonView.Output = builder.ToString();
            }
        }
    }
}
