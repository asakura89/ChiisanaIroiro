using System;
using System.Collections.Generic;

namespace ChiisanaIroiro.Utility {
    public interface ISessionStore {
        Object Get(String key);
        void Add(String key, Object value);
        void Remove(String key);
        void Clear();
        IEnumerable<String> GetKeys();
    }
}