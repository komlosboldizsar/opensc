using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{
    abstract public class MacroTriggerBase : IMacroTrigger
    {
        public string Code { get; protected set; }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public delegate string HumanReadableMethodDelegate(object[] argumentsValues);
        private HumanReadableMethodDelegate humanReadableMethod;

        public MacroTriggerBase(string code, string name, string description, HumanReadableMethodDelegate humanReadableMethod)
        {
            this.Code = code;
            this.Name = name;
            this.Description = description;
            this.humanReadableMethod = humanReadableMethod;
        }

        #region Arguments
        public int ArgumentCount => arguments.Count;

        public IMacroTriggerArgument[] Arguments => arguments.ToArray();

        protected List<IMacroTriggerArgument> arguments = new List<IMacroTriggerArgument>();

        public delegate object[] GetPossibilitiesMethodDelegate(object[] previousArgumentValues);

        public delegate string GetStringForPossibilityMethodDelegate(object items);

        protected void addArgument(string name, string description, Type type, GetPossibilitiesMethodDelegate getPossibilitiesMethod, GetStringForPossibilityMethodDelegate getStringForPossibilityMethod)
            => arguments.Add(new ArgumentImplementation(arguments.Count, name, description, type, getPossibilitiesMethod, getStringForPossibilityMethod));

        private class ArgumentImplementation : IMacroTriggerArgument
        {

            private int index;
            
            public string Name { get; private set; }

            public string Description { get; private set; }

            public Type Type { get; private set; }

            private GetPossibilitiesMethodDelegate getPossibilitiesMethod;

            private GetStringForPossibilityMethodDelegate getStringForPossibilityMethod;

            public ArgumentImplementation(int index, string name, string description, Type type, GetPossibilitiesMethodDelegate getPossibilitiesMethod, GetStringForPossibilityMethodDelegate getStringForPossibilityMethod)
            {
                this.index = index;
                this.Name = name;
                this.Description = description;
                this.Type = type;
                this.getPossibilitiesMethod = getPossibilitiesMethod;
                this.getStringForPossibilityMethod = getStringForPossibilityMethod;
            }
            public object[] GetPossibilities(object[] previousArgumentValues)
                => getPossibilitiesMethod(previousArgumentValues);

            public string GetStringForPossibility(object item)
                => getStringForPossibilityMethod(item);

        }
        #endregion

        public virtual string[] GetArgumentKeys(object[] arguments)
        {
            List<string> argumentKeys = new List<string>();
            int i = 0;
            foreach (object argument in arguments)
                argumentKeys.Add(getArgumentKey(i++, argument));
            return argumentKeys.ToArray();
        }

        protected virtual string getArgumentKey(int index, object value)
        {
            ModelBase argumentAsModel = value as ModelBase;
            if (argumentAsModel != null)
                return argumentAsModel.ID.ToString();
            return value?.ToString();
        }

        public abstract object[] GetArgumentsByKeys(string[] keys);

        public virtual MacroTriggerWithArguments GetWithArgumentsByKeys(string[] argumentKeys)
        {
            return new MacroTriggerWithArguments(this, argumentKeys, true);
        }

        public virtual MacroTriggerWithArguments GetWithArguments(object[] argumentValues)
        {
            return new MacroTriggerWithArguments(this, argumentValues);
        }

        protected List<MacroTriggerWithArguments> registeredTriggersWithArguments = new List<MacroTriggerWithArguments>();
        
        public virtual void Register(MacroTriggerWithArguments triggerWithArguments)
        {
            if (!registeredTriggersWithArguments.Contains(triggerWithArguments))
                registeredTriggersWithArguments.Add(triggerWithArguments);
        }

        public virtual void Unregister(MacroTriggerWithArguments triggerWithArguments)
        {
            registeredTriggersWithArguments.Remove(triggerWithArguments);
        }

        public abstract void Call(params object[] arguments);

        public string HumanReadable(object[] argumentsValues)
            => humanReadableMethod(argumentsValues);

    }
}
