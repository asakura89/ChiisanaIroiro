using Ayumi.Logger;

namespace ChiisanaIroiro.Presenter {
    public interface IGenerateTemplatePresenter : IExceptionCapture, IActionCapture {
        void Initialize();
        void GenerateAction();
    }
}