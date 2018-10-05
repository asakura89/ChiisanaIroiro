using System;
using System.Windows.Forms;

namespace ChiisanaIroiro.ViewModel {
    public interface IMainFormViewModel : IViewModel, IExceptionOccuredViewModel {
        String SearchedFeature { get; set; }
        UserControl ActiveView { get; set; }
    }
}