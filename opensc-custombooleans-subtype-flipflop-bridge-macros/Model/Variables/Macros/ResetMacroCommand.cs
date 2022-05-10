using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables.Macros
{
    [MacroCommand("CustomBooleans.FlipFlop.Rest", "Reset a flip-flop.", "Change the value of a flip-flop boolean to 0/off.")]
    public class ResetMacroCommand : FlipFlopMacroCommandBase
    {
        protected override void changeState(FlipFlopBoolean flipFlop) => flipFlop.Reset();
    }
}
