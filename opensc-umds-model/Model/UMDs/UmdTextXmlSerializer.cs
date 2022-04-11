using OpenSC.Model.Persistence;
using System;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.UMDs
{

    public class UmdTextXmlSerializer : IValueXmlSerializer
    {

        public Type Type => typeof(UmdText);

        private const string TAG_NAME = "text";
        private const string ATTRIBUTE_STATICVALUE = "staticvalue";
        private const string ATTRIBUTE_USESTATICVALUE = "usestaticvalue";
        private const string ATTRIBUTE_USED = "used";
        private const string ATTRIBUTE_ALIGNMENT = "alignment";

        public object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys)
        {
            UMD parentUmd = (UMD)parentItem;
            UmdTextInfo thisTextInfo = parentUmd.TextInfo[(int)indicesOrKeys[0]];
            if (serializedItem.LocalName != TAG_NAME)
                return null;
            string sourceGlobalId = serializedItem.InnerText;
            string staticValueAttributeValue = serializedItem.Attributes[ATTRIBUTE_STATICVALUE]?.Value;
            string useStaticValueAttributeValue = serializedItem.Attributes[ATTRIBUTE_USESTATICVALUE]?.Value;
            if (!bool.TryParse(useStaticValueAttributeValue, out bool useStaticValueAttributeValueConverted))
                useStaticValueAttributeValueConverted = false;
            string usedAttributeValue = serializedItem.Attributes[ATTRIBUTE_USED]?.Value;
            if (!bool.TryParse(usedAttributeValue, out bool usedAttributeValueConverted))
                usedAttributeValueConverted = thisTextInfo.DefaultUsed;
            string alignmentAttributeValue = serializedItem.Attributes[ATTRIBUTE_ALIGNMENT]?.Value;
            if (!Enum.TryParse(alignmentAttributeValue, out UmdTextAlignment alignmentAttributeValueConverted))
                alignmentAttributeValueConverted = thisTextInfo.DefaultAlignment;
            return new UmdText(parentUmd, (int)indicesOrKeys[0], thisTextInfo)
            {
                _tfk_globalId_source = sourceGlobalId,
                StaticValue = staticValueAttributeValue,
                UseStaticValue = useStaticValueAttributeValueConverted,
                Used = usedAttributeValueConverted,
                Alignment = alignmentAttributeValueConverted
            };
        }

        public XElement SerializeItem(object item, object parentItem, object[] indicesOrKeys)
        {
            UMD parentUmd = (UMD)parentItem;
            UmdTextInfo thisTextInfo = parentUmd.TextInfo[(int)indicesOrKeys[0]];
            UmdText text = item as UmdText;
            if (text == null)
                return null;
            XElement xmlElement = new XElement(TAG_NAME);
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
