using System;
using System.Windows.Forms;
using Ayumi.Core;
using Ayumi.Data;
using ChiisanaIroiro.Presenter;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.Utility;
using ChiisanaIroiro.ViewModel;

namespace ChiisanaIroiro.View {
    public partial class StringUtilView : UserControl, IStringUtilViewModel {
        const String ErrorSessName = "stringutilview.session.errormessage";

        static readonly InMemoryCommonList actions = new InMemoryCommonList();
        readonly IStringUtilPresenter presenter;

        public StringUtilView() {
            InitializeComponent();

            IStringUtilService service = ObjectRegistry.GetRegisteredObject<IStringUtilService>();
            presenter = ObjectRegistry.GetRegisteredObject<IStringUtilPresenter>(this, service);
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
        public String ViewName => "String Util";
        public String ViewDesc => "";

        public String InputString {
            get { return txtInput.Text; }
            set { txtInput.Text = value; }
        }

        public String OutputString {
            get { return txtOutput.Text; }
            set { txtOutput.Text = value; }
        }

        void btnProcess_Click(object sender, EventArgs e) {
            try {
                presenter.StringUtilAction();
                presenter.CaptureAction(ViewName, "String util process has been done.");
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