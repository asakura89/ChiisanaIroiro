using System;
using System.Collections.Generic;
using System.Linq;
using Nvy;

namespace Ayumi.Data {
    public class InMemoryCommonList : ICommonList {
        IList<NameValueItem> nvies = new List<NameValueItem>();

        public IEnumerable<NameValueItem> Items {
            get { return nvies; }
            set { nvies = value.ToList(); }
        }

        public Int32 SelectedIndex { get; set; }
        public NameValueItem SelectedItem => nvies[SelectedIndex];

        public NameValueItem this[Int32 index] {
            get { return nvies[index]; }
            set { nvies[index] = value; }
        }

        public void Add(NameValueItem nvi) {
            nvies.Add(nvi);
        }

        public void AddRange(IEnumerable<NameValueItem> nviList) {
            foreach (NameValueItem nvy in nviList)
                nvies.Add(nvy);
        }

        public void Clear() {
            nvies.Clear();
        }
    }
}