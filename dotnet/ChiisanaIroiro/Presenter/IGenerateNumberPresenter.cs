using Ayumi.Logger;

namespace ChiisanaIroiro.Presenter {
    public interface IGenerateNumberPresenter : IExceptionCapture, IActionCapture {
        void Initialize();
        void GenerateAction();
    }
}