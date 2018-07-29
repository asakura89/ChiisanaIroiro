using System;
using Ayumi.Data;

namespace ChiisanaIroiro.ViewModel {
    public interface IMakeLabelViewModel : IExceptionOccuredViewModel
    {
        ICommonList LabelType { get; }
        String InputString { get; set; }
        String OutputString { get; set; }
    }
}