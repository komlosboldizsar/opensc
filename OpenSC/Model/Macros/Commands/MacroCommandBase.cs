using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{
    abstract public class MacroCommandBase : IMacroCommand
    {

        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public MacroCommandBase()
        {
            MacroCommandAttribute myAttribute = GetType().GetAttribute<MacroCommandAttribute>();
            if (myAttribute == null)
                throw new Exception(); //NoMacroCommandAttributeException();
            Code = myAttribute.Code;
            Name = myAttribute.Name;
            Description = myAttribute.Description;
            lookupForArguments();
        }

        private void lookupForArguments()
        {
            List<IMacroCommandArgument> arguments = new List<IMacroCommandArgument>();
            Type[] nestedTypes = GetType().GetNestedTypes();
            foreach (Type nestedType in nestedTypes)
            {
                if (nestedType.IsSubclassOf(typeof(IMacroCommandArgument)))
                {
                    IMacroCommandArgument argument = (IMacroCommandArgument)Activator.CreateInstance(nestedType);
                    arguments.Add(argument);
                }
            }
            arguments.OrderBy(arg => arg.Index);
            int i = 0;
            foreach (IMacroCommandArgument argument in arguments)
                if (argument.Index != i++)
                    throw new Exception(); // ArgumentIndexMismatchException();
            Arguments = arguments.ToArray();
        }

        public IMacroCommandArgument[] Arguments { get; private set; }

        public virtual string[] GetArgumentKeys(object[] argumentObjects)
        {
            string[] argumentKeys = new string[Arguments.Length];
            int i = 0;
            foreach (IMacroCommandArgument argument in Arguments)
                argumentKeys[i] = argument.GetKeyByObject(argumentObjects[i++]);
            return argumentKeys;
        }

        public virtual object[] GetArgumentsByKeys(string[] argumentKeys)
        {
            object[] argumentObjects = new object[Arguments.Length];
            int i = 0;
            foreach (IMacroCommandArgument argument in Arguments)
                argumentObjects[i] = argument.GetObjectByKey(argumentKeys[i++], argumentObjects);
            return argumentObjects;
        }

        public virtual MacroCommandWithArguments GetWithArgumentsByKeys(string[] argumentKeys)
            => new MacroCommandWithArguments(this, argumentKeys, true);

        public virtual MacroCommandWithArguments GetWithArgumentsByKeysConvertImmediately(string[] argumentKeys)
            => new MacroCommandWithArguments(this, GetArgumentsByKeys(argumentKeys));

        public virtual MacroCommandWithArguments GetWithArguments(object[] argumentValues)
            => new MacroCommandWithArguments(this, argumentValues);

        public virtual void Run(object[] argumentObjects)
        {
            if (argumentObjects.Length != Arguments.Length)
                return;
            _run(argumentObjects);
        }

        protected virtual void _run(object[] argumentValues)
        { }

    }
}
