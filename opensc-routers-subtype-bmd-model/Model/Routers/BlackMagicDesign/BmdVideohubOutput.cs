using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.BlackMagicDesign
{
    public class BmdVideohubOutput : RouterOutput
    {

        public BmdVideohubOutput() : base()
        { }

        public BmdVideohubOutput(string name, Router router, int index) : base(name, router, index)
        { }

        #region Supported lock operations
        public override bool LocksSupported => true;
        public override bool ProtectsSupported => false;

        public override bool LockOwnerKnown => true;
        public override bool ProtectOwnerKnown => false;
        #endregion

    }

}
