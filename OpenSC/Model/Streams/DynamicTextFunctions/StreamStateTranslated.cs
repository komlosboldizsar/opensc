using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Streams.DynamicTextFunctions
{

    class StreamStateTranslated : StreamState
    {
        public override string FunctionName => nameof(StreamStateTranslated);

        public override string Description => "The state of a stream with the given translations.";

        public override int ParameterCount => 6;

        public override DynamicTextFunctionArgumentType[] ArgumentTypes => new DynamicTextFunctionArgumentType[]
        {
            DynamicTextFunctionArgumentType.Integer,
            DynamicTextFunctionArgumentType.String,
            DynamicTextFunctionArgumentType.String,
            DynamicTextFunctionArgumentType.String,
            DynamicTextFunctionArgumentType.String,
            DynamicTextFunctionArgumentType.String
        };

        public override string[] ArgumentDescriptions => new string[]
        {
            "ID of the stream.",
            "Translation for state 'unknown'.",
            "Translation for state 'not running'.",
            "Translation for state 'not started'.",
            "Translation for state 'running'.",
            "Translation for state 'ended'."
        };

        public override IDynamicTextFunctionSubstitute GetSubstitute(object[] arguments)
        {
            Stream stream = StreamDatabase.Instance.GetTById((int)arguments[0]);
            Substitute substitute = new Substitute(
                stream,
                arguments[1].ToString(),
                arguments[2].ToString(),
                arguments[3].ToString(),
                arguments[1].ToString(),
                arguments[5].ToString()
            );
            substitute.UpdateAfterConstruct();
            return substitute;
        }

        protected new class Substitute : StreamState.Substitute
        {

            private readonly string translationForUnknown;
            private readonly string translationForNotRunning;
            private readonly string translationForNotStarted;
            private readonly string translationForRunning;
            private readonly string translationForEnded;

            public Substitute(Stream stream, string translationForUnknown, string translationForNotRunning, string translationForNotStarted, string translationForRunning, string translationForEnded) : base(stream)
            {
                this.translationForUnknown = translationForUnknown;
                this.translationForNotRunning = translationForNotRunning;
                this.translationForNotStarted = translationForNotStarted;
                this.translationForRunning = translationForRunning;
                this.translationForEnded = translationForEnded;
            }

            protected override string convertStateToString(Streams.StreamState state)
            {
                switch (state)
                {
                    case Streams.StreamState.Unknown:
                        return translationForUnknown;
                    case Streams.StreamState.NotRunning:
                        return translationForNotRunning;
                    case Streams.StreamState.NotStarted:
                        return translationForNotStarted;
                    case Streams.StreamState.Running:
                        return translationForRunning;
                    case Streams.StreamState.Ended:
                        return translationForEnded;
                }
                return base.convertStateToString(state);
            }

        }

    }

}
