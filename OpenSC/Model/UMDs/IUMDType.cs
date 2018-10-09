using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs
{
    interface IUMDType
    {
        string Name { get; }
        int TallyCount { get; }
        Type[] PortTypes { get; }
    }
}
