using System;
using System.Windows.Forms;
using Ayumi.Core;
using ChiisanaIroiro.Presenter;
using ChiisanaIroiro.Presenter.Impl;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.Service.Impl;

namespace ChiisanaIroiro {
    internal static class Program {
        [STAThread]
        static void Main() {
            InitializeObjectRegistry();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        static void InitializeObjectRegistry() {
            ObjectRegistry.RegisterObject<IChangeCaseService, ChangeCaseService>();
            ObjectRegistry.RegisterObject<IChangeCasePresenter, ChangeCasePresenter>();
            ObjectRegistry.RegisterObject<IMakeLabelService, MakeLabelService>();
            ObjectRegistry.RegisterObject<IMakeLabelPresenter, MakeLabelPresenter>();
            ObjectRegistry.RegisterObject<IGenerateTemplateService, GenerateTemplateService>();
            ObjectRegistry.RegisterObject<IGenerateTemplatePresenter, GenerateTemplatePresenter>();
            ObjectRegistry.RegisterObject<IObjectCreateService, ObjectCreateService>();
            ObjectRegistry.RegisterObject<IObjectCreatePresenter, ObjectCreatePresenter>();
            ObjectRegistry.RegisterObject<IMainFormService, MainFormService>();
            ObjectRegistry.RegisterObject<IMainFormPresenter, MainFormPresenter>();
        }
    }
}