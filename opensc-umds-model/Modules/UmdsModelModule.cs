using OpenSC.Model;
using OpenSC.Model.Persistence;
using OpenSC.Model.UMDs;

namespace OpenSC.Modules
{

    [Module("umds-model", "UMDs (model)", "TODO")]
    [DependsOnModule(typeof(DynamictextsModelModule))]
    public class UmdsModelModule : BasetypeModuleBase
    {

        public override void Initialize()
        {
            base.Initialize();
            UmdPeriodicUpdater.Start();
        }

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(UmdDatabase));
        }

        protected override void registerSerializers()
        {
            DatabasePersister<Umd>.RegisterSerializer(new UmdTextXmlSerializer());
            DatabasePersister<Umd>.RegisterSerializer(new UmdTallyXmlSerializer());
        }

    }

}
