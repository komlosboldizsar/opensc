using OpenSC.GUI.Menus;
using OpenSC.GUI.VTRs;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("vtrs-gui", "VTRs (GUI)", "TODO")]
    [DependsOnModule(typeof(VtrsModelModule))]
    public class VtrsGuiModule : BasetypeGuiModuleBase<VtrsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<VtrList>();
        }

        protected override void registerMenus()
        {
            var vtrsMenu = MenuManager.Instance.TopMenu["VTR"]["VTR list"];
            vtrsMenu.ClickHandler = (menu, tag) => new VtrList().ShowAsChild();
        }

    }

}
