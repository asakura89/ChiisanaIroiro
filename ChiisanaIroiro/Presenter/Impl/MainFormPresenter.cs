using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Ayumi.Extension;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.ViewModel;
using Nvy;

namespace ChiisanaIroiro.Presenter.Impl {
    public class MainFormPresenter : IMainFormPresenter {
        readonly IMainFormService service;
        readonly IMainFormViewModel viewModel;

        readonly IList<NameValueItem> features;

        public MainFormPresenter(IMainFormViewModel viewModel, IMainFormService service) {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            this.viewModel = viewModel;
            this.service = service;
            features = service.GetFeatures();
        }

        public void Initialize() {
            viewModel.ViewActions.BindToICommonList(features);
        }

        public void FeatureSelectedAction() {
            NameValueItem selected = viewModel.ViewActions.SelectedItem ?? NameValueItem.Empty;
            String viewName = new Regex("^\\[(?<ViewName>\\w.*)\\]")
                .Match(selected.Name)
                .Groups["ViewName"]
                .Value;

            Object instance = service.GetViewInstance(viewName);
            UserControl view = instance as UserControl;
            viewModel.ActiveView = view;

            var vm = instance as IViewModel;
            vm.ViewActions.SelectedIndex = (vm.ViewActions.Items
                .Select((item, idx) => new {Index = idx, Item = item})
                .SingleOrDefault(item => item.Item.Value == selected.Value) ?? new {Index = -1, Item = NameValueItem.Empty})
                .Index;
        }

        public void FeatureSearchedAction() {
            throw new NotImplementedException();
        }

        public void CaptureException(Exception ex) { }

        public void CaptureAction(String action, String description) { }
    }
}