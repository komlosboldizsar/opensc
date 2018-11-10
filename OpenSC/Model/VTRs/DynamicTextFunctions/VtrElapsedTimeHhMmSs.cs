using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.VTRs.DynamicTextFunctions
{
    class VtrElapsedTimeHhMmSs : IDynamicTextFunction
    {
        public string FunctionName => nameof(VtrElapsedTimeHhMmSs);

        public string Description => "HH:MM:SS format of elapsed (played) time of a playout on a VTR.";

        public int ParameterCount => 2;

        public DynamicTextFunctionArgumentType[] ArgumentTypes => new DynamicTextFunctionArgumentType[]
        {
            DynamicTextFunctionArgumentType.Integer,
            DynamicTextFunctionArgumentType.Integer
        };

        public string[] ArgumentDescriptions => new string[]
        {
            "ID of the VTR.",
            "Spaces around colons in formatted time string."
        };

        public IDynamicTextFunctionSubstitute GetSubstitute(object[] arguments)
        {
            Vtr vtr = VtrDatabase.Instance.GetTById((int)arguments[0]);
            return new Substitute(vtr, (int)arguments[1]);
        }

        public class Substitute : DynamicTextFunctionSubstituteBase
        {

            private readonly Vtr vtr;
            private readonly string colonReplacementWithSpaces;

            public Substitute(Vtr vtr, int spacesAround)
            {

                spacesAround = (spacesAround >= 0) ? spacesAround : 0;
                colonReplacementWithSpaces = string.Format("{0}:{0}", new string(' ', spacesAround));

                if (vtr == null)
                {
                    CurrentValue = "??:??:??".Replace(":", colonReplacementWithSpaces);
                    return;
                }
                this.vtr = vtr;

                vtr.SecondsElapsedChanged += vtrSecondsElapsedChangedHandler;
                updateValue();

            }

            private void vtrSecondsElapsedChangedHandler(Vtr vtr, int oldValue, int newValue)
            {
                updateValue();
            }

            private void updateValue()
            {
                string timeString = vtr.TimeElapsed.ToString(@"hh\:mm\:ss");
                CurrentValue = timeString.Replace(":", colonReplacementWithSpaces);
            }

        }

    }

}
