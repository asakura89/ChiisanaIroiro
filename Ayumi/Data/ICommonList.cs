using System;
using System.Collections.Generic;

namespace Ayumi.Data
{
    public interface ICommonList
    {
        Int32 SelectedIndex { get; set; }
        NameValueItem SelectedItem { get; set; }
        NameValueItem this[Int32 index] { get; set; }
        void Add(NameValueItem nvi);
        void AddRange(IEnumerable<NameValueItem> nviList);
        void Clear();
    }
}