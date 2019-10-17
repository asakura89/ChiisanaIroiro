using System;
using System.Windows;
using System.Windows.Controls;
using Ayumi.ViewablePlugin;
using Keywielder;
using K = Keywielder.Keywielder;

namespace DefaultPlugin {
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

            CommonView.Output = K.New()
                .AddRandomString(Length, GeneratorType)
                .BuildKey();
        }
    }
}
