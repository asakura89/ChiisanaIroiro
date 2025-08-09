using System;

namespace ChiisanaIroiro.Service.Impl {
    public sealed class GenerateNumberConfig {
        public Int32 Start { get; set; }
        public Int32 Length { get; set; }
        public Int32 Iterate { get; set; }
        public Boolean Base64 { get; set; }
        public Boolean Base64Url { get; set; }
        public String Pad { get; set; }
    }
}