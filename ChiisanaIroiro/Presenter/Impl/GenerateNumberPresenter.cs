using System;
using System.Collections.Generic;
using Ayumi.Extension;
using ChiisanaIroiro.Constant;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.ViewModel;
using Nvy;

namespace ChiisanaIroiro.Presenter.Impl {
    public class GenerateNumberPresenter : IGenerateNumberPresenter {
        readonly IGenerateNumberService service;
        readonly IGenerateNumberViewModel viewModel;

        public GenerateNumberPresenter(IGenerateNumberViewModel viewModel, IGenerateNumberService service) {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            this.viewModel = viewModel;
            this.service = service;
        }

        public void Initialize() {
            viewModel.ViewActions.BindToICommonList(GetNumberTypeList());

            viewModel.InputString = @"{
    ""Start"": 3,
    ""Length"": 12,
    ""Iterate"": 10,
    ""Base64"": false,
    ""Base64Url"": false,
    ""Pad"": ""No"" // No, Left, Right
}";
        }

        public void GenerateAction() {
            NameValueItem selectedCaseType = viewModel.ViewActions.SelectedItem;
            switch (selectedCaseType.Value) {
                case NumberType.ProcessId:
                    viewModel.OutputString = service.GenerateProcessId(viewModel.InputString);
                    break;
                case NumberType.RandomNumber:
                    viewModel.OutputString = service.GenerateRandomNumber(viewModel.InputString);
                    break;
                case NumberType.RandomHex:
                    viewModel.OutputString = service.GenerateRandomHexNumber(viewModel.InputString);
                    break;
                case NumberType.RandomGuid:
                    viewModel.OutputString = service.GenerateRandomGuid(viewModel.InputString);
                    break;
            }
        }

        public void CaptureException(Exception ex) {
            viewModel.ErrorMessage = ex.Message;
        }

        public void CaptureAction(String action, String description) { }

        IEnumerable<NameValueItem> GetNumberTypeList() {
            yield return new NameValueItem("Process Id", NumberType.ProcessId);
            yield return new NameValueItem("Random Number", NumberType.RandomNumber);
            yield return new NameValueItem("Random Hex", NumberType.RandomHex);
            yield return new NameValueItem("Random Guid", NumberType.RandomGuid);
        }
    }
}