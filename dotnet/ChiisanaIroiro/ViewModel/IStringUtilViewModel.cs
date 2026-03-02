using System;

namespace ChiisanaIroiro.ViewModel {
    public interface IStringUtilViewModel : IViewModel, IExceptionOccuredViewModel {
        String InputString { get; set; }
        String OutputString { get; set; }
    }
}