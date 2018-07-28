using System.Drawing;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace ChiisanaIroiro {
    public static class TextEditorHelper {
        public static FastColoredTextBox Initialize() {
            return new FastColoredTextBox {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoCompleteBracketsList = new[] {'(', ')', '{', '}', '[', ']', '\"', '\"', '\'', '\''},
                AutoIndentCharsPatterns =
                    "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]*(?<range>:)\\s*(?<range>[^;]+);\r\n",
                AutoScrollMinSize = new Size(27, 15),
                BackBrush = null,
                BorderStyle = BorderStyle.FixedSingle,
                BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2,
                CharHeight = 15,
                CharWidth = 8,
                Cursor = Cursors.IBeam,
                DisabledColor = Color.FromArgb(100, 180, 180, 180),
                Font = new Font("Consolas", 10F),
                IsReplaceMode = false,
                Language = Language.CSharp,
                LeftBracket = '(',
                LeftBracket2 = '{',
                /*Location = new Point(12, 282),*/
                Name = "ClassTextBox",
                Paddings = new Padding(0),
                RightBracket = ')',
                RightBracket2 = '}',
                SelectionColor = Color.FromArgb(60, 0, 0, 255),
                ServiceColors = new ServiceColors {
                    CollapseMarkerBackColor = Color.White,
                    CollapseMarkerBorderColor = Color.Silver,
                    CollapseMarkerForeColor = Color.Silver,
                    ExpandMarkerBackColor = Color.White,
                    ExpandMarkerBorderColor = Color.Silver,
                    ExpandMarkerForeColor = Color.Red
                },
                /*Dock = DockStyle.Fill,*/
                ShowScrollBars = true,
                /*TabIndex = 5,*/
                Zoom = 100
            };
        }
    }
}