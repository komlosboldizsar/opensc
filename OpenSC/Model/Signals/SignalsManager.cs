using OpenSC.GUI.Menus;
using OpenSC.GUI.Signals;
using OpenSC.GUI.WorkspaceManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    class SignalsManager
    {

        public static void RegisterDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(ExternalSignalDatabases.ExternalSignalCategoryDatabase));
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(ExternalSignalDatabases.ExternalSignalDatabase));
        }

        public static void RegisterWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType(typeof(SignalList));
            WindowTypeRegister.RegisterWindowType(typeof(ExternalSignalList));
            WindowTypeRegister.RegisterWindowType(typeof(ExternalSignalCategoryList));
        }

        public static void RegisterMenus()
        {
            var signalsMenu = MenuManager.Instance.TopMenu["Signals"];
            var allSignalsSubMenu = signalsMenu["All signals"];
            allSignalsSubMenu.ClickHandler = (menu, tag) => new SignalList().ShowAsChild();
            var externalSignalsSubMenu = signalsMenu["External signals"];
            externalSignalsSubMenu.ClickHandler = (menu, tag) => new ExternalSignalList().ShowAsChild();
            var externalCategoriesSubMenu = signalsMenu["External Categories"];
            externalCategoriesSubMenu.ClickHandler = (menu, tag) => new ExternalSignalCategoryList().ShowAsChild();
        }

    }

}
