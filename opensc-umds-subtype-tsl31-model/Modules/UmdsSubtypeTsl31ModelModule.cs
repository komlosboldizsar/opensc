﻿using OpenSC.Model;
using OpenSC.Model.UMDs;
using OpenSC.Model.UMDs.Tsl31;

namespace OpenSC.Modules
{

    [Module("umds-subtype-tsl31-model", "UMDs / TSL 3.1 (model)", "TODO")]
    [DependsOnModule(typeof(UmdsModelModule))]
    public class UmdsSubtypeTsl31ModelModule : SubtypeModelModuleBase<UmdsModelModule>
    {

        protected override void registerModelTypes()
        {
            UmdTypeRegister.Instance.RegisterType<Tsl31>();
        }

        protected override void registerSerializers()
        { }

    }

}
