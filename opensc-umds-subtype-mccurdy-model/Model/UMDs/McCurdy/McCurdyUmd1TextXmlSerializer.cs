using OpenSC.Model.Persistence;
using System;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.UMDs.McCurdy
{

    public class McCurdyUmd1TextXmlSerializer : UmdTextXmlSerializer
    {

        public override Type Type => typeof(McCurdyUmd1Text);

        private const string ATTRIBUTE_COLUMNWIDTH = "columnwidth";

        public override void DeserializeItem(XmlNode serializedItem, object item, object parentItem, object[] indicesOrKeys)
        {
            base.DeserializeItem(serializedItem, item, parentItem, indicesOrKeys);
            if (item is not McCurdyUmd1Text castedText)
                return;
            if (int.TryParse(serializedItem.Attributes[ATTRIBUTE_COLUMNWIDTH]?.Value, out int columnWidth))
                castedText.ColumnWidth = columnWidth;
            return;
        }

        public override void SerializeItem(object item, object parentItem, XmlNode xmlNode, XmlDocument xmlDocument, object[] indicesOrKeys)
        {
            if (item is not McCurdyUmd1Text mcCurdyUmd1Text)
                return;
            base.SerializeItem(item, parentItem, xmlNode, xmlDocument, indicesOrKeys);
            if (xmlNode is not XmlElement xmlElement)
                return;
            xmlElement.SetAttribute(ATTRIBUTE_COLUMNWIDTH, mcCurdyUmd1Text.ColumnWidth.ToString());
        }

    }

}
