using OpenSC.Model;
using OpenSC.Model.Midi;
using OpenSC.Model.Persistence;

namespace OpenSC.Modules
{

    [Module("midi-model", "Midi (model)", "TODO")]
    public class MidiModelModule : BasetypeModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(MidiControllerDatabase));
        }

        protected override void registerSerializers()
        {
        }

    }

}
