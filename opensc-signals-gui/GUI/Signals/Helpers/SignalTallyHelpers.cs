using OpenSC.Model.Signals;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.Signals.TallyCopying.Helpers
{
    public static class SignalTallyHelpers
    {

        public static Color COLOR_LIGHT_RED = Color.LightPink;
        public static Color COLOR_LIGHT_YELLOW = Color.LightYellow;
        public static Color COLOR_LIGHT_GREEN = Color.LightGreen;
        public static Color COLOR_LIGHT_DEFAULT = Color.White;

        public static Color ConvertToLightColor(this SignalTallyColor signalTallyColor)
        {
            switch (signalTallyColor)
            {
                case SignalTallyColor.Red:
                    return COLOR_LIGHT_RED;
                case SignalTallyColor.Yellow:
                    return COLOR_LIGHT_YELLOW;
                case SignalTallyColor.Green:
                    return COLOR_LIGHT_GREEN;
            }
            return COLOR_LIGHT_DEFAULT;
        }
    }
}
