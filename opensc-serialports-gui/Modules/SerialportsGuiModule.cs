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

    }

}
