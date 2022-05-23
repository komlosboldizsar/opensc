using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Virtual
{
    public class VirtualRouterOutput : RouterOutput
    {

        public VirtualRouterOutput() : base()
        { }

        public VirtualRouterOutput(string name, Router router, int index) : base(name, router, index)
        { }

        #region Supported lock operations
        public override bool LocksSupported => true;
        public override bool ProtectsSupported => true;

        public override RouterOutputLockOwnerKnowLevel LockOwnerKnowLevel => RouterOutputLockOwnerKnowLevel.None;
        public override RouterOutputLockOwnerKnowLevel ProtectOwnerKnowLevel => RouterOutputLockOwnerKnowLevel.None;
        #endregion

    }

}
