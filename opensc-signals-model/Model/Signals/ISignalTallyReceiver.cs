using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public delegate void SignalTallyReceiverGotTally(ISignalTallyReceiver receiver, List<ISignalTallySender> recursionChain);
    public delegate void SignalTallyReceiverRevokedTally(ISignalTallyReceiver receiver, List<ISignalTallySender> recursionChain);

    public interface ISignalTallyReceiver : ISignalTallyStateOrReceiver
    {
        void Give(List<ISignalTallySender> recursionChain);
        void Revoke(List<ISignalTallySender> recursionChain);
        event SignalTallyReceiverGotTally Got;
        event SignalTallyReceiverRevokedTally Revoked;
        List<List<ISignalTallySender>> CurrentRecursionChains { get; }
    }

}
