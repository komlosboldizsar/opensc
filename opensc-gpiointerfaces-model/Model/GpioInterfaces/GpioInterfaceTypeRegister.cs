using OpenSC.Model.Persistence;

namespace OpenSC.Model.GpioInterfaces
{
    public class GpioInterfaceTypeRegister : ModelTypeRegisterBase<GpioInterface>
    {
        public static ModelTypeRegisterBase<GpioInterface> Instance { get; } = new GpioInterfaceTypeRegister();
        private GpioInterfaceTypeRegister() { }
    }
}
