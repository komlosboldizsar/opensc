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

        public override XElement SerializeItem(object item, object parentItem, object[] indicesOrKeys)
        {
            if (item is not McCurdyUmd1Text castedText)
                return null;
            XElement serializedText = base.SerializeItem(item, parentItem, indicesOrKeys);
            serializedText.SetAttributeValue(ATTRIBUTE_COLUMNWIDTH, castedText.ColumnWidth);
            return serializedText;
        }

    }

}
