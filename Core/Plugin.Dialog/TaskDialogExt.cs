using Microsoft.WindowsAPICodePack.Dialogs;

namespace Plugin.Dialog {
    public static class TaskDialogExt {
        public static void ShowInTaskDialog(this Exception ex) => new TaskDialog {
            Caption = "Application Error ",
            InstructionText = "Relax and report to Admin (*￣3￣)╭",
            Text = ex.Message,
            Icon = TaskDialogStandardIcon.Error,
            Cancelable = false
        }
        .Show();
    }
}
