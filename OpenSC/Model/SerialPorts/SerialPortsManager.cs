using OpenSC.GUI.Menus;
using OpenSC.GUI.Variables;
using OpenSC.GUI.WorkspaceManager;
using System;

namespace OpenSC.Model.SerialPorts
{

    public class SerialPortsManager
    {

        public static void RegisterDatabases()
        {
            //MasterDatabase.Instance.RegisterSingletonDatabase(typeof(SerialPortsDatabase));
        }

        public static void RegisterWindowTypes()
        {
            //WindowTypeRegister.RegisterWindowType(typeof(SerialPortsList));
        }

        public static void RegisterMenus()
        {
            /*var portsMenu = MenuManager.Instance.TopMenu["Ports"];
            var serialPortsMenu = portsMenu["Serial ports"];
            serialPortsMenu.ClickHandler = (menu, tag) => new SerialPortsList().ShowAsChild();*/
        }

    }

}
