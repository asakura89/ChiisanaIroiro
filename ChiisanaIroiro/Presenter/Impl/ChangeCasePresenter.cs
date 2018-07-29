using System;
using System.Collections.Generic;
using Ayumi.Extension;
using ChiisanaIroiro.Constant;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.ViewModel;
using Nvy;

namespace ChiisanaIroiro.Presenter.Impl {
    public class ChangeCasePresenter : IChangeCasePresenter {
        readonly IChangeCaseService service;
        readonly IChangeCaseViewModel viewModel;

        public ChangeCasePresenter(IChangeCaseViewModel viewModel, IChangeCaseService service) {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            this.viewModel = viewModel;
            this.service = service;
        }

        public void Initialize() {
            viewModel.CaseType.BindToICommonList(GetChangeCaseTypeList());
        }

        public void ChangeCaseAction() {
            NameValueItem selectedCaseType = viewModel.CaseType.SelectedItem;
            switch (selectedCaseType.Value) {
                case ChangeCaseType.LowerCase:
                    viewModel.OutputString = service.ToLowerCase(viewModel.InputString);
                    break;
                case ChangeCaseType.UpperCase:
                    viewModel.OutputString = service.ToUpperCase(viewModel.InputString);
                    break;
                case ChangeCaseType.TitleCase:
                    viewModel.OutputString = service.ToTitleCase(viewModel.InputString);
                    break;
            }
        }

        public void CaptureException(Exception ex) {
            viewModel.ErrorMessage = ex.Message;
        }

        public void CaptureAction(String action, String description) { }

        IEnumerable<NameValueItem> GetChangeCaseTypeList() {
            yield return new NameValueItem("Lower Case", ChangeCaseType.LowerCase);
            yield return new NameValueItem("Upper Case", ChangeCaseType.UpperCase);
            yield return new NameValueItem("Title Case", ChangeCaseType.TitleCase);
        }
    }
}