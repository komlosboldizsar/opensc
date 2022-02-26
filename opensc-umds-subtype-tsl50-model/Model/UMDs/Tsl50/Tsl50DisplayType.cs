using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.Tsl50
{
    class Tsl50DisplayType : IUMDType
    {

        public string Name => "TSL 5.0";

        public int TallyCount => TALLY_COUNT;

        public const int TALLY_COUNT = 6;

    }
}
