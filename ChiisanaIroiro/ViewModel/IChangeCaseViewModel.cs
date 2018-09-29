using System;

namespace ChiisanaIroiro.ViewModel {
    public interface IChangeCaseViewModel : IViewModel {
        String InputString { get; set; }
        String OutputString { get; set; }
    }
}