using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    internal interface IDimensionedPersistAttribute
    {
        int Dimension { get; }
    }
}
