using OpenSC.GUI.Macros;
using OpenSC.GUI.Menus;
using OpenSC.GUI.Variables;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Persistence;
using System;

namespace OpenSC.Model.Macros
{

    public class MacrosManager
    {

        public static void RegisterDatabases()
        {
            DatabasePersister<Macro>.RegisterSerializer(new MacroCommandWithArgumentsXmlSerializer()); // TODO: shouldn't register here
            DatabasePersister<MacroPanel>.RegisterSerializer(new MacroPanelElementXmlSerializer()); // TODO: shouldn't register here
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(MacroDatabase));
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(MacroPanelDatabase));
        }

        public static void RegisterWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType(typeof(MacroList));
        }

        public static void RegisterMenus()
        {
            var macrosMenu = MenuManager.Instance.TopMenu["Macros"];
            var listMacrosMenu = macrosMenu["Macros list"];
            listMacrosMenu.ClickHandler = (menu, tag) => new MacroList().ShowAsChild();
        }

    }

}
