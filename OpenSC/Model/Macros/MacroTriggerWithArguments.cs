using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{
    public class MacroTriggerWithArguments
    {

        public IMacroTrigger Trigger { get; private set; }

        public string TriggerCode => Trigger?.Code;

        public Macro Macro { get; internal set; }

        private object[] argumentValues;
        public object[] ArgumentValues
        {
            get => argumentValues;
            set
            {
                if (value.Length != Trigger.ArgumentCount)
                    throw new ArgumentException();
                argumentValues = value;
            }
        }

        private string[] argumentKeys;

        public string[] ArgumentKeys
        {
            get => Trigger.GetArgumentKeys(ArgumentValues);
        }

        public string HumanReadable => Trigger?.HumanReadable(ArgumentValues);

        public MacroTriggerWithArguments(IMacroTrigger trigger, string[] argumentKeys, bool byKeys)
        {
            trigger.Register(this);
            this.Trigger = trigger;
            this.argumentKeys = argumentKeys;
        }

        public MacroTriggerWithArguments(IMacroTrigger trigger, object[] argumentValues)
        {
            trigger.Register(this);
            this.Trigger = trigger;
            this.argumentValues = argumentValues;
        }

        public void Restored()
        {
            restoreArgumentValues();
        }

        private void restoreArgumentValues()
        {
            if (argumentKeys != null)
                argumentValues = Trigger.GetArgumentsByKeys(argumentKeys);
        }

        public void Fire(IMacroTrigger sender)
        {
            if (sender != Trigger)
                return;
            Macro?.Triggered(this);
        }

    }
}
