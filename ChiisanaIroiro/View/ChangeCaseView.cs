using System;
using System.Windows.Forms;
using Ayumi.Core;
using Ayumi.Data;
using Ayumi.Desktop;
using ChiisanaIroiro.Presenter;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.ViewModel;

namespace ChiisanaIroiro.View {
    public partial class ChangeCaseView : UserControl, IChangeCaseViewModel {
        readonly IChangeCasePresenter presenter;

        public ChangeCaseView() {
            InitializeComponent();

            IChangeCaseService service = ObjectRegistry.GetRegisteredObject<IChangeCaseService>();
            presenter = ObjectRegistry.GetRegisteredObject<IChangeCasePresenter>(this, service);
            presenter.Initialize();
        }

        static readonly InMemoryCommonList actions = new InMemoryCommonList();
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