using System;

namespace ChiisanaIroiro.ViewModel {
    public interface IChangeCaseViewModel : IViewModel, IExceptionOccuredViewModel {
        String InputString { get; set; }
        String OutputString { get; set; }
    }
}