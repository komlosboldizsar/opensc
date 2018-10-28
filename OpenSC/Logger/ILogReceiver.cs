using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Logger
{
    public interface ILogReceiver
    {
        void ReceiveLogMessage(LogMessageType messageType, string tag, string message);
    }
}
