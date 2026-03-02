using System;
using Ayumi.Data;

namespace ChiisanaIroiro.ViewModel {
    public interface IGenerateNumberViewModel : IViewModel, IExceptionOccuredViewModel {
        String InputString { get; set; }
        String OutputString { get; set; }
    }
}