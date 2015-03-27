using System;
using Ayumi.Data;

namespace ChiisanaIroiro.ViewModel
{
    public interface IChangeCaseViewModel
    {
        ICommonList CaseType { get; }
        String ProcessedString { get; set; }
    }
}