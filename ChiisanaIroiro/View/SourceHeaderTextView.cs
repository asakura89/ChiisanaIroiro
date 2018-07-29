using System;
using System.Windows.Forms;
using Ayumi.Core;
using ChiisanaIroiro.Presenter;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.Utility;
using ChiisanaIroiro.ViewModel;

namespace ChiisanaIroiro.View {
    public partial class SourceHeaderTextView : UserControl, IMakeHeaderViewModel {
        readonly IMakeHeaderPresenter presenter;

        public SourceHeaderTextView() {
            InitializeComponent();

            IMakeHeaderService service = ObjectRegistry.GetRegisteredObject<IMakeHeaderService>();
            presenter = ObjectRegistry.GetRegisteredObject<IMakeHeaderPresenter>(this, service);
            //presenter.Initialize();
        }

        public String InputString {
            get { return txtInput.Text; }
            set { txtInput.Text = value; }
        }

        public String OutputString {
            get { return txtOutput.Text; }
            set { txtOutput.Text = value; }
        }

        const String ErrorSessName = "makeheaderview.session.errormessage";

        public String ErrorMessage {
            get { return Convert.ToString(SessionStore.Get(ErrorSessName)); }
            set {
                SessionStore.Add(ErrorSessName, value);
                MessageBox.Show(this, value);
            }
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