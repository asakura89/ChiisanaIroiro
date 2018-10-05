using System;
using System.Collections.Generic;
using Ayumi.Extension;
using ChiisanaIroiro.Constant;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.ViewModel;
using Nvy;

namespace ChiisanaIroiro.Presenter.Impl {
    public class ObjectCreatePresenter : IObjectCreatePresenter {
        readonly IObjectCreateService service;
        readonly IObjectCreateViewModel viewModel;

        public ObjectCreatePresenter(IObjectCreateViewModel viewModel, IObjectCreateService service) {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            this.viewModel = viewModel;
            this.service = service;
        }

        public void Initialize() {
            viewModel.ViewActions.BindToICommonList(GetObjectTypeList());
            viewModel.InputString = @"[
    {
        ""Name"": ""Client"",
        ""Properties"": [
            ""Name:str"",       // Name:Type
            ""Address:str"",
            ""Pic:str"",
        ]
    }
]
/*

Type:

String:str
Boolean:boo
Int32:int
Int64:lint
Decimal:dec
Single:sin
Double:dou
CustomType:CustomType

*/";
        }

        public void CaptureException(Exception ex) { }

        public void CaptureAction(String action, String description) { }

        public void CreateObjectAction() {
            NameValueItem selected = viewModel.ViewActions.SelectedItem;
            switch (selected.Value) {
                case ObjectType.Normal:
                    viewModel.OutputString = service.CreateObject(viewModel.InputString);
                    break;
                case ObjectType.Notified:
                    viewModel.OutputString = service.CreateNotifiedObject(viewModel.InputString);
                    break;
            }
        }

        IEnumerable<NameValueItem> GetObjectTypeList() {
            yield return new NameValueItem("Normal object", ObjectType.Normal);
            yield return new NameValueItem("Notified object", ObjectType.Notified);
        }
    }
}