using System;
using Ayumi.Data;

namespace ChiisanaIroiro.ViewModel {
    public interface IChangeCaseViewModel : IExceptionOccuredViewModel {
        ICommonList CaseType { get; }
        String InputString { get; set; }
        String OutputString { get; set; }
    }
}