using System;
using System.Collections.Generic;
using Ayumi.Data;
using Ayumi.Extension;
using ChiisanaIroiro.Constant;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.ViewModel;
using Nvy;

namespace ChiisanaIroiro.Presenter.Impl {
    public class MakeLabelPresenter : IMakeLabelPresenter {
        readonly IMakeLabelService service;
        readonly IMakeLabelViewModel viewModel;

        public MakeLabelPresenter(IMakeLabelViewModel viewModel, IMakeLabelService service) {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            this.viewModel = viewModel;
            this.service = service;
        }

        public void Initialize() {
            viewModel.LabelType.BindToICommonList(GetLabelTypeList());
        }

        public void MakeLabelAction() {
            NameValueItem selectedLabelType = viewModel.LabelType.SelectedItem;
            switch (selectedLabelType.Value) {
                case LabelType.NormalLabel:
                    viewModel.OutputString = service.MakeLabel(viewModel.InputString);
                    break;
                case LabelType.RegionLabel:
                    viewModel.OutputString = service.MakeRegionLabel(viewModel.InputString);
                    break;
            }
        }

        public void CaptureException(Exception ex) { }

        public void CaptureAction(String action, String description) { }

        IEnumerable<NameValueItem> GetLabelTypeList() {
            yield return new NameValueItem("Normal label", LabelType.NormalLabel);
            yield return new NameValueItem("Region label", LabelType.RegionLabel);
        }
    }
}