using Ayumi.Logger;

namespace ChiisanaIroiro.Presenter {
    public interface IObjectCreatePresenter : IExceptionCapture, IActionCapture {
        void Initialize();
        void CreateObjectAction();
    }
}