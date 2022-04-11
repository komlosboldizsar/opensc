using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Persistence
{

    class ColorXmlSerializer : IValueXmlSerializer
    {

        public Type Type => typeof(Color);

        private const string XML_TAG_NAME = "color";
        private const string ATTRIBUTE_ALPHA = "a";
        private const string ATTRIBUTE_RED = "r";
        private const string ATTRIBUTE_GREEN = "g";
        private const string ATTRIBUTE_BLUE = "b";

        public object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys)
        {
            if (serializedItem.LocalName != XML_TAG_NAME)
                return Color.Black;
            byte a, r, g, b;
            if (!byte.TryParse(serializedItem.Attributes[ATTRIBUTE_ALPHA]?.Value, out a))
                a = 255;
            byte.TryParse(serializedItem.Attributes[ATTRIBUTE_RED]?.Value, out r);
            byte.TryParse(serializedItem.Attributes[ATTRIBUTE_GREEN]?.Value, out g);
            byte.TryParse(serializedItem.Attributes[ATTRIBUTE_BLUE]?.Value, out b);
            return Color.FromArgb(a, r, g, b);
        }

        public XElement SerializeItem(object item, object parentItem, object[] indicesOrKeys)
        {
            Color color = (item is Color) ? (Color)item : Color.Black;
            XElement xmlElement = new XElement(XML_TAG_NAME);
            xmlElement.SetAttributeValue(ATTRIBUTE_ALPHA, color.A);
            xmlElement.SetAttributeValue(ATTRIBUTE_RED, color.R);
            xmlElement.SetAttributeValue(ATTRIBUTE_GREEN, color.G);
            xmlElement.SetAttributeValue(ATTRIBUTE_BLUE, color.B);
            return xmlElement;
        }

    }

}
