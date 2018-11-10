using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public delegate void SignalTallyChangedDelegate(ISignal signal, bool oldState, bool newState);
    public delegate void SignalLabelChangedDelegate(ISignal signal, string newLabel);

    public interface ISignal
    {

        string SignalLabel { get; }
        event SignalLabelChangedDelegate SignalLabelChanged;

        string SignalUniqueId { get; }

        bool RedTally { get; }
        event SignalTallyChangedDelegate RedTallyChanged;

        bool GreenTally { get; }
        event SignalTallyChangedDelegate GreenTallyChanged;

        void IsTalliedFrom(ISignalTallySource source, SignalTallyType type, bool isTallied);

    }

}
