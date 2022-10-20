using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Salvos.Macros
{
    [MacroCommand("Salvos.Take", "Take/fire a salvo", "Do all of the stored crosspoint changes of a salvo.")]

    public class TakeSalvoMacroCommand : MacroCommandBase
    {

        public override void Run(object[] argumentValues) => (argumentValues[0] as Salvo)?.Take();

        [MacroCommandArgument(0, "Salvo", "The salvo to take/fire.")]
        public class Arg0 : MacroCommandArgumentDatabaseItem<Salvo>
        {
            public Arg0() : base(SalvoDatabase.Instance)
            { }
        }

    }

}
