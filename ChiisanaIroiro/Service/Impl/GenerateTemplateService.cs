using System;
using System.IO;
using System.Reflection;

namespace ChiisanaIroiro.Service.Impl {
    public class GenerateTemplateService : IGenerateTemplateService {
        readonly Assembly asm = Assembly.GetExecutingAssembly();

        public String GenerateTemplate(String type) => GetEmbeddedTemplate($"ChiisanaIroiro.Embedded.{type}");

        String GetEmbeddedTemplate(String filename) {
            using (Stream stream = asm.GetManifestResourceStream(filename))
            using (StreamReader reader = new StreamReader(stream)) {
                return reader.ReadToEnd();
            }
        }
    }
}