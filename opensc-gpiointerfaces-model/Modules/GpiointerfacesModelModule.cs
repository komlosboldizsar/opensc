using OpenSC.Model;
using OpenSC.Model.Persistence;
using OpenSC.Model.GpioInterfaces;

namespace OpenSC.Modules
{

    [Module("gpiointerfaces-model", "GPIO interfaces (model)", "TODO")]
    [DependsOnModule(typeof(BooleansModelModule))]
    public class GpiointerfacesModelModule : BasetypeModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(GpioInterfaceDatabase));
        }

        protected override void registerSerializers()
        {
            SerializerRegister.RegisterCompleteSerializer(new GpioInterfaceInputXmlSerializer());
            SerializerRegister.RegisterCompleteSerializer(new GpioInterfaceOutputXmlSerializer());
        }

    }

}
