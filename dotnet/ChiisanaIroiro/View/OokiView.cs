using System;
using System.Windows.Forms;
using Ayumi.Core;
using Ayumi.Data;
using ChiisanaIroiro.Presenter;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.Utility;
using ChiisanaIroiro.ViewModel;

namespace ChiisanaIroiro.View {
    public partial class OokiView : UserControl, IOokiViewModel {
        const String ErrorSessName = "generatenumberview.session.errormessage";

        static readonly InMemoryCommonList actions = new InMemoryCommonList();
        readonly IOokiPresenter presenter;

        public OokiView() {
            InitializeComponent();

            IOokiService service = ObjectRegistry.GetRegisteredObject<IOokiService>();
            presenter = ObjectRegistry.GetRegisteredObject<IOokiPresenter>(this, service);
            presenter.Initialize();
        }

        public ICommonList ViewActions => actions;
        public String ViewName => "Ooki";
        public String ViewDesc => "";

        public String InputString {
            get { return txtInput.Text; }
            set { txtInput.Text = value; }
        }

        public String OutputString {
            get { return txtOutput.Text; }
            set { txtOutput.Text = value; }
        }

        public String ErrorMessage {
            get { return Convert.ToString(SessionStore.Get(ErrorSessName)); }
            set {
                SessionStore.Add(ErrorSessName, value);
                MessageBox.Show(this, value);
            }
        }

        void btnOoki_Click(object sender, EventArgs e) {
            try {
                presenter.CreateObjectAction();
                presenter.CaptureAction(ViewName, "Object create has been done.");
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