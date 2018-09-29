using System;

namespace ChiisanaIroiro.ViewModel {
    public interface IMakeLabelViewModel : IViewModel {
        String InputString { get; set; }
        String OutputString { get; set; }
    }
}