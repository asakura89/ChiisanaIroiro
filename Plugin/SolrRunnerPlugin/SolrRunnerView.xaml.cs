using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json.Linq;
using Plugin.Dialog;
using Puru.Wpf;

namespace SolrRunnerPlugin {
    public partial class SolrRunnerView : UserControl, IViewablePlugin {
        public String ComponentName => "Solr Runner";

        public String ComponentDesc => "Runs Solr and its Zk ensemble";

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        public SolrRunnerView() {
            InitializeComponent();
            CommonView.HideAllButton();

            String configPath = Path.Combine(GetAssemblyDirectory(GetType().Assembly), "solr-runner.json");
            if (!String.IsNullOrEmpty(configPath))
                CommonView.Output = File.ReadAllText(configPath);
        }

        String GetAssemblyDirectory(Assembly assembly) {
            String codeBase = assembly.CodeBase;
            var uri = new UriBuilder(codeBase);
            String path = Uri.UnescapeDataString(uri.Path);
            return Path.Combine(Path.GetDirectoryName(path), "Plugins", GetType().Namespace);
        }

        void RunButton_Click(Object sender, RoutedEventArgs e) {
            if (!String.IsNullOrEmpty(CommonView.Output)) {
                Task.Factory
                    .StartNew(() => {
                        try {
                            var config = JObject.Parse(CommonView.Output);
                            JToken defaultProfile = config
                                .SelectTokens("SolrRunner.Profile[?(@.Default == true)]")
                                .FirstOrDefault();

                            if (defaultProfile != null) {
                                IEnumerable<(String Name, String Path)> zks = defaultProfile
                                    .SelectTokens("Zookeeper")
                                    .Select(zk => (Name: Convert.ToString(zk.SelectToken("Name")), Path: Convert.ToString(zk.SelectToken("Path"))))
                                    .ToList();

                                Console.WriteLine();
                            }
                        }
                        catch (Exception ex) {
                            ex.ShowInTaskDialog();
                        }
                    }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
    }
}
