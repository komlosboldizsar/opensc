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

        public MenuItem TopMenu = new MenuItem(null, null, null, null);

    }
}
