using OpenSC.Model.Persistence;
using System;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.UMDs
{

    public class UmdTallyXmlSerializer : IValueXmlSerializer
    {

        public Type Type => typeof(UmdTally);

        private const string TAG_NAME = "tally";
        private const string ATTRIBUTE_COLOR = "color";

        public object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys)
        {
            UMD parentUmd = (UMD)parentItem;
            UmdTallyInfo thisTallyInfo = parentUmd.TallyInfo[(int)indicesOrKeys[0]];
            if (serializedItem.LocalName != TAG_NAME)
                return null;
            string sourceGlobalId = serializedItem.InnerText;
            string alignmentAttributeValue = serializedItem.Attributes[ATTRIBUTE_COLOR]?.Value;
            Color colorAttributeValueConverted = thisTallyInfo.DefaultColor;
            if (thisTallyInfo.ColorMode != UmdTallyInfo.ColorSettingMode.Fix)
            {
                try
                {
                    colorAttributeValueConverted = ColorTranslator.FromHtml(alignmentAttributeValue);
                }
                catch { }
            }
            return new UmdTally(parentUmd, (int)indicesOrKeys[0], thisTallyInfo)
            {
                _tfk_name_source = sourceGlobalId,
                Color = colorAttributeValueConverted
            };
        }

        public XElement SerializeItem(object item, object parentItem, object[] indicesOrKeys)
        {
            UMD parentUmd = (UMD)parentItem;
            UmdTallyInfo thisTallyInfo = parentUmd.TallyInfo[(int)indicesOrKeys[0]];
            UmdTally tally = item as UmdTally;
            if (tally == null)
                return null;
            XElement xmlElement = new XElement(TAG_NAME);
            xmlElement.Value = tally.Source?.Name ?? "";
            if (thisTallyInfo.ColorMode != UmdTallyInfo.ColorSettingMode.Fix)
                xmlElement.SetAttributeValue(ATTRIBUTE_COLOR, colorToString(tally.Color));
            return xmlElement;
        }

        private static string colorToString(Color color)
            => string.Format("#{0:X02}{1:X02}{2:X02}", color.R, color.G, color.B);

    }

}
