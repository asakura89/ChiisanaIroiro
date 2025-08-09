using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ChiisanaIroiro.Service.Impl {
    public class StringUtilService : IStringUtilService {
        public String SortStringList(String normalText) {
            IList<String> list = normalText
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                .OrderBy(item => item)
                .ToList();

            return String.Join(Environment.NewLine, list);
        }
    }
}