using System;
using System.Collections.Generic;
using Nvy;

namespace Ayumi.Data {
    public interface ICommonList {
        IEnumerable<NameValueItem> Items { get; set; }
        Int32 SelectedIndex { get; set; }
        NameValueItem SelectedItem { get; }
        NameValueItem this[Int32 index] { get; set; }
        void Add(NameValueItem nvi);
        void AddRange(IEnumerable<NameValueItem> nviList);
        void Clear();
    }
}