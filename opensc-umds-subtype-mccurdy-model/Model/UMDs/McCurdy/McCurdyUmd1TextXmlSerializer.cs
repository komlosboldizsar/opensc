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

        public override object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys)
        {
            McCurdyUmd1Text restoredText = base.DeserializeItem(serializedItem, parentItem, indicesOrKeys) as McCurdyUmd1Text;
            if (!int.TryParse(serializedItem.Attributes[ATTRIBUTE_COLUMNWIDTH]?.Value, out int columnWidth))
                columnWidth = 0;
            restoredText.ColumnWidth = columnWidth;
            return restoredText;
        }

        public override XElement SerializeItem(object item, object parentItem, object[] indicesOrKeys)
        {
            McCurdyUmd1Text text = item as McCurdyUmd1Text;
            if (text == null)
                return null;
            XElement serializedText = base.SerializeItem(item, parentItem, indicesOrKeys);
            serializedText.SetAttributeValue(ATTRIBUTE_COLUMNWIDTH, text.ColumnWidth);
            return serializedText;
        }

    }

}
