using System;
using System.Windows.Forms;
using Ayumi.Core;
using Ayumi.Data;
using Ayumi.Desktop;
using ChiisanaIroiro.Presenter;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.Utility;
using ChiisanaIroiro.ViewModel;

namespace ChiisanaIroiro.View {
    public partial class GenerateNumberView : UserControl, IGenerateNumberViewModel {
        readonly IGenerateNumberPresenter presenter;

        public GenerateNumberView() {
            InitializeComponent();

            IGenerateNumberService service = ObjectRegistry.GetRegisteredObject<IGenerateNumberService>();
            presenter = ObjectRegistry.GetRegisteredObject<IGenerateNumberPresenter>(this, service);
            presenter.Initialize();
        }

        public ICommonList NumberType => new DesktopDropdownList(cmbAvailableType);

        public String InputString {
            get { return txtInput.Text; }
            set { txtInput.Text = value; }
        }

        public String OutputString {
            get { return txtOutput.Text; }
            set { txtOutput.Text = value; }
        }

        const String ErrorSessName = "generatenumberview.session.errormessage";

        public String ErrorMessage {
            get { return Convert.ToString(SessionStore.Get(ErrorSessName)); }
            set {
                SessionStore.Add(ErrorSessName, value);
                MessageBox.Show(this, value);
            }
        }

        void btnGenerate_Click(object sender, EventArgs e) {
            try {
                presenter.GenerateAction();
                presenter.CaptureAction("Generate Number", "Generate has been done.");
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

        void cmbAvailableTypeOnSelectedIndexChanged(object sender, EventArgs eventArgs) {
            try { }
            catch (Exception ex) {
                presenter.CaptureException(ex);
            }
        }
    }
}