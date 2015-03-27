using System;
using System.Windows.Forms;
using ChiisanaIroiro.Ayumi.Core;
using ChiisanaIroiro.Ayumi.Data;
using ChiisanaIroiro.Ayumi.Desktop;
using ChiisanaIroiro.Presenter;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.ViewModel;

namespace ChiisanaIroiro.View
{
    public partial class ChangeCaseView : UserControl, IChangeCaseViewModel
    {
        private readonly IChangeCasePresenter Presenter;

        public ChangeCaseView()
        {
            InitializeComponent();

            var registry = new ObjectRegistry();
            IChangeCaseService service = registry.GetRegisteredObject<IChangeCaseService>();
            Presenter = registry.GetRegisteredObject<IChangeCasePresenter>(this, service);
            Presenter.Initialize();
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
                Presenter.ChangeCaseAction();
            }
            catch (Exception ex)
            {
                Presenter.OnException(ex);
            }
        }
    }
}
