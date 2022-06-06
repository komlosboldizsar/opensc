using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.Helpers
{
    public class FontStore
    {

        private static Dictionary<ValueTuple<FontFamily, FontStyle, int>, Font> fontsForDesiredHeights = new();

        public static Font getFontForDesiredHeight(FontFamily fontFamily, FontStyle fontStyle, int desiredHeight)
        {
            if (fontsForDesiredHeights.TryGetValue((fontFamily, fontStyle, desiredHeight), out Font returnValue))
                return returnValue;
            float fontSize = 12.0f;
            int difference = 0, previousDifference;
            Font font = new Font(fontFamily, fontSize);
            Size textSize;
            do
            {
                previousDifference = difference;
                textSize = TextRenderer.MeasureText("0yQŐ_+{?", font);
                fontSize *= desiredHeight / (float)textSize.Height;
                font.Dispose();
                font = new Font(fontFamily, fontSize, fontStyle);
                difference = desiredHeight - textSize.Height;
            } while ((difference > 0) && ((previousDifference / (float)difference) >= 0));
            fontsForDesiredHeights.Add((fontFamily, fontStyle, desiredHeight), font);
            return font;
        }

    }
}
