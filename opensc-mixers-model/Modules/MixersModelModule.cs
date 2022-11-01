using OpenSC.Model;
using OpenSC.Model.Mixers;
using OpenSC.Model.Persistence;

namespace OpenSC.Modules
{

    [Module("mixers-model", "Mixers (model)", "TODO")]
    [DependsOnModule(typeof(SignalsModelModule))]
    public class MixersModelModule : BasetypeModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(MixerDatabase));
        }

        protected override void registerSerializers()
        {
            SerializerRegister.RegisterCompleteSerializer(new MixerInputXmlSerializer());
        }

    }

}
