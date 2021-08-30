using OpenSC.GUI.Menus;
using OpenSC.GUI.Routers.CrosspointBooleans;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("crosspointbooleans-gui", "Crosspoint booleans (GUI)", "TODO")]
    [DependsOnModule(typeof(CrosspointbooleansModelModule))]
    public class CrosspointbooleansGuiModule : BasetypeGuiModuleBase<CrosspointbooleansModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<CrosspointBooleanList>();
        }

        protected override void registerMenus()
        {
            var routersMenu = MenuManager.Instance.TopMenu["Routers"];
            var crosspointBooleansListMenu = routersMenu["Crosspoint boolean list"];
            crosspointBooleansListMenu.ClickHandler = (menu, tag) => new CrosspointBooleanList().ShowAsChild();
        }

    }

}
