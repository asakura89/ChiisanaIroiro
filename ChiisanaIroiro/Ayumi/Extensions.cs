using System;
using System.Collections.Generic;
using System.Linq;
using ChiisanaIroiro.Ayumi.Data;

namespace ChiisanaIroiro.Ayumi
{
    public static class Extensions
    {
        public static void BindToICommonList<T>(this ICommonList commonList, IEnumerable<T> dataList, Func<T, String> nameSelector, Func<T, String> valueSelector) where T : class
        {
            BindToICommonList(commonList, dataList, nameSelector, valueSelector, false);
        }
        public static void BindToICommonList<T>(this ICommonList commonList, IEnumerable<T> dataList, Func<T, String> nameSelector, Func<T, String> valueSelector, Boolean isOrdered) where T : class
        {
            IList<NameValueItem> nviList = dataList
                .Select(item => new NameValueItem(nameSelector(item), valueSelector(item)))
                .ToList();
            nviList.Insert(0, new NameValueItem(String.Empty, String.Empty));

            RawBindToICommonList(commonList, nviList, isOrdered);
        }

        public static void BindToICommonList(this ICommonList commonList, IEnumerable<NameValueItem> dataList)
        {
            BindToICommonList(commonList, dataList, false);
        }
        public static void BindToICommonList(this ICommonList commonList, IEnumerable<NameValueItem> dataList, Boolean isOrdered)
        {
            IList<NameValueItem> nviList = dataList as IList<NameValueItem> ?? dataList.ToList();
            nviList.Insert(0, new NameValueItem(String.Empty, String.Empty));

            RawBindToICommonList(commonList, nviList, isOrdered);
        }

        public static void RawBindToICommonList<T>(this ICommonList commonList, IEnumerable<T> dataList, Func<T, String> nameSelector, Func<T, String> valueSelector) where T : class
        {
            RawBindToICommonList(commonList, dataList, nameSelector, valueSelector, false);
        }
        public static void RawBindToICommonList<T>(this ICommonList commonList, IEnumerable<T> dataList, Func<T, String> nameSelector, Func<T, String> valueSelector, Boolean isOrdered) where T : class
        {
            commonList.Clear();

            if (isOrdered)
            {
                IEnumerable<NameValueItem> orderedList = dataList
                    .Select(item => new NameValueItem(nameSelector(item), valueSelector(item)))
                    .OrderBy(nvi => nvi.Name);
                commonList.AddRange(orderedList);
            }
            else
            {
                foreach (T item in dataList)
                    commonList.Add(new NameValueItem(nameSelector(item), valueSelector(item)));
            }
        }

        public static void RawBindToICommonList(this ICommonList commonList, IEnumerable<NameValueItem> dataList)
        {
            RawBindToICommonList(commonList, dataList, false);
        }
        public static void RawBindToICommonList(this ICommonList commonList, IEnumerable<NameValueItem> dataList, Boolean isOrdered)
        {
            commonList.Clear();
            commonList.AddRange(isOrdered ? dataList.OrderBy(nvi => nvi.Name) : dataList);
        }
    }
}