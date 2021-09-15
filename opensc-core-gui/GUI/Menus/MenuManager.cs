using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.Menus
{
    public class MenuManager
    {
        public static MenuManager Instance { get; } = new MenuManager();
        public MenuItem TopMenu = new MenuItem(null, null, null, null, null);
        public const string GROUP_ID_MODULES = "modules";
        public const string GROUP_ID_BASE = "base";
        public const int GROUP_WEIGHT_BASE = 50;
    }
}
