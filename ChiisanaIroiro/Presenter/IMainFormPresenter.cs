using Ayumi.Logger;

namespace ChiisanaIroiro.Presenter {
    public interface IMainFormPresenter : IExceptionCapture, IActionCapture {
        void Initialize();
        void FeatureSelectedAction();
        void FeatureSearchedAction();
    }
}