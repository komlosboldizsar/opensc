using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.VTRs.DynamicTextFunctions
{

    [DynamicTextFunction(nameof(VtrState), "The state of a VTR.")]
    public class VtrState : DynamicTextFunctionBase<VtrState.Substitute>
    {

        [DynamicTextFunctionArgument(0, "ID of the VTR.")]
        public class Arg0 : DynamicTextFunctionArgumentDatabaseItem<Vtr>
        {
            public Arg0() : base(VtrDatabase.Instance)
            { }
        }

        public class Substitute : DynamicTextFunctionSubstituteBase
        {

            public override void Init(object[] argumentObjects)
            {
                Vtr vtr = argumentObjects[0] as Vtr;
                if (vtr == null)
                {
                    CurrentValue = "?";
                    return;
                }
                vtr.StateChanged += vtrStateChangedHandler;
                CurrentValue = convertStateToString(vtr.State);
            }

            private void vtrStateChangedHandler(Vtr vtr, VTRs.VtrState oldState, VTRs.VtrState newState)
                => CurrentValue = convertStateToString(newState);

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
