using OpenSC.GUI.Menus;
using OpenSC.GUI.Variables;
using OpenSC.GUI.WorkspaceManager;
using System;

namespace OpenSC.Model.Macros
{

    public class MacrosManager
    {

        public static void RegisterDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(MacroDatabase));
        }

        public static void RegisterWindowTypes()
        {
            //WindowTypeRegister.RegisterWindowType(typeof(DynamicTextList));
        }

        public static void RegisterMenus()
        {
            var macrosMenu = MenuManager.Instance.TopMenu["Macros"];
            /*var dynamicTextsMenu = variablesMenu["Dynamic texts"];
            dynamicTextsMenu.ClickHandler = (menu, tag) => new DynamicTextList().ShowAsChild();*/
        }

    }

}
