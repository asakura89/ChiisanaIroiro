using System;

namespace ChiisanaIroiro.ViewModel {
    public interface IMakeLabelViewModel : IViewModel, IExceptionOccuredViewModel {
        String InputString { get; set; }
        String OutputString { get; set; }
    }
}