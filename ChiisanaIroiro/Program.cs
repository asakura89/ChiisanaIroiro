using System;
using System.Windows.Forms;
using Ayumi.Core;
using ChiisanaIroiro.Presenter;
using ChiisanaIroiro.Presenter.Impl;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.Service.Impl;

namespace ChiisanaIroiro
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitializeObjectRegistry();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static void InitializeObjectRegistry()
        {
            IObjectRegistry registry = new ObjectRegistry();
            registry.RegisterObject<IChangeCasePresenter, ChangeCasePresenter>();
            registry.RegisterObject<IChangeCaseService, ChangeCaseService>();
            registry.RegisterObject<IMakeLabelPresenter, MakeLabelPresenter>();
            registry.RegisterObject<IMakeLabelService, MakeLabelService>();
        }
    }
}
