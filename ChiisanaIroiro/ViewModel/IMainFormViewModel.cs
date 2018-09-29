using System;
using System.Windows.Forms;

namespace ChiisanaIroiro.ViewModel {
    public interface IMainFormViewModel : IViewModel {
        String SearchedFeature { get; set; }
        UserControl ActiveView { get; set; }
    }
}