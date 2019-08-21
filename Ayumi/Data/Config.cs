using System;
using System.IO;

namespace Common {
    public static class Config {
        public static String DirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");
    }
}
