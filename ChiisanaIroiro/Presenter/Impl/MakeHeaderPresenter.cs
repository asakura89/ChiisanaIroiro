using System;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.ViewModel;

namespace ChiisanaIroiro.Presenter.Impl
{
    public class MakeHeaderPresenter : IMakeHeaderPresenter
    {
        private readonly IMakeHeaderViewModel viewModel;
        private readonly IMakeHeaderService service;

        public MakeHeaderPresenter(IMakeHeaderViewModel viewModel, IMakeHeaderService service)
        {
            if (viewModel == null)
                throw new ArgumentNullException("viewModel");
            if (service == null)
                throw new ArgumentNullException("service");

            this.viewModel = viewModel;
            this.service = service;
        }

        public void CaptureException(Exception ex)
        {

        }

        public void CaptureAction(String action, String description)
        {

        }

        public void MakeHeaderAction()
        {
            viewModel.OutputString = service.MakeHeader(viewModel.InputString);
        }
    }
}