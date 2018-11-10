using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public delegate void SourceSignalNameChangedDelegate(ISignal signal, string newName);
    public delegate void SignalLabelChangedDelegate(ISignal signal, string newLabel);
    public delegate void SignalTallyChangedDelegate(ISignal signal, bool oldState, bool newState);

    public interface ISignal
    {

        string SourceSignalName { get; }
        string GetSourceSignalName(List<object> recursionChain = null);
        event SourceSignalNameChangedDelegate SourceSignalNameChanged;

        string SignalLabel { get; }
        event SignalLabelChangedDelegate SignalLabelChanged;

        string SignalUniqueId { get; }

        bool RedTally { get; }
        bool GetRedTally(List<object> recursionChain = null);
        event SignalTallyChangedDelegate RedTallyChanged;

        bool GreenTally { get; }
        bool GetGreenTally(List<object> recursionChain = null);
        event SignalTallyChangedDelegate GreenTallyChanged;

        void IsTalliedFrom(ISignalTallySource source, SignalTallyType type, bool isTallied);

    }

}
