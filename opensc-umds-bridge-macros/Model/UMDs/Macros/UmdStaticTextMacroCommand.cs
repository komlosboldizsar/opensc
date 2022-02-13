using OpenSC.Model.Macros;
using OpenSC.Model.UMDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.Macros
{

    [MacroCommand("UMDs.StaticText", "Set an UMD to static mode", "Set the static text of an UMD and switch to static mode.")]
    public class UmdStaticTextMacroCommand : MacroCommandBase
    {

        protected override void _run(object[] argumentValues)
        {
            UMD umd = argumentValues[0] as UMD;
            if (umd == null)
                return;
            string text = argumentValues[1] as string;
            if (text == null)
                return;
            umd.StaticText = text;
            umd.UseStaticText = true;
        }

        [MacroCommandArgument(0, "UMD", "The UMD to change mode and text.")]
        public class Arg0 : MacroCommandArgumentDatabaseItem<UMD>
        {
            public Arg0() : base(UmdDatabase.Instance)
            { }
        }

        [MacroCommandArgument(1, "Text", "Text to display on the UMD.")]
        public class Arg1 : MacroCommandArgumentString
        { }


    }

}
