using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{

    public static class MacroCommandWithArgumentsToCodeSerializer
    {

        public static string GetCode(this MacroCommandWithArguments commandWA)
        {
            return GetCode(commandWA.Command, commandWA.ArgumentValues);
        }

        public static string GetCode(this IMacroCommand command, IEnumerable<object> argumentValues)
        {
            StringBuilder code = new StringBuilder();
            code.Append(command.CommandCode);
            code.Append("(");
            string[] argKeys = command.GetArgumentKeys(argumentValues.ToArray());
            int argCount = command.Arguments.Length;
            for (int i = 0; i < argCount; i++)
            {
                bool isString = (command.Arguments[i].KeyType == MacroArgumentKeyType.String);
                string argKey = string.Format("{1}{0}{1}", argKeys[i], isString ? "\"" : "");
                code.Append(argKey);
                if (i < (argCount - 1))
                    code.Append(", ");
            }
            code.Append(")");
            return code.ToString();
        }

    }

}
