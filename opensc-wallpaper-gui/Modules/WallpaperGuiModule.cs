using OpenSC.GUI.Menus;
using OpenSC.GUI.Wallpaper;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Settings;

namespace OpenSC.Modules
{

    [Module("wallpaper-gui", "Wallpaper (GUI)", "TODO")]
    [DependsOnModule(typeof(WallpaperGuiModule))]
    public class WallpaperGuiModule : GuiModuleBase<WallpaperGuiModule>
    {

        public override void Initialize()
        {
            base.Initialize();
            WallpaperManager.Instance.IntializeSettings();
            WallpaperManager.Instance.Initialize();
        }

        protected override void registerPersistableWindowTypes()
        { }

    }

}
