using System;

namespace ChiisanaIroiro.Ayumi.Core
{
    public interface IExceptionCapture
    {
        void OnException(Exception ex);
    }
}