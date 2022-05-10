using OpenSC.GUI.GpioInterfaces;
using OpenSC.Model.GpioInterfaces.BlackMagicDesign;

namespace OpenSC.Modules
{

    [Module("gpiointerfaces-subtype-bmdtallybox-gui", "GPIO intefaces / BlackMagic Design Tally Box (GUI)", "TODO")]
    [DependsOnModule(typeof(GpioInterfacesSubtypeBmdtallyboxModelModule))]
    public class GpiointerfacesSubtypeBmdtallyboxGuiModule : SubtypeGuiModuleBase<GpioInterfacesSubtypeBmdtallyboxModelModule>
    {

        protected override void registerPersistableWindowTypes()
        { }

        protected override void registerSubtypeEditorWindowTypes()
        {
            GpioInterfaceEditorFormTypeRegister.Instance.RegisterFormType<BmdTallyBox, BmdTallyBoxEditorForm>();
        }

    }

}
