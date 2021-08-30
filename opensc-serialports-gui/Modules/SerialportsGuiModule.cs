using OpenSC.GUI.Menus;
using OpenSC.GUI.SerialPorts;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("serialports-gui", "Serial ports (GUI)", "TODO")]
    [DependsOnModule(typeof(SerialportsModelModule))]
    public class SerialportsGuiModule : BasetypeGuiModuleBase<SerialportsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<SerialPortList>();
        }

        protected override void registerMenus()
        {
            var portsMenu = MenuManager.Instance.TopMenu["Ports"];
            var serialPortsMenu = portsMenu["Serial ports"];
            serialPortsMenu.ClickHandler = (menu, tag) => new SerialPortList().ShowAsChild();
        }

    }

}
