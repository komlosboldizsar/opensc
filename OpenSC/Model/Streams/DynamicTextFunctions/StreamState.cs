using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Streams.DynamicTextFunctions
{

    class StreamState : IDynamicTextFunction
    {
        public virtual string FunctionName => nameof(StreamState);

        public virtual string Description => "The state of a stream.";

        public virtual int ParameterCount => 1;

        public virtual DynamicTextFunctionArgumentType[] ArgumentTypes => new DynamicTextFunctionArgumentType[]
        {
            DynamicTextFunctionArgumentType.Integer
        };

        public virtual string[] ArgumentDescriptions => new string[]
        {
            "ID of the stream."
        };

        public virtual IDynamicTextFunctionSubstitute GetSubstitute(object[] arguments)
        {
            Stream stream = StreamDatabase.Instance.GetTById((int)arguments[0]);
            Substitute substitute = new Substitute(stream);
            substitute.UpdateAfterConstruct();
            return substitute;
        }

        protected class Substitute : DynamicTextFunctionSubstituteBase
        {

            private Stream stream;

            public Substitute(Stream stream)
            {
                if (stream == null) {
                    CurrentValue = "?";
                    return;
                }
                this.stream = stream;
                stream.StateChanged += streamStateChangedHandler;
            }

            public void UpdateAfterConstruct()
            {
                CurrentValue = convertStateToString(stream.State);
            }

            private void streamStateChangedHandler(Stream stream, Streams.StreamState oldState, Streams.StreamState newState)
            {
                CurrentValue = convertStateToString(newState);
            }

            protected virtual string convertStateToString(Streams.StreamState state)
            {
                switch (state)
                {
                    case Streams.StreamState.Unknown:
                        return "unknown";
                    case Streams.StreamState.NotRunning:
                        return "not running";
                    case Streams.StreamState.NotStarted:
                        return "not started";
                    case Streams.StreamState.Running:
                        return "running";
                    case Streams.StreamState.Ended:
                        return "ended";
                }
                return "?";
            }

        }

    }

}
