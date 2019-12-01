using OpenSC.Model.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{

    public delegate void SourceSignalChangedDelegate(IRouterOutputAssignable output, ExternalSignal newSignal);
    
    public interface IRouterOutputAssignable
    {
        string Name { get; }
        ExternalSignal SourceSignal { get; }
        event SourceSignalChangedDelegate SourceSignalChanged;
    }
}
