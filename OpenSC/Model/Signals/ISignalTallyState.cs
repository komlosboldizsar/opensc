using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public delegate void StateChangedHandler(ISignalSource signalSource, ISignalTallyState tally, bool newState);

    public interface ISignalTallyState : ISignalTallyStateOrReceiver
    {

        #region Property: ParentSignalSource
        ISignalSource ParentSignalSource { get; }
        #endregion

        #region Property: State
        bool State { get; }
        bool GetState(List<object> recursionChain);
        event StateChangedHandler StateChanged;
        #endregion

    }

}
