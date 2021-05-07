using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{
    public interface ISignalDestination
    {
        void AssignSource(ISignalSource source);
        ISignalSource CurrentSource { get; }
    }
}
