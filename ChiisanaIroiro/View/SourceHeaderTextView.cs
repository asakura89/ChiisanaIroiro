using System;
using System.Windows.Forms;
using ChiisanaIroiro.Presenter;
using ChiisanaIroiro.ViewModel;

namespace ChiisanaIroiro.View {
    public partial class SourceHeaderTextView : UserControl, IMakeHeaderViewModel {
        readonly IMakeHeaderPresenter presenter;

        public SourceHeaderTextView() {
            InitializeComponent();
        }

        public String InputString {
            get { return txtInput.Text; }
            set { txtInput.Text = value; }
        }

        public String OutputString {
            get { return txtOutput.Text; }
            set { txtOutput.Text = value; }
        }

        void btnMakeHeader_Click(object sender, EventArgs e) {
            try {
                presenter.MakeHeaderAction();
                presenter.CaptureAction("Make Header", "Make header has been done.");
            }
            catch (Exception ex) {
                presenter.CaptureException(ex);
            }
        }

        void btnClipboard_Click(object sender, EventArgs e) {
            try {
                if (OutputString != String.Empty) {
                    Clipboard.Clear();
                    Clipboard.SetText(OutputString);

                    MessageBox.Show("Text has been copied to clipboard.");
                }
            }
            catch (Exception ex) {
                presenter.CaptureException(ex);
            }
        }
    }
}