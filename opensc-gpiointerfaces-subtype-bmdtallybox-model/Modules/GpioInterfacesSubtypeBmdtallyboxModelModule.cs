using OpenSC.Model.Persistence;
using OpenSC.Model.GpioInterfaces;
using OpenSC.Model.GpioInterfaces.BlackMagicDesign;

namespace OpenSC.Modules
{

    [Module("gpiointerfaces-subtype-bmdtallybox-model", "GPIO intefaces / BlackMagic Design Tally Box (model)", "TODO")]
    [DependsOnModule(typeof(GpiointerfacesModelModule))]
    public class GpioInterfacesSubtypeBmdtallyboxModelModule : SubtypeModelModuleBase<GpiointerfacesModelModule>
    {

        protected override void registerModelTypes()
        {
            GpioInterfaceTypeRegister.Instance.RegisterType<BmdTallyBox>();
        }

        protected override void registerSerializers()
        {
            DatabasePersister<GpioInterface>.RegisterSerializer(new BmdTallyBoxInputXmlSerializer());
            DatabasePersister<GpioInterface>.RegisterSerializer(new BmdTallyBoxOutputXmlSerializer());
        }

    }

}
