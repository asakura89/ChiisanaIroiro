using System;

namespace ChiisanaIroiro.ViewModel {
    public interface IObjectCreateViewModel : IViewModel {
        String InputString { get; set; }
        String OutputString { get; set; }
    }
}