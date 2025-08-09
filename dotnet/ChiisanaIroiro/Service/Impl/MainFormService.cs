using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Ayumi;
using ChiisanaIroiro.ViewModel;
using Nvy;

namespace ChiisanaIroiro.Service.Impl {
    public class MainFormService : IMainFormService {
        readonly IList<Object> viewInstances;

        public MainFormService() {
            viewInstances = GetViewInstances();
        }

        public Object GetViewInstance(String viewName) =>
            viewInstances
                .SingleOrDefault(v => ((IViewModel) v)
                    .ViewName.Equals(viewName, StringComparison.InvariantCultureIgnoreCase));

        public IList<NameValueItem> GetFeatures() =>
            viewInstances
                .Cast<IViewModel>()
                .SelectMany(ivm => ivm
                    .ViewActions
                    .Items
                    .Where(item => !String.IsNullOrEmpty(item.Value))
                    .AsNameValueList(item => $"[{ivm.ViewName}] {item.Name}", item => item.Value))
                .ToList();

        IList<Object> GetViewInstances() {
            IEnumerable<Type> controls = DynamicTypeLoader.GetTypesInheritedBy<UserControl>(AppDomain.CurrentDomain.GetAssemblies());
            if (controls == null || !controls.Any())
                return null;

            return controls
                .Select(Activator.CreateInstance)
                .Where(c => c is IViewModel)
                .ToList();
        }
    }
}