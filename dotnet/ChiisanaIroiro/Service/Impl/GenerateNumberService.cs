using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChiisanaIroiro.Service.Impl {
    public class GenerateNumberService : IGenerateNumberService {
        public String GenerateProcessId(String configJson) {
            return Generate(configJson, GenerateDailyRunningNumber);
        }

        public String GenerateRandomNumber(String configJson) {
            return Generate(configJson, GenerateRandomNo);
        }

        public String GenerateRandomHexNumber(String configJson) {
            return Generate(configJson, GenerateRandomHexNo);
        }

        public String GenerateRandomGuid(String configJson) {
            return Generate(configJson, GenerateRandomGuidString);
        }

        String Generate(String configJson, Func<GenerateNumberConfig, IEnumerable<String>> generator) {
            var config = Newtonsoft.Json.JsonConvert.DeserializeObject<GenerateNumberConfig>(configJson);
            IEnumerable<String> running = generator(config)
                .Select(outp => outp.Length > config.Length ?
                    outp.Substring(0, config.Length) :
                    config.Pad.ToLowerInvariant() == "left" ?
                        outp.PadLeft(config.Length, '0') :
                        config.Pad.ToLowerInvariant() == "right" ?
                            outp.PadRight(config.Length, '0') :
                            outp);

            IEnumerable<String> generated = running;
            if (config.Base64)
                generated = generated.Concat(GenerateBase64(running));
            if (config.Base64Url)
                generated = generated.Concat(GenerateBase64Url(running));

            return String.Join("\r\n", generated);
        }

        IEnumerable<String> GenerateRandomGuidString(GenerateNumberConfig config) {
            return Enumerable
                .Range(1, config.Iterate)
                .Select(n => Guid.NewGuid().ToString("N"));
        }

        IEnumerable<String> GenerateRandomHexNo(GenerateNumberConfig config) {
            return Enumerable
                .Range(1, config.Iterate)
                .Select(no =>
                    String.Join(String.Empty, Enumerable
                        .Range(1, config.Length)
                        .Select(ino => Convert
                            .ToInt64(Keywielder
                                .Wielder
                                .New()
                                .AddRandomNumber(2)
                                .BuildKey())
                            .ToString("x2")
                        )
                    )
                );
        }

        IEnumerable<String> GenerateRandomNo(GenerateNumberConfig config) {
            return Enumerable
                .Range(1, config.Iterate)
                .Select(n => Keywielder
                    .Wielder
                    .New()
                    .AddRandomNumber(config.Length)
                    .BuildKey());
        }

        IEnumerable<String> GenerateDailyRunningNumber(GenerateNumberConfig config) {
            Int32 end = config.Start + config.Iterate;
            while (config.Start < end) {
                yield return Keywielder
                    .Wielder
                    .New()
                    .AddLongYear()
                    .AddNumericMonth()
                    .AddDate()
                    .AddLeftPadded(iwielder => iwielder.AddCounter(config.Start++, 0), 4, '0')
                    .BuildKey();
            }
        }

        IEnumerable<String> GenerateBase64(IEnumerable<String> inputList) {
            return inputList
                .Select(inp => Convert
                    .ToBase64String(Encoding.UTF8.GetBytes(inp)));
        }

        IEnumerable<String> GenerateBase64Url(IEnumerable<String> inputList) {
            return GenerateBase64(inputList)
                .Select(inp => inp
                    .TrimEnd('=')
                    .Replace("+", "-")
                    .Replace("/", "_"));
        }
    }
}