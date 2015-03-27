using System;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.ViewModel;

namespace ChiisanaIroiro.Presenter.Impl
{
    public class MakeLabelPresenter : IMakeLabelPresenter
    {
        private readonly IMakeLabelViewModel viewModel;
        private readonly IMakeLabelService service;

        public MakeLabelPresenter(IMakeLabelViewModel viewModel, IMakeLabelService service)
        {
            if (viewModel == null)
                throw new ArgumentNullException("viewModel");
            if (service == null)
                throw new ArgumentNullException("service");

            this.viewModel = viewModel;
            this.service = service;
        }

        public void MakeLabelAction()
        {
            viewModel.ProcessedString = service.MakeLabel(viewModel.ProcessedString);
        }

        public void CaptureException(Exception ex)
        {
            
        }

        public void CaptureAction(String action, String description)
        {

        }
    }
}