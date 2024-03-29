﻿using OpenSC.Model;
using OpenSC.Model.Routers.CrosspointStores;

namespace OpenSC.Modules
{

    [Module("crosspointstores-model", "Crosspoint stores (model)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class CrosspointstoresModelModule : BasetypeModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(CrosspointStoreDatabase));
        }

        protected override void registerSerializers()
        { }

    }

}
