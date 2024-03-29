﻿using OpenSC.Model.Macros;
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
            Umd umd = argumentValues[0] as Umd;
            if (umd == null)
                return;
            string text = argumentValues[1] as string;
            if (text == null)
                return;
            umd.FullStaticText = text;
            umd.UseFullStaticText = true;
        }

        [MacroCommandArgument(0, "UMD", "The UMD to change mode and text.")]
        public class Arg0 : MacroCommandArgumentDatabaseItem<Umd>
        {
            public Arg0() : base(UmdDatabase.Instance)
            { }
        }

        [MacroCommandArgument(1, "Text", "Text to display on the UMD.")]
        public class Arg1 : MacroCommandArgumentString
        { }


    }

}
