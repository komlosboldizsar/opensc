using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{
    public class MacroCodeInterpreter
    {

        private MacroCodeTokenizer tokenizer = new MacroCodeTokenizer();

        public string Formula
        {
            get => tokenizer.Formula;
            set
            {
                tokenizer.Formula = value;
            }
        }

        public string CommandCode => tokenizer.CommandCode;

        public IReadOnlyList<MacroCodeTokenizer.Token> Tokens => tokenizer.Tokens;

        public IReadOnlyList<string> ArgumentKeys
        {
            get
            {
                List<string> argKeys = new List<string>();
                foreach (var argument in tokenizer.Arguments)
                    argKeys.Add(argument.StrValue);
                return argKeys;
            }
        }

        public bool IsEmpty => tokenizer.IsEmpty;

        public bool IsComplete => tokenizer.IsComplete;

        public bool HasSyntaxError => tokenizer.HasSyntaxError;

        public int SyntaxErrorPosition => tokenizer.SyntaxErrorPosition;

        public bool ArgumentCountMismatch
        {
            get
            {
                IMacroCommand command = MacroCommandRegister.Instance.GetCommand(tokenizer.CommandCode);
                if (command == null)
                    return false;
                return (tokenizer.Arguments.Count != command.Arguments.Length);
            }
        }

        public bool[] ArgumentTypeMatches
        {
            get
            {
                IMacroCommand command = MacroCommandRegister.Instance.GetCommand(tokenizer.CommandCode);
                if (command == null)
                    return new bool[] { };
                int cmdArgCount = command.Arguments.Length;
                int interpretedArgCount = tokenizer.Arguments.Count;
                int resultSize = (interpretedArgCount < cmdArgCount) ? interpretedArgCount: cmdArgCount;
                bool[] result = new bool[resultSize];
                for (int i = 0; i < resultSize; i++)
                    result[i] = argumentTypeSuitable(command.Arguments[i].KeyType, tokenizer.Arguments[i].Type);
                return result;
            }
        }

        private static bool argumentTypeSuitable(MacroArgumentKeyType commandArg, MacroArgumentKeyType interpretedArg)
        {
            if (commandArg == interpretedArg)
                return true;
            if ((commandArg == MacroArgumentKeyType.Float) && (interpretedArg == MacroArgumentKeyType.Integer))
                return true;
            return false;
        }

        public bool CommandExists
            => (MacroCommandRegister.Instance.GetCommand(tokenizer.CommandCode) != null);

        public IMacroCommand GetCommand()
        {
            IMacroCommand command = MacroCommandRegister.Instance.GetCommand(tokenizer.CommandCode);
            if (command == null)
                throw new Exception("Macro command does not exist!");
            return command;
        }
        public MacroCommandWithArguments GetCommandWithArguments()
        {
            if (!IsComplete)
                throw new Exception("Command line is not complete!");
            return GetCommand().GetWithArguments(ArgumentKeys.ToArray());
        }


    }
}
