using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public interface ISignalSourceRegistered : ISignalSource
    {

        #region Property: SignalUniqueId
        string SignalUniqueId { get; }
        event PropertyChangedOneValueDelegate<ISignalSourceRegistered, string> SignalUniqueIdChanged;
        #endregion

        #region Property: SignalLabel
        string SignalLabel { get; }
        event PropertyChangedOneValueDelegate<ISignalSourceRegistered, string> SignalLabelChanged;
        #endregion

    }

}
