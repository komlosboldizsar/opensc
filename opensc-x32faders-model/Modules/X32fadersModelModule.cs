using OpenSC.Model;
using OpenSC.Model.Persistence;
using OpenSC.Model.X32Faders;

namespace OpenSC.Modules
{

    [Module("x32faders-model", "X32 faders (model)", "TODO")]
    public class X32fadersModelModule : BasetypeModuleBase
    {

        public override void Initialize()
        {
            base.Initialize();
            X32FaderCommons.Init();
        }

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(X32FaderDatabase));
        }

        protected override void registerSerializers()
        { }

    }

}
