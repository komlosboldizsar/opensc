using OpenSC.GUI.Menus;
using OpenSC.GUI.Signals;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("signals-gui", "Signals (GUI)", "TODO")]
    [DependsOnModule(typeof(SignalsModelModule))]
    public class SignalsGuiModule : BasetypeGuiModuleBase<SignalsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<SignalList>();
            WindowTypeRegister.RegisterWindowType<ExternalSignalList>();
            WindowTypeRegister.RegisterWindowType<ExternalSignalCategoryList>();
        }

        protected override void registerMenus()
        {
            var signalsMenu = MenuManager.Instance.TopMenu["Signals"];
            var allSignalsSubMenu = signalsMenu["All signals"];
            allSignalsSubMenu.ClickHandler = (menu, tag) => new SignalList().ShowAsChild();
            var externalSignalsSubMenu = signalsMenu["External signals"];
            externalSignalsSubMenu.ClickHandler = (menu, tag) => new ExternalSignalList().ShowAsChild();
            var externalCategoriesSubMenu = signalsMenu["External categories"];
            externalCategoriesSubMenu.ClickHandler = (menu, tag) => new ExternalSignalCategoryList().ShowAsChild();
        }

    }

}
