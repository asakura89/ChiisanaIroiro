using System;
using System.Collections.Generic;

namespace ChiisanaIroiro.Utility {
    public static class SessionStore {
        static readonly Dictionary<String, Object> InternalStorage = new Dictionary<String, Object>();

        public static Object Get(String key) {
            if (InternalStorage.ContainsKey(key))
                return InternalStorage[key];

            return null;
        }

        public static void Add(String key, Object value) {
            if (InternalStorage.ContainsKey(key))
                InternalStorage[key] = value;
            else
                InternalStorage.Add(key, value);
        }

        public static void Remove(String key) {
            if (InternalStorage.ContainsKey(key))
                InternalStorage.Remove(key);
        }

        public static void Clear() {
            InternalStorage.Clear();
        }

        public static IEnumerable<String> GetKeys() {
            return InternalStorage.Keys;
        }
    }
}