using OpenSC.GUI.Macros;
using OpenSC.GUI.Menus;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("macros-gui", "Macros (GUI)", "TODO")]
    [DependsOnModule(typeof(MacrosModelModule))]
    public class MacrosGuiModule : BasetypeGuiModuleBase<MacrosModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<MacroList>();
        }

        protected override void registerMenus()
        {
            var macrosMenu = MenuManager.Instance.TopMenu["Macros"];
            var listMacrosMenu = macrosMenu["Macros list"];
            listMacrosMenu.ClickHandler = (menu, tag) => new MacroList().ShowAsChild();
        }

    }

}
