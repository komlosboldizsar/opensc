using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Macros
{

    public class MacroPanelElementXmlSerializer : IValueXmlSerializer
    {

        public Type Type => typeof(MacroPanelElement);

        private const string TAG_NAME = "macro_panel_element";
        private const string ATTRIBUTE_MACRO = "macro";
        private const string ATTRIBUTE_LABEL = "label";
        private const string ATTRIBUTE_SHOWLABEL = "showlabel";
        private const string ATTRIBUTE_BACKCOLOR = "backcolor";
        private const string ATTRIBUTE_FORECOLOR = "forecolor";
        private const string ATTRIBUTE_X = "x";
        private const string ATTRIBUTE_Y = "y";
        private const string ATTRIBUTE_W = "w";
        private const string ATTRIBUTE_H = "h";
        public object DeserializeItem(XmlNode serializedItem, object parentItem)
        {

            if (serializedItem.LocalName != TAG_NAME)
                return null;

            string macroIdStr = serializedItem.Attributes[ATTRIBUTE_MACRO]?.Value;
            if (!int.TryParse(macroIdStr, out int macroId) || (macroId < 0))
                macroId = -1;

            string label = serializedItem.Attributes[ATTRIBUTE_LABEL]?.Value;
            bool showLabel = (serializedItem.Attributes[ATTRIBUTE_SHOWLABEL]?.Value == "true");

            string backColorStr = serializedItem.Attributes[ATTRIBUTE_BACKCOLOR]?.Value;
            Color backColor = ColorTranslator.FromHtml(backColorStr);

            string foreColorStr = serializedItem.Attributes[ATTRIBUTE_FORECOLOR]?.Value;
            Color foreColor = ColorTranslator.FromHtml(foreColorStr);

            string xStr = serializedItem.Attributes[ATTRIBUTE_X]?.Value;
            if (!int.TryParse(xStr, out int x) || (x < 0))
                return null;

            string yStr = serializedItem.Attributes[ATTRIBUTE_Y]?.Value;
            if (!int.TryParse(yStr, out int y) || (y < 0))
                return null;

            string wStr = serializedItem.Attributes[ATTRIBUTE_W]?.Value;
            if (!int.TryParse(wStr, out int w) || (w < 0))
                return null;

            string hStr = serializedItem.Attributes[ATTRIBUTE_H]?.Value;
            if (!int.TryParse(hStr, out int h) || (h < 0))
                return null;

            return new MacroPanelElement(macroId)
            {
                Label = label,
                ShowLabel = showLabel,
                BackColor = backColor,
                ForeColor = foreColor,
                PositionX = x,
                PositionY = y,
                SizeW = w,
                SizeH = h
            };

        }

        public XElement SerializeItem(object item, object parentItem)
        {

            MacroPanelElement element = item as MacroPanelElement;
            if (element == null)
                return null;

            XElement xmlElement = new XElement(TAG_NAME);
            xmlElement.SetAttributeValue(ATTRIBUTE_MACRO, element.Macro?.ID);
            xmlElement.SetAttributeValue(ATTRIBUTE_LABEL, element.Label ?? "");
            xmlElement.SetAttributeValue(ATTRIBUTE_SHOWLABEL, (element.ShowLabel ? "true" : "false"));
            xmlElement.SetAttributeValue(ATTRIBUTE_BACKCOLOR, ColorToHexString(element.BackColor));
            xmlElement.SetAttributeValue(ATTRIBUTE_FORECOLOR, ColorToHexString(element.ForeColor));
            xmlElement.SetAttributeValue(ATTRIBUTE_X, element.PositionX);
            xmlElement.SetAttributeValue(ATTRIBUTE_Y, element.PositionY);
            xmlElement.SetAttributeValue(ATTRIBUTE_W, element.SizeW);
            xmlElement.SetAttributeValue(ATTRIBUTE_H, element.SizeH);

            return xmlElement;

        }

        // @source https://www.cambiaresearch.com/articles/1/convert-dotnet-color-to-hex-string
        static char[] hexDigits = {
         '0', '1', '2', '3', '4', '5', '6', '7',
         '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};
        public static string ColorToHexString(Color color)
        {
            byte[] bytes = new byte[3];
            bytes[0] = color.R;
            bytes[1] = color.G;
            bytes[2] = color.B;
            char[] chars = new char[bytes.Length * 2 + 1];
            chars[0] = '#';
            for (int i = 0; i < bytes.Length; i++)
            {
                int b = bytes[i];
                chars[i * 2 + 1] = hexDigits[b >> 4];
                chars[i * 2 + 2] = hexDigits[b & 0xF];
            }
            return new string(chars);
        }

    }

}
