using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{
    public class FlipFlopDataStore : CustomBooleanDataStore
    {
        public FlipFlopType Type { get; set; }
        public IBoolean Input1 { get; set; }
        public IBoolean Input2 { get; set; }
        public bool Input1Inverted { get; set; }
        public bool Input2Inverted { get; set; }
    }
}
