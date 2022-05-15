using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public delegate void StateChangedHandler(ISignalSource signalSource, ISignalTallyState tally, bool newState, List<object> recursionChain);

    public interface ISignalTallyState : ISignalTallyStateOrReceiver, INotifyPropertyChanged
    {
        bool State { get; }
        bool GetState(List<object> recursionChain);
        event StateChangedHandler StateChanged;
    }

}
