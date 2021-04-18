using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;

namespace Plugin.Common {
    public static class TextEditorHelper {
        public static void Initialize(TextEditor editor) {
            editor.FontFamily = new FontFamily("Consolas");
            editor.FontSize = 12;
            editor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("C#");
            editor.ShowLineNumbers = true;
            editor.Encoding = Encoding.UTF8;
            editor.WordWrap = false;
            editor.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            editor.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            editor.Options = new TextEditorOptions {
                ConvertTabsToSpaces = true,
                EnableHyperlinks = false,
                EnableEmailHyperlinks = false,
                HighlightCurrentLine = true,
                IndentationSize = 4,
                EnableRectangularSelection = true
            };
        }
    }
}