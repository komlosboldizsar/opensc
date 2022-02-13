using OpenSC.Model;
using OpenSC.Model.UMDs;
using OpenSC.Model.UMDs.Tsl50;

namespace OpenSC.Modules
{

    [Module("umds-subtype-tsl50-model", "UMDs / TSL 5.0 (model)", "TODO")]
    [DependsOnModule(typeof(UmdsModelModule))]
    public class UmdsSubtypeTsl50ModelModule : SubtypeModelModuleBase<UmdsModelModule>
    {

        public override void Initialize()
        {
            base.Initialize();
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(Tsl50ScreenDatabase));
        }

        protected override void registerModelTypes()
        {
            UmdTypeRegister.Instance.RegisterType<Tsl50Display>();
        }

        protected override void registerSerializers()
        { }

    }

}
