using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace ConfigCleaner
{
    public class Processor
    {
        public void CleanFile(string filePath)
        {
            var doc = LoadDocument(filePath);
            var rootElement = doc.Element("appSettings");
            IOrderedEnumerable<XElement> xml = SortAppSettings(rootElement);

            var fileName = Path.GetFileName(filePath);
            var outputFolderName = GetOutputFolderName();

            SaveSortedAppSettings(xml, string.Format(@"{0}\{1}", outputFolderName,fileName));
        }

        private static string GetOutputFolderName()
        {
            var outputFolderName = ConfigurationManager.AppSettings["OutputPath"];
            if (!Directory.Exists(outputFolderName))
            {
                Directory.CreateDirectory(outputFolderName);
            }
            return outputFolderName;
        }

        private XDocument LoadDocument(string path)
        {
            return XDocument.Load(path);
        }

        private IOrderedEnumerable<XElement> SortAppSettings(XElement rootElement)
        {
            return rootElement
              .Elements("add")
              .Where(s => s.NodeType != XmlNodeType.Comment)
              .OrderBy(s => s.Attribute("key").Value);
        }

        private void SaveSortedAppSettings(IOrderedEnumerable<XElement> xml,string path)
        {
            var doc = new XDocument(new XElement("appSettings", xml));
            doc.Save(path);
        }


        public void CleanFiles(string configs)
        {
            var files = Directory.GetFiles(configs);
            foreach (var file in files)
            {
                Console.Out.WriteLine("Processing: " + file);
                CleanFile(file);
            }
        }
    }
}