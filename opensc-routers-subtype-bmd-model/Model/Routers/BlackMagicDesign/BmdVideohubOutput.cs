using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.BlackMagicDesign
{
    public class BmdVideohubOutput : RouterOutput
    {

        public BmdVideohubOutput() : base() { }
        public BmdVideohubOutput(string name, Router router, int index) : base(name, router, index) { }

        protected override RouterOutputLockInfo LockInfo { get; } = new RouterOutputLockInfo(true, RouterOutputLockOwnerKnowLevel.LocalOrRemote);
        protected override RouterOutputLockInfo ProtectInfo { get; } = RouterOutputLockInfo.NotSupported;

    }

}
