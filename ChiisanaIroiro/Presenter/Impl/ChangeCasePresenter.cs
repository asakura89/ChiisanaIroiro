using System;
using System.Collections.Generic;
using ChiisanaIroiro.Ayumi.Data;
using ChiisanaIroiro.Ayumi.Extension;
using ChiisanaIroiro.Constant;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.ViewModel;

namespace ChiisanaIroiro.Presenter.Impl
{
    public class ChangeCasePresenter : IChangeCasePresenter
    {
        private readonly IChangeCaseViewModel viewModel;
        private readonly IChangeCaseService service;
        public ChangeCasePresenter(IChangeCaseViewModel viewModel, IChangeCaseService service)
        {
            if (viewModel == null)
                throw new ArgumentNullException("viewModel");
            if (service == null)
                throw new ArgumentNullException("service");

            this.viewModel = viewModel;
            this.service = service;
        }

        public void Initialize()
        {
            viewModel.CaseType.BindToICommonList(GetChangeCaseTypeList(), nvi => nvi.Name, nvi => nvi.Value);
        }

        private IEnumerable<NameValueItem> GetChangeCaseTypeList()
        {
            yield return new NameValueItem("Lower Case", ChangeCaseType.LowerCase);
            yield return new NameValueItem("Upper Case", ChangeCaseType.UpperCase);
            yield return new NameValueItem("Title Case", ChangeCaseType.TitleCase);
        }

        public void ChangeCaseAction()
        {
            NameValueItem selectedCaseType = viewModel.CaseType.SelectedItem;
            switch (selectedCaseType.Value)
            {
                case ChangeCaseType.LowerCase:
                    viewModel.ProcessedString = service.ToLowerCase(viewModel.ProcessedString);
                    break;
                case ChangeCaseType.UpperCase:
                    viewModel.ProcessedString = service.ToUpperCase(viewModel.ProcessedString);
                    break;
                case ChangeCaseType.TitleCase:
                    viewModel.ProcessedString = service.ToTitleCase(viewModel.ProcessedString);
                    break;
            }
        }

        public void OnException(Exception ex)
        {

        }

        public void OnAction(string action, string actionDesc)
        {

        }
    }
}