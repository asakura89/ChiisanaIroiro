using System;
using System.Windows.Forms;
using Ayumi.Core;
using Ayumi.Data;
using Ayumi.Desktop;
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
            presenter.Initialize();
        }
        public ICommonList LabelType
        {
            get { return new DesktopDropdownList(cmbLabelType); }
        }

        public String InputString
        {
            get { return txtInput.Text; }
            set { txtInput.Text = value; }
        }

        public String OutputString
        {
            get { return txtOutput.Text; }
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
                if (OutputString != String.Empty)
                {
                    Clipboard.Clear();
                    Clipboard.SetText(OutputString);

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
