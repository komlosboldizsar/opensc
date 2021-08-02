using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public delegate void SignalUniqueIdChangedDelegate(ISignalSourceRegistered signal, string newName);
    public delegate void SignalLabelChangedDelegate(ISignalSourceRegistered signal, string newLabel);

    public interface ISignalSourceRegistered : ISignalSource
    {

        #region Property: SignalUniqueId
        string SignalUniqueId { get; }
        event SignalUniqueIdChangedDelegate SignalUniqueIdChanged;
        #endregion

        #region Property: SignalLabel
        string SignalLabel { get; }
        event SignalLabelChangedDelegate SignalLabelChanged;
        #endregion

    }

}
