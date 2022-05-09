using OpenSC.Model.GpioInterfaces;

namespace OpenSC.GUI.GpioInterfaces
{
    public class GpioInterfaceEditorFormTypeRegister : ModelEditorFormTypeRegister<GpioInterface>
    {
        public static ModelEditorFormTypeRegister<GpioInterface> Instance { get; } = new GpioInterfaceEditorFormTypeRegister();
        private GpioInterfaceEditorFormTypeRegister() { }
    }
}
