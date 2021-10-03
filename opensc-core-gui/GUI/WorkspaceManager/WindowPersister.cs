using OpenSC.GUI.WorkspaceManager.ValueConverters;
using OpenSC.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.GUI.WorkspaceManager
{
    public class WindowPersister
    {

        private static readonly string DIRECTORY_WORKSPACE = $"{DataPathInfo.PATH_DATA}workspace{Path.DirectorySeparatorChar}";
        private static readonly string FILE_WORKSPACE_SAVED = $"{DIRECTORY_WORKSPACE}saved.{EXTENSION_WORKSPACE}";
        private static readonly string EXTENSION_WORKSPACE = "xml";

        private const string ROOT_TAG = "workspace";
        private const string WINDOW_TAG = "window";
        private const string ATTRIBUTE_TAG = "attribute";

        private const string ROOT_ATTRIBUTE_ACTIVE = "active";
        private const string ROOT_ATTRIBUTE_MAIN_WIDTH = "mwidth";
        private const string ROOT_ATTRIBUTE_MAIN_HEIGHT = "mheight";
        private const string ROOT_ATTRIBUTE_MAIN_LEFT = "mleft";
        private const string ROOT_ATTRIBUTE_MAIN_TOP = "mtop";
        private const string ROOT_ATTRIBUTE_MAIN_MAXIMIZED = "mmaximized";

        private const string WINDOW_ATTRIBUTE_TYPE = "type";
        private const string WINDOW_ATTRIBUTE_WIDTH = "width";
        private const string WINDOW_ATTRIBUTE_HEIGHT = "height";
        private const string WINDOW_ATTRIBUTE_LEFT = "left";
        private const string WINDOW_ATTRIBUTE_TOP = "top";

        private const string ATTRIBUTE_ATTRIBUTE_KEY = "key";
        private const string ATTRIBUTE_ATTRIBUTE_VALUE = "value";
        private const string ATTRIBUTE_ATTRIBUTE_TYPE = "type";

        private static readonly XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
        {
            CloseOutput = false,
            Indent = true,
            IndentChars = "\t"
        };

        public static void SaveWindows(Workspace workspace)
        {

            XElement rootElement = new XElement(ROOT_TAG);

            rootElement.SetAttributeValue(ROOT_ATTRIBUTE_ACTIVE, workspace.ActiveWindowIndex);

            rootElement.SetAttributeValue(ROOT_ATTRIBUTE_MAIN_WIDTH, workspace.MainSize.Width);
            rootElement.SetAttributeValue(ROOT_ATTRIBUTE_MAIN_HEIGHT, workspace.MainSize.Height);
            rootElement.SetAttributeValue(ROOT_ATTRIBUTE_MAIN_LEFT, workspace.MainPosition.X);
            rootElement.SetAttributeValue(ROOT_ATTRIBUTE_MAIN_TOP, workspace.MainPosition.Y);
            rootElement.SetAttributeValue(ROOT_ATTRIBUTE_MAIN_MAXIMIZED, workspace.MainMaximized.ToString());

            foreach (IPersistableWindow window in workspace.Windows)
            {
                XElement windowElement = serializeWindow(window);
                if (windowElement != null)
                    rootElement.Add(windowElement);
            }

            using (FileStream stream = new FileStream(FILE_WORKSPACE_SAVED, FileMode.Create))
            using (XmlWriter writer = XmlWriter.Create(stream, xmlWriterSettings))
            {
                rootElement.WriteTo(writer);
            }

        }

        public static Workspace LoadWindows()
        {

            Workspace workspace = new Workspace();

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(FILE_WORKSPACE_SAVED);

                XmlNode root = doc.DocumentElement;
                if (root.LocalName != ROOT_TAG)
                    return null;

                workspace.ActiveWindowIndex = int.TryParse(root.Attributes[ROOT_ATTRIBUTE_ACTIVE]?.Value, out int fwi) ? (int?)fwi : null;

                if (!int.TryParse(root.Attributes[ROOT_ATTRIBUTE_MAIN_LEFT]?.Value, out int x))
                    x = 0;
                if (!int.TryParse(root.Attributes[ROOT_ATTRIBUTE_MAIN_TOP]?.Value, out int y))
                    y = 0;
                workspace.MainPosition = new Point(x, y);

                if (!int.TryParse(root.Attributes[ROOT_ATTRIBUTE_MAIN_WIDTH]?.Value, out int w))
                    w = 1024;
                if (!int.TryParse(root.Attributes[ROOT_ATTRIBUTE_MAIN_HEIGHT]?.Value, out int h))
                    h = 768;
                workspace.MainSize = new Size(w, h);

                if (!bool.TryParse(root.Attributes[ROOT_ATTRIBUTE_MAIN_MAXIMIZED]?.Value, out workspace.MainMaximized))
                    workspace.MainMaximized = true;

                foreach (XmlNode node in root.ChildNodes)
                {
                    IPersistableWindow window = deserializeWindow(node);
                    if (window != null)
                        workspace.Windows.Add(window);
                }

            }
            catch
            {
                return null;
            }

            return workspace;

        }

        private static XElement serializeWindow(IPersistableWindow window)
        {

            XElement xmlElement = new XElement(WINDOW_TAG);
            Type windowType = window.GetType();

            string windowTypeName = WindowTypeRegister.GetTypeNameForWindow(window);
            if (windowTypeName == null)
                return null;

            xmlElement.SetAttributeValue(WINDOW_ATTRIBUTE_TYPE, windowTypeName);

            Point pos = window.Position;
            xmlElement.SetAttributeValue(WINDOW_ATTRIBUTE_LEFT, pos.X);
            xmlElement.SetAttributeValue(WINDOW_ATTRIBUTE_TOP, pos.Y);

            Size size = window.Size;
            xmlElement.SetAttributeValue(WINDOW_ATTRIBUTE_WIDTH, size.Width);
            xmlElement.SetAttributeValue(WINDOW_ATTRIBUTE_HEIGHT, size.Height);

            foreach (var pair in window.GetKeyValuePairs())
            {
                XElement attributeElement = new XElement(ATTRIBUTE_TAG);
                attributeElement.SetAttributeValue(ATTRIBUTE_ATTRIBUTE_KEY, pair.Key);
                ValueConverter.SerializedData serialized = ValueConverter.Serialize(pair.Value);
                attributeElement.SetAttributeValue(ATTRIBUTE_ATTRIBUTE_VALUE, serialized.Value);
                if (serialized.TypeName != string.Empty)
                    attributeElement.SetAttributeValue(ATTRIBUTE_ATTRIBUTE_TYPE, serialized.TypeName);
                xmlElement.Add(attributeElement);
            }

            return xmlElement;

        }

        private static IPersistableWindow deserializeWindow(XmlNode node)
        {

            try
            {

                string windowTypeName = node.Attributes[WINDOW_ATTRIBUTE_TYPE].Value;
                Type windowType = WindowTypeRegister.GetTypeForTypeName(windowTypeName);
                if (windowType == null)
                    return null;

                if (!int.TryParse(node.Attributes[WINDOW_ATTRIBUTE_LEFT].Value, out int x))
                    x = 0;
                if (!int.TryParse(node.Attributes[WINDOW_ATTRIBUTE_TOP].Value, out int y))
                    y = 0;
                Point position = new Point(x, y);

                if (!int.TryParse(node.Attributes[WINDOW_ATTRIBUTE_WIDTH].Value, out int w))
                    w = 400;
                if (!int.TryParse(node.Attributes[WINDOW_ATTRIBUTE_HEIGHT].Value, out int h))
                    h = 300;
                Size size = new Size(w, h);

                Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
                foreach (XmlNode attributeNode in node.ChildNodes)
                {
                    if (attributeNode.LocalName == ATTRIBUTE_TAG)
                    {
                        string key = attributeNode.Attributes[ATTRIBUTE_ATTRIBUTE_KEY].Value;
                        string value = attributeNode.Attributes[ATTRIBUTE_ATTRIBUTE_VALUE].Value;
                        string type = attributeNode.Attributes[ATTRIBUTE_ATTRIBUTE_TYPE]?.Value;
                        if (!string.IsNullOrWhiteSpace(key))
                        {
                            object deserializedValue = value;
                            if (!string.IsNullOrEmpty(type))
                                deserializedValue = ValueConverter.Deserialize(value, type);
                            keyValuePairs.Add(key, deserializedValue);
                        }
                    }
                }

                ConstructorInfo foundCtor = null;
                foreach (var ctor in windowType.GetConstructors())
                {
                    if (ctor.GetParameters().Length == 0)
                    {
                        foundCtor = ctor;
                        break;
                    }
                }

                if (foundCtor == null)
                    return null;

                IPersistableWindow window = foundCtor.Invoke(new object[] { }) as IPersistableWindow;
                if (window == null)
                    return null;

                window.RestoreData(position, size, keyValuePairs);

                return window;

            }
            catch
            {
                return null;
            }

        }

        public class Workspace
        {
            public List<IPersistableWindow> Windows = new List<IPersistableWindow>();
            public int? ActiveWindowIndex = null;
            public Size MainSize;
            public Point MainPosition;
            public bool MainMaximized;
        }

    }

}
