using System;
using System.Collections.Generic;
using Ayumi.Extension;
using ChiisanaIroiro.Constant;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.ViewModel;
using Nvy;

namespace ChiisanaIroiro.Presenter.Impl {
    public class GenerateSqlTemplatePresenter : IGenerateSqlTemplatePresenter {
        readonly IGenerateSqlTemplateService service;
        readonly IGenerateSqlTemplateViewModel viewModel;

        public GenerateSqlTemplatePresenter(IGenerateSqlTemplateViewModel viewModel, IGenerateSqlTemplateService service)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            this.viewModel = viewModel;
            this.service = service;
        }

        public void Initialize()
        {
            viewModel.TemplateType.BindToICommonList(GetTemplateTypeList());
        }

        public void GenerateAction()
        {
            NameValueItem selectedCaseType = viewModel.TemplateType.SelectedItem;
            switch (selectedCaseType.Value)
            {
                case GenerateSqlTemplateType.ActionTemplate:
                    viewModel.OutputString = service.GenerateActionTemplate();
                    break;
                case GenerateSqlTemplateType.RetrieveTemplate:
                    viewModel.OutputString = service.GenerateRetrieveTemplate();
                    break;
            }
        }

        public void CaptureException(Exception ex) { }

        public void CaptureAction(String action, String description) { }

        IEnumerable<NameValueItem> GetTemplateTypeList()
        {
            yield return new NameValueItem("Action Template", GenerateSqlTemplateType.ActionTemplate);
            yield return new NameValueItem("Retrieve Template", GenerateSqlTemplateType.RetrieveTemplate);
        }
    }
}