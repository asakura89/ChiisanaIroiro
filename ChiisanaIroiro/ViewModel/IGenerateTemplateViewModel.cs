using System;

namespace ChiisanaIroiro.ViewModel {
    public interface IGenerateTemplateViewModel : IViewModel, IExceptionOccuredViewModel {
        String OutputString { get; set; }
    }
}