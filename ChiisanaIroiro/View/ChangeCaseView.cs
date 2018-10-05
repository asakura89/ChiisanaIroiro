using System;
using System.Windows.Forms;
using Ayumi.Core;
using Ayumi.Data;
using ChiisanaIroiro.Presenter;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.Utility;
using ChiisanaIroiro.ViewModel;

namespace ChiisanaIroiro.View {
    public partial class ChangeCaseView : UserControl, IChangeCaseViewModel {
        const String ErrorSessName = "changecaseview.session.errormessage";

        static readonly InMemoryCommonList actions = new InMemoryCommonList();
        readonly IChangeCasePresenter presenter;

        public ChangeCaseView() {
            InitializeComponent();

            IChangeCaseService service = ObjectRegistry.GetRegisteredObject<IChangeCaseService>();
            presenter = ObjectRegistry.GetRegisteredObject<IChangeCasePresenter>(this, service);
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
        public String ViewName => "Change Case";
        public String ViewDesc => "";

        public String InputString {
            get { return txtInput.Text; }
            set { txtInput.Text = value; }
        }

        public String OutputString {
            get { return txtOutput.Text; }
            set { txtOutput.Text = value; }
        }

        void btnChangeCase_Click(object sender, EventArgs e) {
            try {
                presenter.ChangeCaseAction();
                presenter.CaptureAction(ViewName, "Change case has been done.");
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