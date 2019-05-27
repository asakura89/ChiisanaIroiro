using System;
using System.Collections.Generic;
using Ayumi.Extension;
using ChiisanaIroiro.Constant;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.ViewModel;
using Nvy;

namespace ChiisanaIroiro.Presenter.Impl {
    public class StringUtilPresenter : IStringUtilPresenter {
        readonly IStringUtilService service;
        readonly IStringUtilViewModel viewModel;

        public StringUtilPresenter(IStringUtilViewModel viewModel, IStringUtilService service) {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            this.viewModel = viewModel;
            this.service = service;
        }

        public void Initialize() {
            viewModel.ViewActions.BindToICommonList(GetStringUtilTypeList());
        }

        public void StringUtilAction() {
            NameValueItem selectedCaseType = viewModel.ViewActions.SelectedItem;
            switch (selectedCaseType.Value) {
                case StringUtilType.SortList:
                    viewModel.OutputString = service.SortStringList(viewModel.InputString);
                    break;
            }
        }

        public void CaptureException(Exception ex) {
            viewModel.ErrorMessage = ex.Message;
        }

        public void CaptureAction(String action, String description) { }

        IEnumerable<NameValueItem> GetStringUtilTypeList() {
            yield return new NameValueItem("Sort List", StringUtilType.SortList);
        }
    }
}