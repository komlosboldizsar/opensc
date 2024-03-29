﻿using System;
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

        protected override RouterOutputLockInfo LockInfo { get; } = new RouterOutputLockInfo(true, RouterOutputLockOwnerKnowLevel.None);
        protected override RouterOutputLockInfo ProtectInfo { get; } = new RouterOutputLockInfo(true, RouterOutputLockOwnerKnowLevel.None);

    }

}
