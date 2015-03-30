using Ayumi.Logger;

namespace ChiisanaIroiro.Presenter
{
    public interface IMakeHeaderPresenter : IExceptionCapture, IActionCapture
    {
        void MakeHeaderAction();
    }
}