using System;

namespace ChiisanaIroiro.Ayumi.Logger
{
    public interface ILogService
    {
        void Debug(Object message);
        void Debug(Object message, Exception exception);
        void Info(Object message);
        void Info(Object message, Exception exception);
        void Error(Object message);
        void Error(Object message, Exception exception);
    }
}