using OpenSC.Model;
using OpenSC.Model.SerialPorts;

namespace OpenSC.Modules
{

    [Module("serialports-model", "Serial ports (model)", "TODO")]
    public class SerialportsModelModule : BasetypeModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(SerialPortDatabase));
        }

    }

}
