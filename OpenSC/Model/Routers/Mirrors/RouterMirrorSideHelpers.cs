using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Mirrors
{
    public static class RouterMirrorSideHelpers
    {
        public static T Choose<T>(this RouterMirrorSide side, T objSideA, T objSideB, bool invert = false)
        {
            if ((!invert && (side == RouterMirrorSide.SideA)) || (invert && (side == RouterMirrorSide.SideB)))
                return objSideA;
            return objSideB;
        }
    }
}
