using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables.Macros
{
    [MacroCommand("CustomBooleans.FlipFlop.Set", "Set a flip-flop.", "Change the value of a flip-flop boolean to 1/on.")]
    public class SetMacroCommand : FlipFlopMacroCommandBase
    {
        protected override void changeState(FlipFlopBoolean flipFlop) => flipFlop.Set();
    }
}
