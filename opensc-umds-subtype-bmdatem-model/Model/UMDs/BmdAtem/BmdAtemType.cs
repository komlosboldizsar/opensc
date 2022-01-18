using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.BmdAtem
{
    class BmdAtemType : IUMDType
    {

        public string Name => "BMD ATEM";

        public int TallyCount => TALLY_COUNT;

        public const int TALLY_COUNT = 0;

    }
}
