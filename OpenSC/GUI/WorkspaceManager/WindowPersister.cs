using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.GUI.WorkspaceManager
{
    class WindowPersister
    {

        private static readonly XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
        {
            CloseOutput = false,
            Indent = true,
            IndentChars = "\t"
        };

        public static void SaveWindows(List<IPersistableWindow> windows)
        {

            XElement rootElement = new XElement("workspace");
            foreach (IPersistableWindow window in windows)
            {
                XElement windowElement = serializeWindow(window);
                if(windowElement != null)
                    rootElement.Add(windowElement);
            }

            using (FileStream stream = new FileStream("workspace.xml", FileMode.Create))
            using (XmlWriter writer = XmlWriter.Create(stream, xmlWriterSettings))
            {
                rootElement.WriteTo(writer);
            }

        }

        private static XElement serializeWindow(IPersistableWindow window)
        {

            XElement xmlElement = new XElement("window");
            Type windowType = window.GetType();

            string windowTypeName = WindowTypeRegister.GetTypeNameForWindow(window);
            if (windowTypeName == null)
                return null;

            xmlElement.SetAttributeValue("type", windowTypeName);

            Point pos = window.Position;
            xmlElement.SetAttributeValue("x", pos.X);
            xmlElement.SetAttributeValue("y", pos.Y);

            Size size = window.Size;
            xmlElement.SetAttributeValue("w", size.Width);
            xmlElement.SetAttributeValue("h", size.Height);

            foreach(var pair in window.GetKeyValuePairs())
            {
                XElement attributeElement = new XElement("attribute");
                attributeElement.SetAttributeValue("key", pair.Key);
                attributeElement.SetAttributeValue("value", pair.Value);
                xmlElement.Add(attributeElement);
            }

            return xmlElement;

        }

    }
}
