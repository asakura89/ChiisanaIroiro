using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Ayumi.Data;
using Nvy;

namespace Ayumi.Desktop {
    public class DesktopDropdownList : ICommonList {
        readonly ListControl internalList;

        public DesktopDropdownList(ListControl listControl) {
            if (listControl == null)
                throw new ArgumentNullException(nameof(listControl));

            internalList = listControl;
        }

        public IEnumerable<NameValueItem> Items {
            get { return internalList.DataSource as List<NameValueItem> ?? new List<NameValueItem>(); }
            set { internalList.PopulateWithRawDataList(value.ToList(), NameValueItem.NameProperty, NameValueItem.ValueProperty); }
        }

        public Int32 SelectedIndex {
            get { return internalList.SelectedIndex; }
            set { internalList.SelectedIndex = value; }
        }

        public NameValueItem SelectedItem => this[internalList.SelectedIndex];

        public NameValueItem this[Int32 index] {
            get {
                var dataSource = internalList.DataSource as List<NameValueItem>;
                if (dataSource == null)
                    return null;

                return dataSource[index];
            }
            set {
                var dataSource = internalList.DataSource as List<NameValueItem> ?? new List<NameValueItem>();
                if (!dataSource.Any())
                    dataSource.Add(value);
                else
                    dataSource[index] = value;

                internalList.DataSource = dataSource;
            }
        }

        public void Add(NameValueItem nvi) {
            var dataSource = internalList.DataSource as List<NameValueItem> ?? new List<NameValueItem>();
            dataSource.Add(nvi);
            internalList.PopulateWithRawDataList(dataSource, NameValueItem.NameProperty, NameValueItem.ValueProperty);
        }

        public void AddRange(IEnumerable<NameValueItem> nviList) {
            var dataSource = internalList.DataSource as List<NameValueItem> ?? new List<NameValueItem>();
            dataSource.AddRange(nviList);
            internalList.PopulateWithRawDataList(dataSource, NameValueItem.NameProperty, NameValueItem.ValueProperty);
        }

        public void Clear() {
            internalList.DataSource = null;
        }
    }
}