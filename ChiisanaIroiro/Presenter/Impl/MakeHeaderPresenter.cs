using System;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.ViewModel;

namespace ChiisanaIroiro.Presenter.Impl {
    public class MakeHeaderPresenter : IMakeHeaderPresenter {
        readonly IMakeHeaderService service;
        readonly IMakeHeaderViewModel viewModel;

        public MakeHeaderPresenter(IMakeHeaderViewModel viewModel, IMakeHeaderService service) {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            this.viewModel = viewModel;
            this.service = service;
        }

        public void CaptureException(Exception ex) {
            viewModel.ErrorMessage = ex.Message;
        }

        public void CaptureAction(String action, String description) { }

        public void MakeHeaderAction() {
            viewModel.OutputString = service.MakeHeader(viewModel.InputString);
        }
    }
}