using System;
using System.Globalization;

namespace ChiisanaIroiro.Service.Impl
{
    public class ChangeCaseService : IChangeCaseService
    {
        private readonly TextInfo currentTextInfo = CultureInfo.CurrentCulture.TextInfo;

        public String ToUpperCase(String normalText)
        {
            return currentTextInfo.ToUpper(normalText);
        }

        public String ToLowerCase(String normalText)
        {
            return currentTextInfo.ToLower(normalText);
        }

        public String ToTitleCase(String normalText)
        {
            return currentTextInfo.ToTitleCase(normalText);
        }
    }
}