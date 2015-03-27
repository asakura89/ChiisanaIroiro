using ChiisanaIroiro.Ayumi.Core;

namespace ChiisanaIroiro.Presenter
{
    public interface IChangeCasePresenter : IExceptionCapture, IActionCapture
    {
        void Initialize();
        void ChangeCaseAction();
    }
}