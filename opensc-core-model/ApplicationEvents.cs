using OpenSC.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC
{

    public class ApplicationEvents
    {

        public static event EventHandler Exiting;
        public static void FireExitingEvent(object sender, System.EventArgs eventArgs)
        {
            Exiting?.Invoke(sender, eventArgs);
            LogDispatcher.I($"Exit event, source: [{sender}].");
        }

    }

}
