using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{
    public static class ISignalSourceTallyHelper
    {
        public static IBidirectionalSignalTally GetTally(this ISignalSource signalSource, SignalTallyColor color)
        {
            switch (color)
            {
                case SignalTallyColor.Red:
                    return signalSource.RedTally;
                case SignalTallyColor.Yellow:
                    return signalSource.YellowTally;
                case SignalTallyColor.Green:
                    return signalSource.GreenTally;
            }
            return null;
        }
    }
}
