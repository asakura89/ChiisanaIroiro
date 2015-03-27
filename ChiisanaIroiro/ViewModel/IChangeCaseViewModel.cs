using System;
using ChiisanaIroiro.Ayumi.Data;

namespace ChiisanaIroiro.ViewModel
{
    public interface IChangeCaseViewModel
    {
        ICommonList CaseType { get; }
        String ProcessedString { get; set; }
    }
}