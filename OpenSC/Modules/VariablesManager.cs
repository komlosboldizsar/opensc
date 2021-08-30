using OpenSC.GUI.Menus;
using OpenSC.GUI.Variables;
using OpenSC.GUI.WorkspaceManager;
using System;

namespace OpenSC.Model.Variables
{

    public class VariablesManager
    {

        public static void RegisterDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(DynamicTextDatabase));
        }

        public static void RegisterWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType(typeof(DynamicTextList));
        }

        public static void RegisterMenus()
        {
            var variablesMenu = MenuManager.Instance.TopMenu["Variables"];
            var dynamicTextsMenu = variablesMenu["Dynamic texts"];
            dynamicTextsMenu.ClickHandler = (menu, tag) => new DynamicTextList().ShowAsChild();
            var booleansMenu = variablesMenu["Booleans"];
            booleansMenu.ClickHandler = (menu, tag) => new BooleanList().ShowAsChild();
        }

    }

}
