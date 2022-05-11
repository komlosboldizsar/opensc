using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.X32Faders.Macros
{

    [MacroCommand("X32Faders.Do", "X32 fade", "Do a fade event for an X32 fader.")]
    public class DoMacroCommand : MacroCommandBase
    {

        protected override void _run(object[] argumentObjects) => (argumentObjects[0] as X32Fader)?.Do();

        [MacroCommandArgument(0, "Fader", "Fader settings to use")]
        public class Arg0 : MacroCommandArgumentDatabaseItem<X32Fader>
        {
            public Arg0() : base(X32FaderDatabase.Instance)
            { }
        }

    }

}
