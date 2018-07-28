using System;
using Ayumi.Data;

namespace ChiisanaIroiro.ViewModel {
    public interface IGenerateSqlTemplateViewModel {
        ICommonList TemplateType { get; }
        String InputString { get; set; }
        String OutputString { get; set; }
    }
}