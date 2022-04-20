using OpenSC.Model;
using OpenSC.Model.MidiControllers;
using OpenSC.Model.Persistence;

namespace OpenSC.Modules
{

    [Module("midicontrollers-model", "MIDI controllers (model)", "TODO")]
    public class MidicontrollersModelModule : BasetypeModuleBase
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
