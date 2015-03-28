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

            IMakeLabelService service = ObjectRegistry.GetRegisteredObject<IMakeLabelService>();
            presenter = ObjectRegistry.GetRegisteredObject<IMakeLabelPresenter>(this, service);
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

        private void btnClipboard_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtInput.Text != String.Empty)
                {
                    Clipboard.Clear();
                    Clipboard.SetText(txtOutput.Text);

                    MessageBox.Show("Text has been copied to clipboard.");
                }
            }
            catch (Exception ex)
            {
                presenter.CaptureException(ex);
            }
        }
    }
}
