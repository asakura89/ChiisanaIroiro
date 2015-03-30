using System;
using System.Collections.Generic;
using Ayumi.Data;
using Ayumi.Extension;
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
            viewModel.CaseType.BindToICommonList(GetChangeCaseTypeList());
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

        public void CaptureException(Exception ex)
        {

        }

        public void CaptureAction(String action, String description)
        {

        }
    }
}