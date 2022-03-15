using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Midi.Triggers
{

    [MacroTrigger("Midi.NoteStateChanged", "State (on/off) change of a note on a MIDI controller", "Observe only one note for changing state from off (not pressed) to on (pressed) or on to off.")]
    class MidiNoteStateChangedMacroTrigger : MacroTriggerBase<MidiNoteStateChangedMacroTrigger.ActivationData>
    {

        [MacroTriggerArgument(0, "Controller", "The MIDI controller to observe.")]
        public class Arg0 : MacroTriggerArgumentDatabaseItem<MidiController>
        {
            public Arg0() : base(MidiControllerDatabase.Instance)
            { }
        }

        [MacroTriggerArgument(1, "Note", "The numeric identifier of the note/pitch to observe. C4 is 60, A4 (440 Hz) is 69.")]
        public class Arg1 : MacroTriggerArgumentInt
        {
            public Arg1() : base(21, 108)
            { }
        }

        [MacroTriggerArgument(2, "Observe off", "Fire trigger when note state is changed to off.")]
        public class Arg2 : MacroTriggerArgumentBool
        { }

        [MacroTriggerArgument(3, "Observe on", "Fire trigger when note state is changed to on.")]
        public class Arg3 : MacroTriggerArgumentBool
        { }

        internal class ActivationData : MacroTriggerWithArgumentsActivationData
        {
            public MidiController Controller { get; private set; }
            public MidiController.NoteChangeDelegate NoteChangeHandler { get; private set; }
            public ActivationData(MidiController controller, MidiController.NoteChangeDelegate noteChangeHandler)
            {
                Controller = controller;
                NoteChangeHandler = noteChangeHandler;
            }
        }

        protected override void _activate(MacroTriggerWithArguments triggerWithArguments)
        {
            object[] argumentObjects = triggerWithArguments.ArgumentObjects;
            MidiController controller = argumentObjects[0] as MidiController;
            if (controller == null)
                return;
            bool observeOff = (bool)argumentObjects[2];
            bool observeOn = (bool)argumentObjects[3];
            MidiController.NoteChangeDelegate noteChangeHandler = (i, ov, nv) => {
                if ((observeOff && !nv) || (observeOn && nv))
                    triggerWithArguments.Fire();
            };
            controller.NoteChange += noteChangeHandler;
            ActivationData activationData = new ActivationData(controller, noteChangeHandler);
            triggerWithArguments.Activated(activationData);
        }

        protected override void _deactivate(MacroTriggerWithArguments triggerWithArguments, ActivationData activationData)
        {
            activationData.Controller.NoteChange -= activationData.NoteChangeHandler;
            triggerWithArguments.Deactivated();
        }

        protected override string _humanReadable(object[] argumentObjects)
        {
            MidiController controller = argumentObjects[0] as MidiController;
            if (controller == null)
                return null;
            int note = (int)argumentObjects[1];
            int observeOff = ((bool)argumentObjects[2] ? 1 : 0);
            int observeOn = ((bool)argumentObjects[3] ? 2 : 0);
            string observedState = (observeOff + observeOn) switch
            {
                0 => "-",
                1 => "off",
                2 => "on",
                3 => "any",
                _ => "??"
            };
            return string.Format("Note [{0}] changes on MIDI controller [{1}] to [{2}].", note, controller, observedState);
        }

    }

}
