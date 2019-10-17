using System;
using System.Linq;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DefaultPlugin {
    public static class DbgExt {
        public static void Dbg(this Object obj) => obj.Dbg(String.Empty);

        public static void Dbg(this Object obj, String title) {
            if (!String.IsNullOrEmpty(title)) {
                Debug.WriteLine(title);
                Debug.WriteLine(
                    String.Join(
                        String.Empty,
                        Enumerable.Repeat("-", title.Length)
                    ));
            }

            Debug.WriteLine(JsonConvert.SerializeObject(obj, Formatting.Indented));
            Debug.WriteLine(String.Empty);
        }
    }
}
