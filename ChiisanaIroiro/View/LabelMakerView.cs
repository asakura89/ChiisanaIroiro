using System;
using System.Windows.Forms;
using Ayumi.Core;
using Ayumi.Data;
using ChiisanaIroiro.Presenter;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.Utility;
using ChiisanaIroiro.ViewModel;

namespace ChiisanaIroiro.View {
    public partial class LabelMakerView : UserControl, IMakeLabelViewModel {
        const String ErrorSessName = "labelmakerview.session.errormessage";

        static readonly InMemoryCommonList actions = new InMemoryCommonList();
        readonly IMakeLabelPresenter presenter;

        public LabelMakerView() {
            InitializeComponent();

            IMakeLabelService service = ObjectRegistry.GetRegisteredObject<IMakeLabelService>();
            presenter = ObjectRegistry.GetRegisteredObject<IMakeLabelPresenter>(this, service);
            presenter.Initialize();
        }

        public String ErrorMessage {
            get { return Convert.ToString(SessionStore.Get(ErrorSessName)); }
            set {
                SessionStore.Add(ErrorSessName, value);
                MessageBox.Show(this, value);
            }
        }

        public ICommonList ViewActions => actions;
        public String ViewName => "Label Maker";
        public String ViewDesc => "";

        public String InputString {
            get { return txtInput.Text; }
            set { txtInput.Text = value; }
        }

        public String OutputString {
            get { return txtOutput.Text; }
            set { txtOutput.Text = value; }
        }

        void btnMakeLabel_Click(object sender, EventArgs e) {
            try {
                presenter.MakeLabelAction();
                presenter.CaptureAction(ViewName, "Make label has been done.");
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

                    presenter.CaptureAction(ViewName, "Text has been copied to clipboard.");
                }
            }
            catch (Exception ex) {
                presenter.CaptureException(ex);
            }
        }
    }
}