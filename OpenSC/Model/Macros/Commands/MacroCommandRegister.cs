using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{

    public class MacroCommandRegister
    {

        public static MacroCommandRegister Instance { get; } = new MacroCommandRegister();

        private MacroCommandRegister()
        { }

        private Dictionary<string, IMacroCommand> registeredCommands = new Dictionary<string, IMacroCommand>();

        public IReadOnlyList<IMacroCommand> RegisteredCommands
        {
            get => registeredCommands.Values.ToList();
        }

        public void RegisterCommand(IMacroCommand command)
        {
            string commandCode = command.CommandCode;
            if (registeredCommands.ContainsKey(commandCode))
                throw new Exception();
            registeredCommands[commandCode] = command;
        }

        public IMacroCommand GetCommand(string code)
        {
            if (!registeredCommands.TryGetValue(code, out IMacroCommand command))
                return null;
            return command;
        }

        public interface IMacroCommandCollection
        {
            IMacroCommand[] CommandsToRegister { get; }
        }

        public void RegisterCommandCollection(IMacroCommandCollection collection)
        {
            foreach (IMacroCommand command in collection.CommandsToRegister)
                RegisterCommand(command);
        }

    }

}
