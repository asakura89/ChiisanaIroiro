using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ayumi.Plugin;

namespace DefaultPlugin {
    public class MergeFiles : IPlugin {
        public String Name => "Merge Files";

        public String Desc => "Merge multiple files";

        public Object Process(Object processArgs) {

            /*String input = Convert.ToString(processArgs);
            IEnumerable<String> paths = input
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .SelectMany(GetFiles);

            String text = String.Empty;
            using (var sw = new StreamWriter(new FileStream(Target, FileMode.Create, FileAccess.ReadWrite), Encoding.UTF8)) {
                for (Int32 idx = 0; idx < Source.Length-1; idx++) {
                    using (var sr = new StreamReader(Source[idx], Encoding.UTF8)) {
                        text = sr.ReadToEnd();
                        sw.WriteLine(text);
                    }
                }
            }*/

            throw new NotImplementedException();
        }

        IEnumerable<String> GetFiles(String path) =>
            File.GetAttributes(path).HasFlag(FileAttributes.Directory) ?
                Directory.GetFiles(path) :
                (new[] { path });
    }
}
