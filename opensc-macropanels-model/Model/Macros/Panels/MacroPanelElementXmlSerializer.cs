using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Macros
{

    public class MacroPanelElementXmlSerializer : IInstantiatingXmlSerializer
    {

        public Type Type => typeof(MacroPanelElement);

        private const string ATTRIBUTE_MACRO = "macro";
        private const string ATTRIBUTE_LABEL = "label";
        private const string ATTRIBUTE_SHOWLABEL = "showlabel";
        private const string ATTRIBUTE_BACKCOLOR = "backcolor";
        private const string ATTRIBUTE_FORECOLOR = "forecolor";
        private const string ATTRIBUTE_X = "x";
        private const string ATTRIBUTE_Y = "y";
        private const string ATTRIBUTE_W = "w";
        private const string ATTRIBUTE_H = "h";

        public object DeserializeItem(XmlNode serializedItem, object parentItem, object[] indicesOrKeys)
        {

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

        public void SerializeItem(object item, object parentItem, XmlNode xmlNode, XmlDocument xmlDocument, object[] indicesOrKeys)
        {
            if (xmlNode is not XmlElement xmlElement)
                return;
            if (item is not MacroPanelElement element)
                return;
            xmlElement.SetAttribute(ATTRIBUTE_MACRO, (element.Macro != null) ? element.Macro.ID.ToString() : string.Empty);
            xmlElement.SetAttribute(ATTRIBUTE_LABEL, element.Label ?? string.Empty);
            xmlElement.SetAttribute(ATTRIBUTE_SHOWLABEL, (element.ShowLabel ? "true" : "false"));
            xmlElement.SetAttribute(ATTRIBUTE_BACKCOLOR, ColorToHexString(element.BackColor));
            xmlElement.SetAttribute(ATTRIBUTE_FORECOLOR, ColorToHexString(element.ForeColor));
            xmlElement.SetAttribute(ATTRIBUTE_X, element.PositionX.ToString());
            xmlElement.SetAttribute(ATTRIBUTE_Y, element.PositionY.ToString());
            xmlElement.SetAttribute(ATTRIBUTE_W, element.SizeW.ToString());
            xmlElement.SetAttribute(ATTRIBUTE_H, element.SizeH.ToString());
        }

        // @source https://www.cambiaresearch.com/articles/1/convert-dotnet-color-to-hex-string
        static readonly char[] hexDigits = {
            '0', '1', '2', '3', '4', '5', '6', '7',
            '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'
        };

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
