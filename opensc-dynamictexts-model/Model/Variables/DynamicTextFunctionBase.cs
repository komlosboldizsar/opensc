using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{
    public class DynamicTextFunctionBase<TSubstitue> : IDynamicTextFunction
        where TSubstitue : IDynamicTextFunctionSubstitute, new()
    {

        public string Name { get; private set; }
        public string Description { get; private set; }

        public DynamicTextFunctionBase()
        {
            DynamicTextFunctionAttribute myAttribute = GetType().GetAttribute<DynamicTextFunctionAttribute>();
            if (myAttribute == null)
                throw new Exception(); //NoDynamicTextFunctionAttributeException();
            Name = myAttribute.Name;
            Description = myAttribute.Description;
            lookupForArguments();
        }

        private void lookupForArguments()
        {
            List<IDynamicTextFunctionArgument> arguments = new List<IDynamicTextFunctionArgument>();
            Type[] nestedTypes = GetType().GetNestedTypes();
            foreach (Type nestedType in nestedTypes)
            {
                if (typeof(IDynamicTextFunctionArgument).IsAssignableFrom(nestedType))
                {
                    IDynamicTextFunctionArgument argument = (IDynamicTextFunctionArgument)Activator.CreateInstance(nestedType);
                    arguments.Add(argument);
                }
            }
            IEnumerable<IDynamicTextFunctionArgument> argumentsOrdered = arguments.OrderBy(arg => arg.Index);
            int i = 0;
            foreach (IDynamicTextFunctionArgument argument in argumentsOrdered)
                if (argument.Index != i++)
                    throw new Exception(); // ArgumentIndexMismatchException();
            Arguments = argumentsOrdered.ToArray();
        }

        public int ArgumentCount => Arguments.Count();

        public IDynamicTextFunctionArgument[] Arguments { get; private set; }

        public virtual object[] GetArgumentsByKeys(object[] argumentKeys)
        {
            object[] argumentObjects = new object[Arguments.Length];
            int i = 0;
            foreach (IDynamicTextFunctionArgument argument in Arguments)
                argumentObjects[i] = argument.GetObjectByKey(argumentKeys[i++], argumentObjects);
            return argumentObjects;
        }

        public IDynamicTextFunctionSubstitute GetSubstitute(object[] argumentKeys)
        {
            if (argumentKeys.Length != Arguments.Length)
                return null;
            TSubstitue substitue = new TSubstitue();
            object[] argumentObjects = GetArgumentsByKeys(argumentKeys);
            substitue.Init(argumentObjects);
            return substitue;
        }

    }
}
