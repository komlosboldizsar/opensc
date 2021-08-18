using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{

    public class DynamicTextFunctionRegister
    {

        public static DynamicTextFunctionRegister Instance { get; } = new DynamicTextFunctionRegister();

        private DynamicTextFunctionRegister()
        { }

        private Dictionary<string, IDynamicTextFunction> registeredFunctions = new Dictionary<string, IDynamicTextFunction>();

        public IReadOnlyList<IDynamicTextFunction> RegisteredFunctions
        {
            get => registeredFunctions.Values.ToList();
        }

        public void RegisterFunction(IDynamicTextFunction function)
        {
            string functionName = function.Name;
            if (registeredFunctions.ContainsKey(functionName))
                throw new Exception();
            registeredFunctions[functionName] = function;
        }

        public IDynamicTextFunction GetFunction(string name)
        {
            if (!registeredFunctions.TryGetValue(name, out IDynamicTextFunction function))
                return null;
            return function;
        }

        public IDynamicTextFunctionSubstitute GetFunctionSubstitute(string name, object[] arguments)
        {
            if (!registeredFunctions.TryGetValue(name, out IDynamicTextFunction function))
                return null;
            return function.GetSubstitute(arguments);
        }

    }

}
