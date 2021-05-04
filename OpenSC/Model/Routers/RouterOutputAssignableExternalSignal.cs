using OpenSC.Model.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{

    public class RouterOutputAssignableExternalSignal : IRouterOutputAssignable
    {

        private ExternalSignal signal;

        public RouterOutputAssignableExternalSignal(ExternalSignal signal)
        {
            this.signal = signal;
        }
        public string Name => signal.Name;

        public ExternalSignal SourceSignal => signal;

        public event SourceSignalChangedDelegate SourceSignalChanged
        {
            add { }
            remove { }
        }

        public static List<IRouterOutputAssignable> GetAll()
        {
            List<IRouterOutputAssignable> allSignalsProxies = new List<IRouterOutputAssignable>();
            foreach (ExternalSignal externalSignal in ExternalSignalDatabases.Signals)
                allSignalsProxies.Add(new RouterOutputAssignableExternalSignal(externalSignal));
            return allSignalsProxies;
        }

    }

}
