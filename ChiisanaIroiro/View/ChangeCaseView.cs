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
    public partial class ChangeCaseView : UserControl, IChangeCaseViewModel
    {
        private readonly IChangeCasePresenter presenter;

        public ChangeCaseView()
        {
            InitializeComponent();

            IChangeCaseService service = ObjectRegistry.GetRegisteredObject<IChangeCaseService>();
            presenter = ObjectRegistry.GetRegisteredObject<IChangeCasePresenter>(this, service);
            presenter.Initialize();
        }

        public ICommonList CaseType
        {
            get { return new DesktopDropdownList(cmbAvailableCase); }
        }

        public String ProcessedString
        {
            get { return txtInput.Text; }
            set { txtOutput.Text = value; }
        }

        private void btnChangeCase_Click(object sender, EventArgs e)
        {
            try
            {
                presenter.ChangeCaseAction();
                presenter.CaptureAction("Change Case", "Change case has been done.");
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
