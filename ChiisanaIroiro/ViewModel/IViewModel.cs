using System;
using Ayumi.Data;

namespace ChiisanaIroiro.ViewModel {
    public interface IViewModel {
        String ViewName { get; }
        String ViewDesc { get; }
        ICommonList ViewActions { get; }
    }
}