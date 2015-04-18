using System;
using Ayumi.Data;

namespace ChiisanaIroiro.ViewModel
{
    public interface IMakeLabelViewModel
    {
        ICommonList LabelType { get; }
        String InputString { get; set; }
        String OutputString { get; set; } 
    }
}