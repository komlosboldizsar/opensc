using OpenSC.Model.Persistence;
using System;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.UMDs
{

    public class UmdTallyXmlSerializer : IValueXmlSerializer
    {

        public virtual Type Type => typeof(UmdTally);

        private const string TAG_NAME = "tally";
        private const string ATTRIBUTE_COLOR = "color";

        public virtual object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys)
        {
            Umd parentUmd = (Umd)parentItem;
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
            UmdTally tally = parentUmd.CreateTally(parentUmd, (int)indicesOrKeys[0], thisTallyInfo);
            tally._tfk_name_source = sourceGlobalId;
            tally.Color = colorAttributeValueConverted;
            return tally;
        }

        public virtual XElement SerializeItem(object item, object parentItem, object[] indicesOrKeys)
        {
            Umd parentUmd = (Umd)parentItem;
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
