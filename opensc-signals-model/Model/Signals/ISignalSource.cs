using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public delegate void RegisteredSourceSignalNameChangedDelegate(ISignalSource signal, string newName, List<object> recursionChain);
    public delegate void RegisteredSourceSignalChangedDelegate(ISignalSource signal, ISignalSourceRegistered registeredSignal, List<object> recursionChain);

    public interface ISignalSource : ISystemObject
    {

        #region Property: RegisteredSourceSignalName
        string RegisteredSourceSignalName { get; }
        string GetRegisteredSourceSignalName(List<object> recursionChain = null);
        event RegisteredSourceSignalNameChangedDelegate RegisteredSourceSignalNameChanged;
        #endregion

        #region Propety: RegisteredSourceSignal
        ISignalSourceRegistered RegisteredSourceSignal { get; }
        ISignalSourceRegistered GetRegisteredSourceSignal(List<object> recursionChain = null);
        event RegisteredSourceSignalChangedDelegate RegisteredSourceSignalChanged;
        #endregion

        #region Tallies
        IBidirectionalSignalTally RedTally { get; }
        IBidirectionalSignalTally YellowTally { get; }
        IBidirectionalSignalTally GreenTally { get; }
        #endregion

    }

}
