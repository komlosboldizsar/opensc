using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.BmdAtemMv
{
    public class BmdAtemMvRouterOutput : RouterOutput
    {

        public BmdAtemMvRouterOutput() : base()
        { }

        public BmdAtemMvRouterOutput(string name, Router router, int index) : base(name, router, index)
        { }

        protected override RouterOutputLockInfo LockInfo { get; } = RouterOutputLockInfo.NotSupported;
        protected override RouterOutputLockInfo ProtectInfo { get; } = RouterOutputLockInfo.NotSupported;

    }

}
