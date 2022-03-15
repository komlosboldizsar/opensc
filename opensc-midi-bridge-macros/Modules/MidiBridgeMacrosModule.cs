using OpenSC.Model;
using OpenSC.Model.Macros;
//using OpenSC.Model.Midi.Triggers;

namespace OpenSC.Modules
{

    [Module("midi-bridge-macros", "Midi (bridge to macros)", "TODO")]
    [DependsOnModule(typeof(MidiModelModule))]
    public class MidiBridgeMacrosModule : MacrosBridgeModuleBase<MidiModelModule>
    {

        protected override void registerMacroCommands()
        { }

        protected override void registerTriggerTypes()
        { }

    }

}
