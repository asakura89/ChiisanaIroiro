using System;

namespace ChiisanaIroiro.ViewModel {
    public interface IMakeHeaderViewModel : IExceptionOccuredViewModel
    {
        String InputString { get; set; }
        String OutputString { get; set; }
    }
}