using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs
{
    public record UmdTallyInfo(string Name, UmdTallyInfo.ColorSettingMode ColorMode, Color DefaultColor)
    {
        public enum ColorSettingMode
        {
            Fix,
            LocalChangeable,
            RemoteChangeable
        }
    }
}
