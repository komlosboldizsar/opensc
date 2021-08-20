using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Leitch
{
    public class LeitchRouterOutput : RouterOutput
    {

        public LeitchRouterOutput() : base()
        { }

        public LeitchRouterOutput(string name, Router router, int index) : base(name, router, index)
        { }

        public int LockProtectOwner { get; set; } = -1;

        #region Supported lock operations
        public override bool LocksSupported => true;
        public override bool ProtectsSupported => true;

        public override bool LockOwnerKnown => true;
        public override bool ProtectOwnerKnown => true;
        #endregion

    }

}
