using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{
    abstract public class MacroCommandBase : IMacroCommand
    {
        public abstract string CommandCode { get; }

        public abstract string CommandName { get; }

        public abstract string Description { get; }

        public abstract IMacroCommandArgument[] Arguments { get; }

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

        public virtual MacroCommandWithArguments GetWithArgumentsByKeys(string[] argumentKeys)
        {
            return new MacroCommandWithArguments(this, argumentKeys, true);
        }

        public virtual MacroCommandWithArguments GetWithArgumentsByKeysConvertImmediately(string[] argumentKeys)
        {
            return new MacroCommandWithArguments(this, GetArgumentsByKeys(argumentKeys));
        }

        public virtual MacroCommandWithArguments GetWithArguments(object[] argumentValues)
        {
            return new MacroCommandWithArguments(this, argumentValues);
        }
        public abstract void Run(object[] argumentValues);

    }
}
