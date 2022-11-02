using System;
using System.Drawing;
using System.Xml;

namespace OpenSC.Model.Persistence
{

    class ColorXmlSerializer : IInstantiatingXmlSerializer
    {

        public Type Type => typeof(Color);

        private const string ATTRIBUTE_ALPHA = "a";
        private const string ATTRIBUTE_RED = "r";
        private const string ATTRIBUTE_GREEN = "g";
        private const string ATTRIBUTE_BLUE = "b";

        public object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys)
        {
            byte a, r, g, b;
            if (!byte.TryParse(serializedItem.Attributes[ATTRIBUTE_ALPHA]?.Value, out a))
                a = 255;
            byte.TryParse(serializedItem.Attributes[ATTRIBUTE_RED]?.Value, out r);
            byte.TryParse(serializedItem.Attributes[ATTRIBUTE_GREEN]?.Value, out g);
            byte.TryParse(serializedItem.Attributes[ATTRIBUTE_BLUE]?.Value, out b);
            return Color.FromArgb(a, r, g, b);
        }

        public void SerializeItem(object item, object parentItem, XmlNode xmlNode, XmlDocument xmlDocument, object[] indicesOrKeys)
        {
            if (xmlNode is not XmlElement xmlElement)
                return;
            Color color = (item is Color colorCasted) ? colorCasted : Color.Black;
            xmlElement.SetAttribute(ATTRIBUTE_ALPHA, color.A.ToString());
            xmlElement.SetAttribute(ATTRIBUTE_RED, color.R.ToString());
            xmlElement.SetAttribute(ATTRIBUTE_GREEN, color.G.ToString());
            xmlElement.SetAttribute(ATTRIBUTE_BLUE, color.B.ToString());
        }

    }

}
