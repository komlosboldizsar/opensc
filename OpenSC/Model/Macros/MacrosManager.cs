using OpenSC.GUI.Macros;
using OpenSC.GUI.Menus;
using OpenSC.GUI.Variables;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Persistence;
using OpenSC.Model.Settings;
using System;

namespace OpenSC.Model.Macros
{

    public class MacrosManager
    {

        public static void Init()
        {
            // TODO: shouldn't register here
            MacroCommandRegister.Instance.RegisterCommand(new RunMacroMacroCommand());
            SettingsManager.Instance.RegisterSetting(Macro.MaxStackDepthSetting);
            SettingsManager.Instance.RegisterSetting(MacroOscServer.PortSetting);
            MacroOscServer.Instance.Start();
        }

        public static void RegisterDatabases()
        {
            
            DatabasePersister<Macro>.RegisterSerializer(new MacroCommandWithArgumentsXmlSerializer()); // TODO: shouldn't register here
            DatabasePersister<Macro>.RegisterSerializer(new MacroTriggerWithArgumentsXmlSerializer()); // TODO: shouldn't register here
            DatabasePersister<MacroPanel>.RegisterSerializer(new MacroPanelElementXmlSerializer()); // TODO: shouldn't register here
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(MacroDatabase));
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(MacroPanelDatabase));
            Init(); // TODO: shouldn't do from this method
        }

        public static void RegisterWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType(typeof(MacroList));
            WindowTypeRegister.RegisterWindowType(typeof(MacroPanelList));
            WindowTypeRegister.RegisterWindowType(typeof(MacroPanelForm));
        }

        public static void RegisterMenus()
        {
            var macrosMenu = MenuManager.Instance.TopMenu["Macros"];
            var listMacrosMenu = macrosMenu["Macros list"];
            listMacrosMenu.ClickHandler = (menu, tag) => new MacroList().ShowAsChild();
            var listMacroPanelsMenu = macrosMenu["Macro panels list"];
            listMacroPanelsMenu.ClickHandler = (menu, tag) => new MacroPanelList().ShowAsChild();
        }

    }

}
