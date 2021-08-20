using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Streams.DynamicTextFunctions
{

    [DynamicTextFunction(nameof(StreamState), "The state of a stream.")]
    public class StreamState : DynamicTextFunctionBase<StreamState.Substitute>
    {

        [DynamicTextFunctionArgument(0, "ID of the stream.")]
        public class Arg0 : DynamicTextFunctionArgumentDatabaseItem<Stream>
        {
            public Arg0() : base(StreamDatabase.Instance)
            { }
        }

        public class Substitute : DynamicTextFunctionSubstituteBase
        {

            public override void Init(object[] argumentObjects)
            {
                Stream stream = argumentObjects[0] as Stream;
                if (stream == null)
                {
                    CurrentValue = "?";
                    return;
                }
                stream.StateChanged += streamStateChangedHandler;
                CurrentValue = convertStateToString(stream.State);
            }

            private void streamStateChangedHandler(Stream stream, Streams.StreamState oldState, Streams.StreamState newState)
                => CurrentValue = convertStateToString(newState);

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
