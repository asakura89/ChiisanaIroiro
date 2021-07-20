using System;

namespace GenerateRandomPlugin {
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DataGeneratorAttribute : Attribute {
        public String Keyword { get; }
        public DataGeneratorAttribute(String keyword) {
            Keyword = keyword;
        }
    }
}
