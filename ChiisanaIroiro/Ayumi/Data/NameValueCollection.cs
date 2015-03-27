using System;
using System.Collections.Generic;
using System.Linq;

namespace ChiisanaIroiro.Ayumi.Data
{
    public class NameValueCollection : List<NameValueItem>
    {
        public const Char Separator = '•'; // ALT+7

        public override String ToString()
        {
            return String.Join(Separator.ToString(), this.Select(item => item.ToString()));
        }

        public static NameValueCollection FromList<T>(List<T> source, Func<T, String> nameSelector, Func<T, String> valueSelector)
        {
            var nviList = source.Select(item => NameValueItem.FromT(item, nameSelector, valueSelector));
            var nvCollection = new NameValueCollection();
            nvCollection.AddRange(nviList);

            return nvCollection;
        }
    }
}