using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables.Macros
{

    public abstract class FlipFlopMacroCommandBase : MacroCommandBase
    {

        protected override void _run(object[] argumentObjects)
        {
            FlipFlopBoolean flipFlop = argumentObjects[0] as FlipFlopBoolean;
            if (flipFlop == null)
                return;
            changeState(flipFlop);
        }

        protected abstract void changeState(FlipFlopBoolean flipFlop);

        [MacroCommandArgument(0, "FlipFlop", "The flip-flop to trigger/change state.")]
        public class Arg0 : MacroCommandArgumentDatabaseSubTypeItem<CustomBoolean, FlipFlopBoolean>
        {
            public Arg0() : base(CustomBooleanDatabase.Instance)
            { }
        }

    }

}
