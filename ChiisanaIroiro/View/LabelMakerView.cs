using System;
using System.Windows.Forms;
using Ayumi.Core;
using ChiisanaIroiro.Presenter;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.ViewModel;

namespace ChiisanaIroiro.View
{
    public partial class LabelMakerView : UserControl, IMakeLabelViewModel
    {
        private readonly IMakeLabelPresenter presenter;

        public LabelMakerView()
        {
            InitializeComponent();

            var registry = new ObjectRegistry();
            IMakeLabelService service = registry.GetRegisteredObject<IMakeLabelService>();
            presenter = registry.GetRegisteredObject<IMakeLabelPresenter>(this, service);
        }

        public string ProcessedString
        {
            get { return txtInput.Text; }
            set { txtOutput.Text = value; }
        }

        private void btnMakeLabel_Click(object sender, EventArgs e)
        {
            try
            {
                presenter.MakeLabelAction();
                presenter.CaptureAction("Make Label", "Make label has been done.");
            }
            catch (Exception ex)
            {
                presenter.CaptureException(ex);
            }
        }
    }
}
