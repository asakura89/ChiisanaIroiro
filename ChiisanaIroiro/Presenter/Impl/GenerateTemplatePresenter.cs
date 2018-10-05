using System;
using System.Collections.Generic;
using Ayumi.Extension;
using ChiisanaIroiro.Constant;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.ViewModel;
using Nvy;

namespace ChiisanaIroiro.Presenter.Impl {
    public class GenerateTemplatePresenter : IGenerateTemplatePresenter {
        readonly IGenerateTemplateService service;
        readonly IGenerateTemplateViewModel viewModel;

        public GenerateTemplatePresenter(IGenerateTemplateViewModel viewModel, IGenerateTemplateService service) {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            this.viewModel = viewModel;
            this.service = service;
        }

        public void Initialize() {
            viewModel.ViewActions.BindToICommonList(GetTemplateTypeList());
        }

        public void GenerateAction() {
            NameValueItem selectedCaseType = viewModel.ViewActions.SelectedItem;
            viewModel.OutputString = service.GenerateTemplate(selectedCaseType.Value);
        }

        public void CaptureException(Exception ex) { }

        public void CaptureAction(String action, String description) { }

        IEnumerable<NameValueItem> GetTemplateTypeList() {
            yield return new NameValueItem("Sql Action Template", GenerateTemplateType.SqlAction);
            yield return new NameValueItem("Sql Retrieve Template", GenerateTemplateType.SqlRetrieve);
            yield return new NameValueItem("Javascript Ajax Template", GenerateTemplateType.JsAjax);
            yield return new NameValueItem("Javascript Timer Template", GenerateTemplateType.JsTimer);
        }
    }
}