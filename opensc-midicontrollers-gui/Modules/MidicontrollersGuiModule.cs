using OpenSC.GUI.Menus;
using OpenSC.GUI.MidiControllers;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("midicontrollers-gui", "MIDI controllers (GUI)", "TODO")]
    [DependsOnModule(typeof(MidicontrollersModelModule))]
    public class MidicontrollersGuiModule : BasetypeGuiModuleBase<MidicontrollersModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<MidiControllerList>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "MIDI";
        public const string MENUGROUP_ID = MenuManager.GROUP_ID_BASE;
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE;
        public const string SUBMENU_LABEL = "MIDI controller list";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_LABEL].ClickHandler = (menu, tag) => new MidiControllerList().ShowAsChild();
        }

    }

}
