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

        public const string LOG_TAG = "ApplicationEvents";

        public static event EventHandler Exiting;
        public static void FireExitingEvent(object sender, System.EventArgs eventArgs)
        {
            Exiting?.Invoke(sender, eventArgs);
            LogDispatcher.I(LOG_TAG, $"Exit event, source: [{sender}].");
        }

    }

}
