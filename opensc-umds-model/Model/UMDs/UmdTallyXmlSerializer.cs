using OpenSC.Model.Persistence;
using System;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.UMDs
{

    public class UmdTallyXmlSerializer : IMemberXmlSerializer
    {

        public virtual Type Type => typeof(UmdTally);

        private const string TAG_NAME = "tally";
        private const string ATTRIBUTE_COLOR = "color";

        public virtual void DeserializeItem(XmlNode serializedItem, object item, object parentItem, object[] indicesOrKeys)
        {
            if (serializedItem.LocalName != TAG_NAME)
                return;
            if (item is not UmdTally tally)
                return;
            Umd parentUmd = (Umd)parentItem;
            UmdTallyInfo thisTallyInfo = parentUmd.TallyInfo[(int)indicesOrKeys[0]];
            tally._tfk_name_source = serializedItem.InnerText;
            if (thisTallyInfo.ColorMode != UmdTallyInfo.ColorSettingMode.Fix)
            {
                try
                {
                    tally.Color = ColorTranslator.FromHtml(serializedItem.Attributes[ATTRIBUTE_COLOR]?.Value);
                }
                catch { }
            }
        }

        public virtual XElement SerializeItem(object item, object parentItem, object[] indicesOrKeys)
        {
            Umd parentUmd = (Umd)parentItem;
            UmdTallyInfo thisTallyInfo = parentUmd.TallyInfo[(int)indicesOrKeys[0]];
            UmdTally tally = item as UmdTally;
            if (tally == null)
                return null;
            XElement xmlElement = new XElement(TAG_NAME);
            xmlElement.Value = tally.Source?.Identifier ?? "";
            if (thisTallyInfo.ColorMode != UmdTallyInfo.ColorSettingMode.Fix)
                xmlElement.SetAttributeValue(ATTRIBUTE_COLOR, colorToString(tally.Color));
            return xmlElement;
        }

        private static string colorToString(Color color)
            => string.Format("#{0:X02}{1:X02}{2:X02}", color.R, color.G, color.B);

    }

}
