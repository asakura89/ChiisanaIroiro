using Ayumi.Logger;

namespace ChiisanaIroiro.Presenter {
    public interface IGenerateSqlTemplatePresenter : IExceptionCapture, IActionCapture {
        void Initialize();
        void GenerateAction();
    }
}