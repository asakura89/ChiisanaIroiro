using System;
using System.IO;
using System.Reflection;

namespace ChiisanaIroiro.Service.Impl {
    public class GenerateSqlTemplateService : IGenerateSqlTemplateService {
        readonly Assembly asm = Assembly.GetExecutingAssembly();

        public String GenerateActionTemplate() {
            return GetEmbeddedTemplate("ChiisanaIroiro.Embedded.ActionSqlTemplate.eqx");
        }

        public String GenerateRetrieveTemplate() {
            return GetEmbeddedTemplate("ChiisanaIroiro.Embedded.RetrieveSqlTemplate.eqx");
        }

        String GetEmbeddedTemplate(String filename) {
            using (Stream stream = asm.GetManifestResourceStream(filename))
                using (StreamReader reader = new StreamReader(stream))
                    return reader.ReadToEnd();
        }
    }
}