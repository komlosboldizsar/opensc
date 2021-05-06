using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public delegate void SourceSignalNameChangedDelegate(ISignalSource signal, string newName);
    public delegate void SignalLabelChangedDelegate(ISignalSource signal, string newLabel);
    public delegate void SignalTallyChangedDelegate(ISignalSource signal, bool oldState, bool newState);

    public interface ISignalSource
    {

        string SourceSignalName { get; }
        string GetSourceSignalName(List<object> recursionChain = null);
        event SourceSignalNameChangedDelegate SourceSignalNameChanged;

        ISignalSourceRegistered RegisteredSourceSignal { get; }
        ISignalSourceRegistered GetRegisteredSourceSignal(List<object> recursionChain = null);

        IBidirectionalSignalTally RedTally { get; }
        IBidirectionalSignalTally YellowTally { get; }
        IBidirectionalSignalTally GreenTally { get; }

    }

}
