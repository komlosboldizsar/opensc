using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{
    abstract public class MacroTriggerBase<TActivationData> : IMacroTrigger
        where TActivationData : MacroTriggerWithArgumentsActivationData
    {

        public string Code { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public MacroTriggerBase()
        {
            MacroTriggerAttribute myAttribute = GetType().GetAttribute<MacroTriggerAttribute>();
            if (myAttribute == null)
                throw new Exception(); //NoMacroTriggerAttributeException();
            Code = myAttribute.Code;
            Name = myAttribute.Name;
            Description = myAttribute.Description;
            lookupForArguments();
        }

        #region Arguments
        private void lookupForArguments()
        {
            List<IMacroTriggerArgument> arguments = new List<IMacroTriggerArgument>();
            Type[] nestedTypes = GetType().GetNestedTypes();
            foreach (Type nestedType in nestedTypes)
            {
                if (nestedType.IsSubclassOf(typeof(IMacroTriggerArgument)))
                {
                    IMacroTriggerArgument argument = (IMacroTriggerArgument)Activator.CreateInstance(nestedType);
                    arguments.Add(argument);
                }
            }
            IEnumerable<IMacroTriggerArgument> argumentsOrdered = arguments.OrderBy(arg => arg.Index);
            int i = 0;
            foreach (IMacroTriggerArgument argument in argumentsOrdered)
                if (argument.Index != i++)
                    throw new Exception(); // ArgumentIndexMismatchException();
            Arguments = argumentsOrdered.ToArray();
        }

        public int ArgumentCount => Arguments.Length;

        public IMacroTriggerArgument[] Arguments { get; private set; }

        public virtual string[] GetArgumentKeys(object[] argumentObjects)
        {
            string[] argumentKeys = new string[Arguments.Length];
            int i = 0;
            foreach (IMacroTriggerArgument argument in Arguments)
                argumentKeys[i] = argument.GetKeyByObject(argumentObjects[i++]);
            return argumentKeys;
        }

        public virtual object[] GetArgumentsByKeys(string[] argumentKeys)
        {
            object[] argumentObjects = new object[Arguments.Length];
            int i = 0;
            foreach (IMacroTriggerArgument argument in Arguments)
                argumentObjects[i] = argument.GetObjectByKey(argumentKeys[i++], argumentObjects);
            return argumentObjects;
        }

        public virtual MacroTriggerWithArguments GetWithArgumentsByKeys(string[] argumentKeys)
            => new MacroTriggerWithArguments(this, argumentKeys, true);

        public virtual MacroTriggerWithArguments GetWithArguments(object[] argumentValues)
            => new MacroTriggerWithArguments(this, argumentValues);
        #endregion

        #region Activation
        public virtual void Activate(MacroTriggerWithArguments triggerWithArguments)
        {
            if (triggerWithArguments.Trigger != this)
                return;
            if (triggerWithArguments.ArgumentObjects.Length != Arguments.Length)
                return;
            _activate(triggerWithArguments);
        }

        protected abstract void _activate(MacroTriggerWithArguments triggerWithArguments);

        public virtual void Deactivate(MacroTriggerWithArguments triggerWithArguments)
        {
            if (triggerWithArguments.Trigger != this)
                return;
            TActivationData activationData = triggerWithArguments.ActivationData as TActivationData;
            if (activationData == null)
                return;
            _deactivate(triggerWithArguments, activationData);
        }

        protected abstract void _deactivate(MacroTriggerWithArguments triggerWithArguments, TActivationData activationData);
        #endregion

        #region Human readable
        private const string HUMAN_READABLE_ERROR = "???";

        public virtual string HumanReadable(object[] argumentObjects)
        {
            if (argumentObjects.Length != Arguments.Length)
                return HUMAN_READABLE_ERROR;
            return _humanReadable(argumentObjects) ?? HUMAN_READABLE_ERROR;
        }

        protected abstract string _humanReadable(object[] argumentObjects);
        #endregion

    }

}
