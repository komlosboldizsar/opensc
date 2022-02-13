using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{

    [MacroCommand("Macros.Run", "Run a macro", "Call and execute another macro")]
    public class RunMacroMacroCommand : MacroCommandBase
    {

        protected override void _run(object[] argumentValues) => (argumentValues[0] as Macro)?.Run();

        [MacroCommandArgument(0, "Macro", "The macro to execute.")]
        public class Arg0 : MacroCommandArgumentDatabaseItem<Macro>
        {
            public Arg0() : base(MacroDatabase.Instance)
            { }
        }

    }

}
