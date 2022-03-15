using OpenSC.Model;
using OpenSC.Model.Macros;
using OpenSC.Model.MidiControllers.Triggers;

namespace OpenSC.Modules
{

    [Module("midicontrollers-bridge-macros", "MIDI controllers (bridge to macros)", "TODO")]
    [DependsOnModule(typeof(MidicontrollersModelModule))]
    public class MidiBridgeMacrosModule : MacrosBridgeModuleBase<MidicontrollersModelModule>
    {

        protected override void registerMacroCommands()
        { }

        protected override void registerTriggerTypes()
        {
            MacroTriggerRegister.Instance.RegisterTrigger(new MidiNoteStateChangedMacroTrigger());
        }

    }

}
