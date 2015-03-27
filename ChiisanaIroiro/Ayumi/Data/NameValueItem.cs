using System;

namespace ChiisanaIroiro.Ayumi.Data
{
    public class NameValueItem
    {
        public const String NameProperty = "Name";
        public const String ValueProperty = "Value";
        public const Char Separator = '·'; // ALT+250

        public String Name { get; set; }
        public String Value { get; set; }

        public NameValueItem(String name, String value)
        {
            Name = name;
            Value = value;
        }

        public NameValueItem() : this(String.Empty, String.Empty) { }

        public override String ToString()
        {
            return Name + Separator + Value;
        }

        public static NameValueItem FromT<T>(T item, Func<T, String> nameSelector, Func<T, String> valueSelector)
        {
            return new NameValueItem(nameSelector(item), valueSelector(item));
        }
    }
}