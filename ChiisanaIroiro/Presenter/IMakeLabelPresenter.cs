using Ayumi.Logger;

namespace ChiisanaIroiro.Presenter {
    public interface IMakeLabelPresenter : IExceptionCapture, IActionCapture {
        void Initialize();
        void MakeLabelAction();
    }
}