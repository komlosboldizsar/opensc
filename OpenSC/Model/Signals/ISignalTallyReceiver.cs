using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public interface ISignalTallyReceiver : ISignalTallyStateOrReceiver
    {
        void Give(List<ISignalTallySender> recursionChain);
        void Revoke(List<ISignalTallySender> recursionChain);
    }

}
