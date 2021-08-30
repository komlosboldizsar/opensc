using OpenSC.GUI.Menus;
using OpenSC.GUI.Mixers;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("mixers-gui", "Mixers (GUI)", "TODO")]
    [DependsOnModule(typeof(MixersModelModule))]
    public class MixersGuiModule : BasetypeGuiModuleBase<MixersModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<MixerList>();
        }

        protected override void registerMenus()
        {
            var mixersMenu = MenuManager.Instance.TopMenu["Mixers"];
            var mixersListMenu = mixersMenu["Mixers list"];
            mixersListMenu.ClickHandler = (menu, tag) => new MixerList().ShowAsChild();
        }

    }

}
