using System;

namespace ChiisanaIroiro.ViewModel {
    public interface IObjectCreateViewModel : IViewModel, IExceptionOccuredViewModel {
        String InputString { get; set; }
        String OutputString { get; set; }
    }
}