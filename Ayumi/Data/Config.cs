using System;
using System.IO;

namespace Ayumi.Data {
    public static class Config {
        public static String DirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");
    }
}
