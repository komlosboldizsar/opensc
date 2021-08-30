using OpenSC.GUI.Macros;
using OpenSC.GUI.Menus;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("macropanels-gui", "Macro panels (GUI)", "TODO")]
    [DependsOnModule(typeof(MacropanelsModelModule))]
    public class MacropanelsGuiModule : BasetypeGuiModuleBase<MacropanelsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<MacroPanelList>();
        }

        protected override void registerMenus()
        {
            var macrosMenu = MenuManager.Instance.TopMenu["Macros"];
            var listMacroPanelsMenu = macrosMenu["Macro panels list"];
            listMacroPanelsMenu.ClickHandler = (menu, tag) => new MacroPanelList().ShowAsChild();
        }

    }

}
