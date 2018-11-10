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
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(SignalDatabases.SignalCategoryDatabase));
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(SignalDatabases.SignalDatabase));
        }

        public static void RegisterWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType(typeof(SignalList));
            WindowTypeRegister.RegisterWindowType(typeof(SignalCategoryList));
        }

        public static void RegisterMenus()
        {
            var signalsMenu = MenuManager.Instance.TopMenu["Signals"];
            var signalsSubMenu = signalsMenu["Signals"];
            signalsSubMenu.ClickHandler = (menu, tag) => new SignalList().ShowAsChild();
            var categoriesSubMenu = signalsMenu["Categories"];
            categoriesSubMenu.ClickHandler = (menu, tag) => new SignalCategoryList().ShowAsChild();
        }

    }

}
