using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Envision.Common
{
    public static class cls_EnvisionConfig
    {
        /// <summary>
        /// Save Details to XML
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="updateText"></param>
        public static void SaveDetailsToXML(string applicationPath, string tagName, string updateText)
        {
            string xmlPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "common.cfg");
            XmlDocument configDoc = new XmlDocument();
            configDoc.Load(xmlPath);

            XmlNode userNode = configDoc.SelectSingleNode(tagName);
            if (userNode != null)
            {
                userNode.InnerText = updateText;
            }
            configDoc.Save(xmlPath);
        }

        public static string ReadDetailsFromXML(string applicationPath, string tagName)
        {
            string returnVal = string.Empty;
            string xmlPath = Path.Combine(applicationPath, "common.cfg");
            XmlDocument configDoc = new XmlDocument();
            configDoc.Load(xmlPath);
            XmlNode sRetNode = configDoc.SelectSingleNode(tagName);
            if (sRetNode != null)
            {
                returnVal = sRetNode.InnerText;
            }

            return returnVal;
        }
    }
}
