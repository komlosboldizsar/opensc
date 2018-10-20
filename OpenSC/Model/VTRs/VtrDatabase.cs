using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.VTRs
{
    [DatabaseName("vtrs")]
    [PolymorphDatabase(typeof(VtrTypeNameConverter))]
    public class VtrDatabase : DatabaseBase<Vtr>
    {

        public static VtrDatabase Instance { get; } = new VtrDatabase();

    }
}
