using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables.Macros
{
    [MacroCommand("CustomBooleans.FlipFlop.Toggle", "Toggle a flip-flop.", "Change the value of a flip-flop boolean to the inverse of the current.")]
    public class ToggleMacroCommand : FlipFlopMacroCommandBase
    {
        protected override void changeState(FlipFlopBoolean flipFlop) => flipFlop.Toggle();
    }
}
