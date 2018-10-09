using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.TSL31
{
    class TSL31Type : IUMDType
    {

        public string Name => "TSL 3.1";

        public Type[] PortTypes => new Type[] { typeof(TSL31Port) };

        public int TallyCount => TALLY_COUNT;

        public const int TALLY_COUNT = 1;

    }
}
