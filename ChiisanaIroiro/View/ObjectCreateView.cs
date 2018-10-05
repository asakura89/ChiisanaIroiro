using System;
using System.Windows.Forms;
using Ayumi.Core;
using Ayumi.Data;
using Ayumi.Desktop;
using ChiisanaIroiro.Presenter;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.ViewModel;

namespace ChiisanaIroiro.View {
    public partial class ObjectCreateView : UserControl, IObjectCreateViewModel {
        readonly IObjectCreatePresenter presenter;

        public ObjectCreateView() {
            InitializeComponent();

            IObjectCreateService service = ObjectRegistry.GetRegisteredObject<IObjectCreateService>();
            presenter = ObjectRegistry.GetRegisteredObject<IObjectCreatePresenter>(this, service);
            presenter.Initialize();
        }

        static readonly InMemoryCommonList actions = new InMemoryCommonList();
        public ICommonList ViewActions => actions;
        public String ViewName => "Object Create";
        public String ViewDesc => "";

        public String InputString {
            get { return txtInput.Text; }
            set { txtInput.Text = value; }
        }

        public String OutputString {
            get { return txtOutput.Text; }
            set { txtOutput.Text = value; }
        }

        void btnObjectCreate_Click(object sender, EventArgs e) {
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
