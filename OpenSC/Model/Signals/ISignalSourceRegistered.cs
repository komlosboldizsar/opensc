using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{
    
    public interface ISignalSourceRegistered : ISignalSource
    {

        string SignalUniqueId { get; }

        string SignalLabel { get; }
        event SignalLabelChangedDelegate SignalLabelChanged;

    }

}
