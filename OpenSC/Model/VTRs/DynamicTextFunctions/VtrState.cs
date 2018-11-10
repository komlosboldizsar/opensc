using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.VTRs.DynamicTextFunctions
{

    class VtrState : IDynamicTextFunction
    {
        public virtual string FunctionName => nameof(VtrState);

        public virtual string Description => "The state of a VTR.";

        public virtual int ParameterCount => 1;

        public virtual DynamicTextFunctionArgumentType[] ArgumentTypes => new DynamicTextFunctionArgumentType[]
        {
            DynamicTextFunctionArgumentType.Integer
        };

        public virtual string[] ArgumentDescriptions => new string[]
        {
            "ID of the VTR."
        };

        public virtual IDynamicTextFunctionSubstitute GetSubstitute(object[] arguments)
        {
            Vtr vtr = VtrDatabase.Instance.GetTById((int)arguments[0]);
            Substitute substitute = new Substitute(vtr);
            substitute.UpdateAfterConstruct();
            return substitute;
        }

        protected class Substitute : DynamicTextFunctionSubstituteBase
        {

            private readonly Vtr vtr;

            public Substitute(Vtr vtr)
            {
                if (vtr == null) {
                    CurrentValue = "?";
                    return;
                }
                this.vtr = vtr;
                vtr.StateChanged += vtrStateChangedHandler;
            }

            public void UpdateAfterConstruct()
            {
                CurrentValue = convertStateToString(vtr.State);
            }

            private void vtrStateChangedHandler(Vtr vtr, VTRs.VtrState oldState, VTRs.VtrState newState)
            {
                CurrentValue = convertStateToString(newState);
            }

            protected virtual string convertStateToString(VTRs.VtrState state)
            {
                switch (state)
                {
                    case VTRs.VtrState.Stopped:
                        return "stopped";
                    case VTRs.VtrState.Paused:
                        return "paused";
                    case VTRs.VtrState.Playing:
                        return "playing";
                    case VTRs.VtrState.Rewinding:
                        return "rewinding";
                    case VTRs.VtrState.FastForwarding:
                        return "fastForwarding";
                    case VTRs.VtrState.Recording:
                        return "recording";
                }
                return "?";
            }

        }

    }

}
