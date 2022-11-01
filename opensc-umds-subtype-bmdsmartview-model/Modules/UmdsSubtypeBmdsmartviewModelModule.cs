using OpenSC.Model;
using OpenSC.Model.Persistence;
using OpenSC.Model.UMDs;
using OpenSC.Model.UMDs.BmdSmartView;

namespace OpenSC.Modules
{

    [Module("umds-subtype-bmdsmartview-model", "UMDs / BMD SmartView (model)", "TODO")]
    [DependsOnModule(typeof(UmdsModelModule))]
    public class UmdsSubtypeBmdsmartviewModelModule : SubtypeModelModuleBase<UmdsModelModule>
    {

        public override void Initialize()
        {
            base.Initialize();
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(BmdSmartViewUnitDatabase));
        }

        protected override void registerModelTypes()
        {
            UmdTypeRegister.Instance.RegisterType<BmdSmartViewDisplay>();
        }

        protected override void registerSerializers()
        {
            SerializerRegister.RegisterMemberSerializer(new BmdSmartViewDisplayTallyXmlSerializer());
        }

    }

}
