using OpenSC.Model.Persistence;
using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Labelsets
{

    public class LabelXmlSerializer : IInstantiatingXmlSerializer
    {

        public Type Type => typeof(Label);

        public const string ATTRIBUTE_OBJECT = "object";
        private const string ATTRIBUTE_TEXT = "text";

        public object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys)
        {
            Label label = ((Labelset)parentItem).CreateLabel();
            label.Text = serializedItem.Attributes[ATTRIBUTE_TEXT]?.Value;
            return label;
        }

        public void SerializeItem(object item, object parentItem, XmlNode xmlNode, XmlDocument xmlDocument, object[] indicesOrKeys)
        {
            if (xmlNode is not XmlElement xmlElement)
                return;
            if (item is not Label label)
                return;
            xmlElement.SetAttribute(ATTRIBUTE_TEXT, label.Text);
        }
        
    }

}
