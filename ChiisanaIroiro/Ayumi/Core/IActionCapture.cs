using System;

namespace ChiisanaIroiro.Ayumi.Core
{
    public interface IActionCapture
    {
        void OnAction(String action, String actionDesc);
    }
}