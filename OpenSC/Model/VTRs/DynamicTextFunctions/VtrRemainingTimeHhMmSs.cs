using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.VTRs.DynamicTextFunctions
{
    class VtrRemainingTimeHhMmSs : IDynamicTextFunction
    {
        public string FunctionName => nameof(VtrRemainingTimeHhMmSs);

        public string Description => "HH:MM:SS format of remaining time of a playout on a VTR.";

        public int ParameterCount => 1;

        public DynamicTextFunctionArgumentType[] ArgumentTypes => new DynamicTextFunctionArgumentType[]
        {
            DynamicTextFunctionArgumentType.Integer
        };

        public string[] ArgumentDescriptions => new string[]
        {
            "ID of the VTR."
        };

        public IDynamicTextFunctionSubstitute GetSubstitute(object[] arguments)
        {
            Vtr vtr = VtrDatabase.Instance.GetTById((int)arguments[0]);
            return new Substitute(vtr);
        }

        public class Substitute : IDynamicTextFunctionSubstitute
        {

            private Vtr vtr;

            public Substitute(Vtr vtr)
            {
                this.vtr = vtr;
                vtr.SecondsRemainingChanged += vtrSecondsRemainingChangedHandler;
                CurrentValue = vtr.TimeRemaining.ToString(@"hh\:mm\:ss");
            }

            private void vtrSecondsRemainingChangedHandler(Vtr vtr, int oldValue, int newValue)
            {
                CurrentValue = vtr.TimeRemaining.ToString(@"hh\:mm\:ss");
            }

            private string currentValue;

            public string CurrentValue
            {
                get { return currentValue; }
                private set
                {
                    if (value == currentValue)
                        return;
                    currentValue = value;
                    ValueChanged?.Invoke(this);
                }
            }

            public event DynamicTextFunctionSubstituteValueChanged ValueChanged;

        }

    }

}
