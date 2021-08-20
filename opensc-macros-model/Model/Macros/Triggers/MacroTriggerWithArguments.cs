using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{

    public class MacroTriggerWithArguments
    {

        public MacroTriggerWithArguments(IMacroTrigger trigger, string[] argumentKeys, bool byKeys)
        {
            this.Trigger = trigger;
            this.argumentKeys = argumentKeys;
        }

        public MacroTriggerWithArguments(IMacroTrigger trigger, object[] argumentValues)
        {
            this.Trigger = trigger;
            this.argumentObjects = argumentValues;
            Activate();
        }

        public void RestoreCustomRelations() => restoreArgumentObjects();

        public void TotallyRestored() => Activate();

        public IMacroTrigger Trigger { get; private set; }

        public string TriggerCode => Trigger?.Code;

        public Macro Macro { get; internal set; }

        private object[] argumentObjects;
        public object[] ArgumentObjects
        {
            get => argumentObjects;
            set
            {
                if (value.Length != Trigger.ArgumentCount)
                    throw new ArgumentException();
                argumentObjects = value;
            }
        }

        private string[] argumentKeys;
        public string[] ArgumentKeys => Trigger.GetArgumentKeys(ArgumentObjects);

        private void restoreArgumentObjects()
        {
            if (argumentKeys != null)
                argumentObjects = Trigger.GetArgumentsByKeys(argumentKeys);
        }

        public void Activate() => Trigger?.Activate(this);
        public void Dectivate() => Trigger?.Deactivate(this);
        internal object ActivationData { get; private set; }
        public void Activated(MacroTriggerWithArgumentsActivationData activationData) => ActivationData = activationData;
        public void Deactivated() => ActivationData = null;

        public string HumanReadable => Trigger?.HumanReadable(ArgumentObjects);

        public void Fire() => Macro?.Run();

    }

}
