using System;
using System.Collections.Generic;
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
            var rootElement = doc.Root;
            IEnumerable<XElement> xml = SortAppSettings(rootElement);

            var fileName = Path.GetFileName(filePath);
            var outputFolderName = GetOutputFolderName();

            SaveSortedAppSettings(rootElement.Name.LocalName,xml, string.Format(@"{0}\{1}", outputFolderName, fileName));
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

        private IEnumerable<XElement> SortAppSettings(XElement rootElement)
        {


            var elements = rootElement.Elements();
            foreach (var element in elements)
            {
                if (element.NodeType == XmlNodeType.Comment)
                {
                    element.Remove();
                   
                }

                if (!element.IsEmpty)
                {
                    var foo = SortAppSettings(element);
                    element.ReplaceAll(foo);
                }
            }


            return elements.OrderBy(s => s.Name.LocalName);

     
        }

        private void SaveSortedAppSettings(string root,IEnumerable<XElement> xml, string path)
        {
            var doc = new XDocument(new XElement(root, xml));
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