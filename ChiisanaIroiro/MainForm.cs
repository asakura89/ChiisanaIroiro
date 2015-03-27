using System.Windows.Forms;
using ChiisanaIroiro.Ayumi.Core;
using ChiisanaIroiro.Presenter;
using ChiisanaIroiro.Presenter.Impl;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.Service.Impl;

namespace ChiisanaIroiro
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeObjectRegistry();
        }

        private void InitializeObjectRegistry()
        {
            IObjectRegistry registry = new ObjectRegistry();
            registry.RegisterObject<IChangeCasePresenter, ChangeCasePresenter>();
            registry.RegisterObject<IChangeCaseService, ChangeCaseService>();
        }
    }
}
