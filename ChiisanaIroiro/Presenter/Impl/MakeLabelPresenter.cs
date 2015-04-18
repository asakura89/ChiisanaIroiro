using System;
using System.Collections.Generic;
using Ayumi.Data;
using Ayumi.Extension;
using ChiisanaIroiro.Constant;
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

        public void Initialize()
        {
            viewModel.LabelType.BindToICommonList(GetLabelTypeList());
        }

        private IEnumerable<NameValueItem> GetLabelTypeList()
        {
            yield return new NameValueItem("Normal label", LabelType.NormalLabel);
            yield return new NameValueItem("Region label", LabelType.RegionLabel);
        }

        public void MakeLabelAction()
        {
            NameValueItem selectedLabelType = viewModel.LabelType.SelectedItem;
            switch (selectedLabelType.Value)
            {
                case LabelType.NormalLabel:
                    viewModel.OutputString = service.MakeLabel(viewModel.InputString);
                    break;
                case LabelType.RegionLabel:
                    viewModel.OutputString = service.MakeRegionLabel(viewModel.InputString);
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