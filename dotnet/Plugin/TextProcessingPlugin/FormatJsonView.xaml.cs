using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.Json;
using Puru.Wpf;
using System.IO;

namespace TextProcessingPlugin {
    public partial class FormatJsonView : UserControl, IViewablePlugin {
        public String ComponentName => "Format JSON";

        public String ComponentDesc => "Beautify JSON String";

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        public FormatJsonView() {
            InitializeComponent();
            CommonView.ConfigButtonAccesssor.Visibility = Visibility.Collapsed;
            CommonView.ProcessButtonAccesssor.Click += ProcessButton_Click;
        }

        void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            String output = "";
            var jsonWriterOpt = new JsonWriterOptions {
                Encoder = null,
                Indented = true,
                SkipValidation = false
            };

            using (var jsonWriter = new Utf8JsonWriter(new MemoryStream(), jsonWriterOpt)) {
                JsonDocument
                    .Parse(CommonView.Input, new JsonDocumentOptions {
                        AllowTrailingCommas = true,
                        CommentHandling = JsonCommentHandling.Allow
                    })
                    .WriteTo(jsonWriter);

                output = jsonWriter.ToString();
            }

            CommonView.Output = output;
        }
    }
}
