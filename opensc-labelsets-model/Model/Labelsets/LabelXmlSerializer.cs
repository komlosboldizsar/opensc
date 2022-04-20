using OpenSC.Model.Persistence;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Labelsets
{

    public class LabelXmlSerializer : IValueXmlSerializer
    {

        public Type Type => typeof(Label);

        private const string TAG_NAME = "label";
        public const string ATTRIBUTE_OBJECT = "object";
        private const string ATTRIBUTE_TEXT = "text";

        public object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys)
        {
            if (serializedItem.LocalName != TAG_NAME)
                return null;
            Label label = ((Labelset)parentItem).CreateLabel();
            label.Text = serializedItem.Attributes[ATTRIBUTE_TEXT]?.Value;
            return label;
        }

        public XElement SerializeItem(object item, object parentItem, object[] indicesOrKeys)
        {
            Label label = item as Label;
            if (label == null)
                return null;
            XElement xmlElement = new XElement(TAG_NAME);
            xmlElement.SetAttributeValue(ATTRIBUTE_TEXT, label.Text);
            return xmlElement;
        }
        
    }

}
