using OpenSC.Model.Persistence;
using System;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.UMDs
{

    public class UmdTextXmlSerializer : IValueOnlyXmlSerializer
    {

        public virtual Type Type => typeof(UmdText);

        private const string TAG_NAME = "text";
        private const string ATTRIBUTE_STATICVALUE = "staticvalue";
        private const string ATTRIBUTE_USESTATICVALUE = "usestaticvalue";
        private const string ATTRIBUTE_USED = "used";
        private const string ATTRIBUTE_ALIGNMENT = "alignment";

        public virtual void DeserializeItem(XmlNode serializedItem, object item, object parentItem, object[] indicesOrKeys)
        {
            if (serializedItem.LocalName != TAG_NAME)
                return;
            if (item is not UmdText text)
                return;
            text._tfk_globalId_source = serializedItem.InnerText;
            text.StaticValue = serializedItem.Attributes[ATTRIBUTE_STATICVALUE]?.Value;
            string useStaticValueAttributeValue = serializedItem.Attributes[ATTRIBUTE_USESTATICVALUE]?.Value;
            if (bool.TryParse(useStaticValueAttributeValue, out bool useStaticValueAttributeValueConverted))
                text.UseStaticValue = useStaticValueAttributeValueConverted;
            string usedAttributeValue = serializedItem.Attributes[ATTRIBUTE_USED]?.Value;
            if (bool.TryParse(usedAttributeValue, out bool usedAttributeValueConverted))
                text.Used = usedAttributeValueConverted;
            string alignmentAttributeValue = serializedItem.Attributes[ATTRIBUTE_ALIGNMENT]?.Value;
            if (Enum.TryParse(alignmentAttributeValue, out UmdTextAlignment alignmentAttributeValueConverted))
                text.Alignment = alignmentAttributeValueConverted;
        }

        public virtual XElement SerializeItem(object item, object parentItem, object[] indicesOrKeys)
        {
            Umd parentUmd = (Umd)parentItem;
            UmdTextInfo thisTextInfo = parentUmd.TextInfo[(int)indicesOrKeys[0]];
            if (item is not UmdText text)
                return null;
            XElement xmlElement = new(TAG_NAME);
            xmlElement.Value = text.Source?.GlobalID ?? "";
            xmlElement.SetAttributeValue(ATTRIBUTE_STATICVALUE, text.StaticValue ?? "");
            xmlElement.SetAttributeValue(ATTRIBUTE_USESTATICVALUE, text.UseStaticValue.ToString());
            if (thisTextInfo.Switchable)
                xmlElement.SetAttributeValue(ATTRIBUTE_USED, text.Used.ToString());
            if (thisTextInfo.Alignable)
                xmlElement.SetAttributeValue(ATTRIBUTE_ALIGNMENT, text.Alignment.ToString());
            return xmlElement;
        }

    }

}
