using System.Drawing;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace ChiisanaIroiro {
    public static class TextEditorHelper {
        public static void Initialize(FastColoredTextBox editor) {
            editor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            editor.AutoCompleteBracketsList = new[] {'(', ')', '{', '}', '[', ']', '\"', '\"', '\'', '\''};
            editor.AutoIndentCharsPatterns =
                "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            editor.AutoScrollMinSize = new Size(27, 15);
            editor.BackBrush = null;
            editor.BorderStyle = BorderStyle.FixedSingle;
            editor.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;
            editor.CharHeight = 15;
            editor.CharWidth = 8;
            editor.Cursor = Cursors.IBeam;
            editor.DisabledColor = Color.FromArgb(100, 180, 180, 180);
            editor.Font = new Font("Consolas", 10F);
            editor.IsReplaceMode = false;
            editor.Language = Language.CSharp;
            editor.LeftBracket = '(';
            editor.LeftBracket2 = '{';
            editor.Paddings = new Padding(0);
            editor.RightBracket = ')';
            editor.RightBracket2 = '}';
            editor.SelectionColor = Color.FromArgb(60, 0, 0, 255);
            editor.ServiceColors = new ServiceColors {
                CollapseMarkerBackColor = Color.White,
                CollapseMarkerBorderColor = Color.Silver,
                CollapseMarkerForeColor = Color.Silver,
                ExpandMarkerBackColor = Color.White,
                ExpandMarkerBorderColor = Color.Silver,
                ExpandMarkerForeColor = Color.Red
            };
            editor.Dock = DockStyle.Fill;
            editor.ShowScrollBars = true;
            editor.Zoom = 100;
        }
    }
}