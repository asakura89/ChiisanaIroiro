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

            var registry = new ObjectRegistry();
            IChangeCaseService service = registry.GetRegisteredObject<IChangeCaseService>();
            presenter = registry.GetRegisteredObject<IChangeCasePresenter>(this, service);
            presenter.Initialize();
        }

        public ICommonList CaseType
        {
            get { return new DesktopDropdownList(cmbAvailableCase); }
        }

        public String ProcessedString
        {
            get { return txtTextCaseInput.Text; }
            set { txtTextCaseOutput.Text = value; }
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
    }
}
