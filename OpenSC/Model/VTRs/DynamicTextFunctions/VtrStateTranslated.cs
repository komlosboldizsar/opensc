using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.VTRs.DynamicTextFunctions
{

    class VtrStateTranslated : VtrState
    {
        public override string FunctionName => nameof(VtrStateTranslated);

        public override string Description => "The state of a VTR with the given translations.";

        public override int ParameterCount => 7;

        public override DynamicTextFunctionArgumentType[] ArgumentTypes => new DynamicTextFunctionArgumentType[]
        {
            DynamicTextFunctionArgumentType.Integer,
            DynamicTextFunctionArgumentType.String,
            DynamicTextFunctionArgumentType.String,
            DynamicTextFunctionArgumentType.String,
            DynamicTextFunctionArgumentType.String,
            DynamicTextFunctionArgumentType.String,
            DynamicTextFunctionArgumentType.String
        };

        public override string[] ArgumentDescriptions => new string[]
        {
            "ID of the VTR.",
            "Translation for state 'stopped'.",
            "Translation for state 'paused'.",
            "Translation for state 'playing'.",
            "Translation for state 'rewinding'.",
            "Translation for state 'fastforwarding'.",
            "Translation for state 'recording'."
        };

        public override IDynamicTextFunctionSubstitute GetSubstitute(object[] arguments)
        {
            Vtr vtr = VtrDatabase.Instance.GetTById((int)arguments[0]);
            Substitute substitute = new Substitute(
                vtr,
                arguments[1].ToString(),
                arguments[2].ToString(),
                arguments[3].ToString(),
                arguments[1].ToString(),
                arguments[5].ToString(),
                arguments[6].ToString()
            );
            substitute.UpdateAfterConstruct();
            return substitute;
        }

        protected new class Substitute : VtrState.Substitute
        {

            private readonly string translationForStopped;
            private readonly string translationForPaused;
            private readonly string translationForPlaying;
            private readonly string translationForRewinding;
            private readonly string translationForFastForwarding;
            private readonly string translationForRecording;

            public Substitute(Vtr vtr, string translationForStopped, string translationForPaused, string translationForPlaying, string translationForRewinding, string translationForFastForwarding, string translationForRecording) : base(vtr)
            {
                this.translationForStopped = translationForStopped;
                this.translationForPaused = translationForPaused;
                this.translationForPlaying = translationForPlaying;
                this.translationForRewinding = translationForRewinding;
                this.translationForFastForwarding = translationForFastForwarding;
                this.translationForRecording = translationForRecording;
            }

            protected override string convertStateToString(VTRs.VtrState state)
            {
                switch (state)
                {
                    case VTRs.VtrState.Stopped:
                        return translationForStopped;
                    case VTRs.VtrState.Paused:
                        return translationForPaused;
                    case VTRs.VtrState.Playing:
                        return translationForPlaying;
                    case VTRs.VtrState.Rewinding:
                        return translationForRewinding;
                    case VTRs.VtrState.FastForwarding:
                        return translationForFastForwarding;
                    case VTRs.VtrState.Recording:
                        return translationForRecording;
                }
                return base.convertStateToString(state);
            }

        }

    }

}
