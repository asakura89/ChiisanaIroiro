using System;
using Ayumi.Data;

namespace ChiisanaIroiro.ViewModel {
    public interface IGenerateNumberViewModel : IExceptionOccuredViewModel
    {
        ICommonList NumberType { get; }
        String InputString { get; set; }
        String OutputString { get; set; }
    }
}