using OpenSC.GUI.Menus;
using OpenSC.GUI.Midi;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("midi-gui", "Midi (GUI)", "TODO")]
    [DependsOnModule(typeof(MidiModelModule))]
    public class MidiGuiModule : BasetypeGuiModuleBase<MidiModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<MidiControllerList>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "MIDI";
        public const string MENUGROUP_ID = MenuManager.GROUP_ID_BASE;
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE;
        public const string SUBMENU_LABEL = "Midi controller list";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_LABEL].ClickHandler = (menu, tag) => new MidiControllerList().ShowAsChild();
        }

    }

}
