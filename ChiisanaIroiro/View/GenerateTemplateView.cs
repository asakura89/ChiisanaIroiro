using System;
using System.Windows.Forms;
using Ayumi.Core;
using Ayumi.Data;
using Ayumi.Desktop;
using ChiisanaIroiro.Presenter;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.ViewModel;

namespace ChiisanaIroiro.View {
    public partial class GenerateTemplateView : UserControl, IGenerateTemplateViewModel {
        readonly IGenerateTemplatePresenter presenter;

        public GenerateTemplateView() {
            InitializeComponent();

            IGenerateTemplateService service = ObjectRegistry.GetRegisteredObject<IGenerateTemplateService>();
            presenter = ObjectRegistry.GetRegisteredObject<IGenerateTemplatePresenter>(this, service);
            presenter.Initialize();
        }

        static readonly InMemoryCommonList templateActions = new InMemoryCommonList();
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