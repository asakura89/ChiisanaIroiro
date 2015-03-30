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
            ObjectRegistry.RegisterObject<IChangeCasePresenter, ChangeCasePresenter>();
            ObjectRegistry.RegisterObject<IChangeCaseService, ChangeCaseService>();
            ObjectRegistry.RegisterObject<IMakeLabelPresenter, MakeLabelPresenter>();
            ObjectRegistry.RegisterObject<IMakeLabelService, MakeLabelService>();
            ObjectRegistry.RegisterObject<IMakeHeaderPresenter, MakeHeaderPresenter>();
            ObjectRegistry.RegisterObject<IMakeHeaderService, MakeHeaderService>();
        }
    }
}
