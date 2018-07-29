﻿using System;
using Ayumi.Data;

namespace ChiisanaIroiro.ViewModel {
    public interface IGenerateSqlTemplateViewModel : IExceptionOccuredViewModel
    {
        ICommonList TemplateType { get; }
        String InputString { get; set; }
        String OutputString { get; set; }
    }
}