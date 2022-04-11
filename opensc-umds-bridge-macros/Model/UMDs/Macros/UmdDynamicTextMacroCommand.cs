using OpenSC.Model.Macros;
using OpenSC.Model.UMDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.Macros
{

    [MacroCommand("UMDs.DynamicText", "Set an UMD to dynamic mode", "Switch an UMD to dynamic mode without changing static text.")]
    public class UmdDynamicTextMacroCommand : MacroCommandBase
    {

        protected override void _run(object[] argumentValues)
        {
            Umd umd = argumentValues[0] as Umd;
            if (umd == null)
                return;
            umd.UseFullStaticText = false;
        }

        [MacroCommandArgument(0, "UMD", "The UMD to change mode and text.")]
        public class Arg0 : MacroCommandArgumentDatabaseItem<Umd>
        {
            public Arg0() : base(UmdDatabase.Instance)
            { }
        }

    }

}
