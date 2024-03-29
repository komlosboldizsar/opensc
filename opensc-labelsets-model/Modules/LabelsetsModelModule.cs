﻿using OpenSC.Model;
using OpenSC.Model.Persistence;
using OpenSC.Model.Labelsets;

namespace OpenSC.Modules
{

    [Module("labelsets-model", "Labelsets (model)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class LabelsetsModelModule : BasetypeModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(LabelsetDatabase));
        }

        protected override void registerSerializers()
        {
            SerializerRegister.RegisterSerializer(new LabelXmlSerializer());
        }

    }

}
