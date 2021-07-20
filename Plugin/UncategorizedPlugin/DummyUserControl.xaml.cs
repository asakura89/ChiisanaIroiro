using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using Eksmaru;

namespace UncategorizedPlugin {
    public partial class DummyUserControl : UserControl {
        IList<UIElement> controls = new List<UIElement>();

        public DummyUserControl() {
            InitializeComponent();
            InitializeInternalComponent();
        }

        void InitializeInternalComponent() {
            
        }

        dynamic XmlToConfig(String configPath) {
            var config = XmlExt.LoadFromPath(configPath);
            IList<XmlNode> appChildren = config
                .SelectSingleNode("configuration/app")
                .ChildNodes
                .Cast<XmlNode>()
                .ToList();

            foreach (XmlNode node in appChildren) {
                String control = node.Name.Split(':')[0];

            }

            return new {
                App = "",
                Data = ""
            };
        }

        IDictionary<String, Func<XmlNode, XFControl>> ControlCreators = new Dictionary<XmlNode, XFControl> {

        };

        static XFControl CreateXFTextControl(XmlNode node) {
            
        }

        static XFControl CreateXFMultilineText(XmlNode node) {
            
        }
    }
}
