using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Eksmaru {
    internal static class XmlExt {
        internal static XmlDocument LoadFromPath(String xmlPath) {
            if (!File.Exists(xmlPath))
                throw new FileNotFoundException(xmlPath);

            String content = File.ReadAllText(xmlPath);
            XmlDocument xmlDoc = Load(content);

            return xmlDoc;
        }

        internal static XmlDocument Load(String xmlContent) {
            String content = xmlContent.Trim();
            if (String.IsNullOrEmpty(content))
                return null;

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);

            return xmlDoc;
        }

        internal static XmlAttribute GetAttribute(this XmlNode node, String name) {
            if (node != null && node.Attributes != null) {
                XmlAttribute attr = node.Attributes[name];
                if (attr != null)
                    return attr;
            }

            return null;
        }

        internal static void AssignAttributeTo(this XmlDocument xmlDoc, XmlNode node, String name, String value) {
            if (xmlDoc != null && node != null && node.Attributes != null) {
                XmlAttribute attr = xmlDoc.CreateAttribute(name);
                attr.Value = value;

                node.Attributes.Append(attr);
            }
        }

        internal static String GetAttributeValue(this XmlNode node, String name) {
            XmlAttribute attr = GetAttribute(node, name);
            if (attr != null)
                return attr.Value;

            return String.Empty;
        }

        internal static String GetNodeValue(XmlDocument xmlDoc, String selector) {
            XmlNode node = xmlDoc.SelectSingleNode(selector);
            return node.InnerText;
        }

        internal static IList<String> GetNodesValue(XmlDocument xmlDoc, String selector) {
            var values = new List<String>();
            XmlNodeList docs = xmlDoc.SelectNodes(selector);
            foreach (XmlNode doc in docs)
                values.Add(doc.InnerText);

            return values;
        }
    }
}