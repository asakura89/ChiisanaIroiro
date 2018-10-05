using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ayumi.Core;
using Newtonsoft.Json;

namespace ChiisanaIroiro.Service.Impl {
    public class ObjectCreateService : IObjectCreateService {
        readonly IGenerateTemplateService templateService;

        public ObjectCreateService() {
            templateService = ObjectRegistry.GetRegisteredObject<IGenerateTemplateService>();
        }

        public String CreateObject(String input) {
            var sb = new StringBuilder();
            var inputList = JsonConvert.DeserializeObject<IEnumerable<ObjectMetadata>>(input);
            foreach (ObjectMetadata meta in inputList) {
                sb.AppendLine($"public sealed class {meta.Name}ViewModel {{");
                foreach (String prop in meta.Properties) {
                    String[] splitted = prop.Split(':').Select(str => str.Trim()).ToArray();
                    if (splitted.Length < 2)
                        throw new InvalidOperationException("Invalid format.");

                    String type = GetPropertyType(splitted[1]);
                    sb.AppendLine($"    public {type} {splitted[0]} {{ get; set; }}");
                }
                sb.AppendLine("}")
                    .AppendLine();
            }

            return sb.ToString();
        }

        public String CreateNotifiedObject(String input) {
            var sb = new StringBuilder();
            var inputList = JsonConvert.DeserializeObject<IEnumerable<ObjectMetadata>>(input);
            sb.AppendLine(templateService.GenerateTemplate("template.type.notified"))
                .AppendLine();
            foreach (ObjectMetadata meta in inputList) {
                sb.AppendLine($"public sealed class {meta.Name}ViewModel : Notified {{");
                foreach (String prop in meta.Properties) {
                    String[] splitted = prop.Split(':').Select(str => str.Trim()).ToArray();
                    if (splitted.Length < 2)
                        throw new InvalidOperationException("Invalid format.");

                    String type = GetPropertyType(splitted[1]);
                    sb.AppendLine($"    {type} {splitted[0].ToLowerInvariant()};")
                        .AppendLine($"    public {type} {splitted[0]} {{")
                        .AppendLine($"        get {{return {splitted[0].ToLowerInvariant()};}}")
                        .AppendLine("        set {")
                        .AppendLine($"            {splitted[0].ToLowerInvariant()} = value;")
                        .AppendLine($"            RaisePropertyChanged(nameof({splitted[0]}));")
                        .AppendLine("        }")
                        .AppendLine();
                }
                sb.AppendLine("}")
                    .AppendLine();
            }

            return sb.ToString();
        }

        String GetPropertyType(String type) {
            switch (type) {
                case "str": return "String";
                case "int": return "Int32";
                case "lint": return "Int64";
                case "boo": return "Boolean";
                case "dec": return "Decimal";
                case "sin": return "Single";
                case "dou": return "Double";
                default: return type;
            }
        }
    }
}