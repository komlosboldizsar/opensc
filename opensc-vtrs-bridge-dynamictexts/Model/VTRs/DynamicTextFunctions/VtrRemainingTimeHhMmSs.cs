using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.VTRs.DynamicTextFunctions
{

    [DynamicTextFunction(nameof(VtrRemainingTimeHhMmSs), "HH:MM:SS format of  remaining time of a playout on a VTR.")]
    public class VtrRemainingTimeHhMmSs : DynamicTextFunctionBase<VtrRemainingTimeHhMmSs.Substitute>
    {

        [DynamicTextFunctionArgument(0, "ID of the VTR.")]
        public class Arg0 : DynamicTextFunctionArgumentDatabaseItem<Vtr>
        {
            public Arg0() : base(VtrDatabase.Instance)
            { }
        }

        [DynamicTextFunctionArgument(1, "Spaces around colons in formatted time string.")]
        public class Arg1 : DynamicTextFunctionArgumentInt
        {
            public Arg1() : base(0, 7)
            { }
        }

        public class Substitute : DynamicTextFunctionSubstituteBase
        {

            private Vtr vtr;
            private string colonReplacementWithSpaces;

            public override void Init(object[] argumentObjects)
            {
                colonReplacementWithSpaces = string.Format("{0}:{0}", new string(' ', (int)argumentObjects[1]));
                vtr = argumentObjects[0] as Vtr;
                if (vtr == null)
                {
                    CurrentValue = "??:??:??".Replace(":", colonReplacementWithSpaces);
                    return;
                }
                vtr.SecondsRemainingChanged += vtrSecondsRemainingChangedHandler;
                updateValue();
            }

            private void vtrSecondsRemainingChangedHandler(Vtr vtr, int oldValue, int newValue) => updateValue();

            private void updateValue()
            {
                string timeString = vtr.TimeRemaining.ToString(@"hh\:mm\:ss");
                CurrentValue = timeString.Replace(":", colonReplacementWithSpaces);
            }

        }

    }

}
