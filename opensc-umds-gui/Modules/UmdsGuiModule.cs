using OpenSC.GUI.Menus;
using OpenSC.GUI.UMDs;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("umds-gui", "UMDs (GUI)", "TODO")]
    [DependsOnModule(typeof(UmdsModelModule))]
    public class UmdsGuiModule : BasetypeGuiModuleBase<UmdsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<UmdList>();
        }

        protected override void registerMenus()
        {
            var umdsMenu = MenuManager.Instance.TopMenu["UMD"]["UMD list"];
            umdsMenu.ClickHandler = (menu, tag) => new UmdList().ShowAsChild();
        }

    }

}
