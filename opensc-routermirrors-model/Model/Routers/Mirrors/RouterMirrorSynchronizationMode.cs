using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Mirrors
{
    public enum RouterMirrorSynchronizationMode
    {
        Never,
        FromSideA,
        FromSideB,
        FromFirstConnected,
        FromLastConnected
    }
}
