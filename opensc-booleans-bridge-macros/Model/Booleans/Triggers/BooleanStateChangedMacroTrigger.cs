using OpenSC.Model.General;
using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables.Triggers
{

    [MacroTrigger("Booleans.StateChanged", "State (on/off) change of a boolean", "Observe a boolean for changing state from false (not active) to true (active) or from true to false.")]
    class BooleanStateChangedMacroTrigger : MacroTriggerBase<BooleanStateChangedMacroTrigger.ActivationData>
    {

        [MacroTriggerArgument(0, "Boolean", "The boolean to observe.")]
        public class Arg0 : MacroTriggerArgumentRegisterItem<string, IBoolean>
        {
            public Arg0() : base(BooleanRegister.Instance) { }
            protected override string StringKeyToTypedKey(string stringKey) => stringKey;
        }

        [MacroTriggerArgument(1, "Observe off", "Fire trigger when note state is changed to not active (false).")]
        public class Arg1 : MacroTriggerArgumentBool
        { }

        [MacroTriggerArgument(2, "Observe on", "Fire trigger when boolean state is changed to active (true).")]
        public class Arg2 : MacroTriggerArgumentBool
        { }

        internal class ActivationData : MacroTriggerWithArgumentsActivationData
        {
            public IBoolean Boolean { get; private set; }
            public PropertyChangedTwoValuesDelegate<IBoolean, bool> StateChangedHandler { get; private set; }
            public ActivationData(IBoolean boolean, PropertyChangedTwoValuesDelegate<IBoolean, bool> stateChangedHandler)
            {
                Boolean = boolean;
                StateChangedHandler = stateChangedHandler;
            }
        }

        protected override void _activate(MacroTriggerWithArguments triggerWithArguments)
        {
            object[] argumentObjects = triggerWithArguments.ArgumentObjects;
            IBoolean boolean = argumentObjects[0] as IBoolean;
            if (boolean == null)
                return;
            bool observeOff = (bool)argumentObjects[1];
            bool observeOn = (bool)argumentObjects[2];
            PropertyChangedTwoValuesDelegate<IBoolean, bool> stateChangedHandler = (i, ov, nv) => {
                if ((observeOff && !nv) || (observeOn && nv))
                    triggerWithArguments.Fire();
            };
            boolean.StateChanged += stateChangedHandler;
            ActivationData activationData = new ActivationData(boolean, stateChangedHandler);
            triggerWithArguments.Activated(activationData);
        }

        protected override void _deactivate(MacroTriggerWithArguments triggerWithArguments, ActivationData activationData)
        {
            activationData.Boolean.StateChanged -= activationData.StateChangedHandler;
            triggerWithArguments.Deactivated();
        }

        protected override string _humanReadable(object[] argumentObjects)
        {
            IBoolean boolean = argumentObjects[0] as IBoolean;
            if (boolean == null)
                return null;
            int observeOff = ((bool)argumentObjects[1] ? 1 : 0);
            int observeOn = ((bool)argumentObjects[2] ? 2 : 0);
            string observedState = (observeOff + observeOn) switch
            {
                0 => "-",
                1 => "off",
                2 => "on",
                3 => "any",
                _ => "??"
            };
            return string.Format("State changes of boolean [{0}] to [{1}].", boolean, observedState);
        }

    }

}