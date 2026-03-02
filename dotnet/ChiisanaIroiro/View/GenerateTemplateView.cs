using System;
using System.Windows.Forms;
using Ayumi.Core;
using Ayumi.Data;
using ChiisanaIroiro.Presenter;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.Utility;
using ChiisanaIroiro.ViewModel;

namespace ChiisanaIroiro.View {
    public partial class GenerateTemplateView : UserControl, IGenerateTemplateViewModel {
        const String ErrorSessName = "generatetemplateview.session.errormessage";

        static readonly InMemoryCommonList templateActions = new InMemoryCommonList();
        readonly IGenerateTemplatePresenter presenter;

        public GenerateTemplateView() {
            InitializeComponent();

            IGenerateTemplateService service = ObjectRegistry.GetRegisteredObject<IGenerateTemplateService>();
            presenter = ObjectRegistry.GetRegisteredObject<IGenerateTemplatePresenter>(this, service);
            presenter.Initialize();
        }

        public String ErrorMessage {
            get { return Convert.ToString(SessionStore.Get(ErrorSessName)); }
            set {
                SessionStore.Add(ErrorSessName, value);
                MessageBox.Show(this, value);
            }
        }

        public ICommonList ViewActions => templateActions;
        public String ViewName => "Generate Template";
        public String ViewDesc => "";

        public String OutputString {
            get { return txtOutput.Text; }
            set { txtOutput.Text = value; }
        }

        void btnGenerate_Click(object sender, EventArgs e) {
            try {
                presenter.GenerateAction();
                presenter.CaptureAction(ViewName, "Generate has been done.");
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