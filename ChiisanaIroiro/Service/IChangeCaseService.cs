using System;

namespace ChiisanaIroiro.Service
{
    public interface IChangeCaseService
    {
        String ToUpperCase(String normalText);
        String ToLowerCase(String normalText);
        String ToTitleCase(String normalText);
    }
}