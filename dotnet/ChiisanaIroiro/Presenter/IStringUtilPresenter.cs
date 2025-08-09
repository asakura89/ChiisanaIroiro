using Ayumi.Logger;

namespace ChiisanaIroiro.Presenter {
    public interface IStringUtilPresenter : IExceptionCapture, IActionCapture {
        void Initialize();
        void StringUtilAction();
    }
}