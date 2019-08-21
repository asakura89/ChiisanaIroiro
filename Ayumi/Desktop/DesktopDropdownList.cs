using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Ayumi.Data;
using Nvy;

namespace Ayumi.Desktop {
    public class DesktopDropdownList : ICommonList {
        readonly ListControl internalL;

        public DesktopDropdownList(ListControl controlL) {
            if (controlL == null)
                throw new ArgumentNullException(nameof(controlL));

            internalL = controlL;
        }

        public IEnumerable<NameValueItem> Items {
            get { return internalL.DataSource as List<NameValueItem> ?? new List<NameValueItem>(); }
            set {
                internalL.PopulateWithRawDataList(value.ToList(), NameValueItem.NameProperty, NameValueItem.ValueProperty);
            }
        }

        public Int32 SelectedIndex {
            get { return internalL.SelectedIndex; }
            set { internalL.SelectedIndex = value; }
        }

        public NameValueItem SelectedItem => this[internalL.SelectedIndex];

        public NameValueItem this[Int32 index] {
            get {
                var dataSource = internalL.DataSource as List<NameValueItem>;
                return dataSource?[index];
            }
            set {
                var dataSource = internalL.DataSource as List<NameValueItem> ?? new List<NameValueItem>();
                if (!dataSource.Any())
                    dataSource.Add(value);
                else
                    dataSource[index] = value;

                internalL.DataSource = dataSource;
            }
        }

        public void Add(NameValueItem nvi) {
            var dataSource = internalL.DataSource as List<NameValueItem> ?? new List<NameValueItem>();
            dataSource.Add(nvi);
            internalL.PopulateWithRawDataList(dataSource, NameValueItem.NameProperty, NameValueItem.ValueProperty);
        }

        public void AddRange(IEnumerable<NameValueItem> nviList) {
            var dataSource = internalL.DataSource as List<NameValueItem> ?? new List<NameValueItem>();
            dataSource.AddRange(nviList);
            internalL.PopulateWithRawDataList(dataSource, NameValueItem.NameProperty, NameValueItem.ValueProperty);
        }

        public void Clear() => internalL.DataSource = null;
    }
}