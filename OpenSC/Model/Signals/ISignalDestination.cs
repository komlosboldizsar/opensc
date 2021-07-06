using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public delegate void CurrentSourceChangedDelegate(ISignalDestination signalDestination, ISignalSource newSource);

    public interface ISignalDestination
    {
        void AssignSource(ISignalSource source);
        ISignalSource CurrentSource { get; }
        event CurrentSourceChangedDelegate CurrentSourceChanged;
    }
}
