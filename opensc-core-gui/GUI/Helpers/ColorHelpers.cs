using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.Helpers
{
    public static class ColorHelpers
    {

        public static Color GetTextColorForBackground(this Color color)
            => (color.GetBrightness() > 0.5f) ? Color.Black : Color.White;
        public static Brush GetTextBrushForBackground(this Color color)
            => (color.GetBrightness() > 0.5f) ? Brushes.Black : Brushes.White;

    }
}
