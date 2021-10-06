using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.McCurdy
{
    class McCurdyUMD1Type : IUMDType
    {

        public string Name => "McCurdy UMD-1";

        public int TallyCount => TALLY_COUNT;

        public const int TALLY_COUNT = 0;

    }
}
