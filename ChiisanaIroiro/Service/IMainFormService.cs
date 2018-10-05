using System;
using System.Collections.Generic;
using Nvy;

namespace ChiisanaIroiro.Service {
    public interface IMainFormService {
        Object GetViewInstance(String viewName);
        IList<NameValueItem> GetFeatures();
    }
}