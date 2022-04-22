using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.BmdSmartView
{
    public record BmdSmartViewDisplayTallyInfo(string Name, UmdTallyInfo.ColorSettingMode ColorMode, Color DefaultColor, int DefaultPriority, string ProtocolCommandBorderValue)
        : UmdTallyInfo(Name, ColorMode, DefaultColor) { }
}
