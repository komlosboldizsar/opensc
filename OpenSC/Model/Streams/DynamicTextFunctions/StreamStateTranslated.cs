using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Streams.DynamicTextFunctions
{

    [DynamicTextFunction(nameof(StreamStateTranslated), "The state of a stream with the given translations.")]
    class StreamStateTranslated : DynamicTextFunctionBase<StreamStateTranslated.Substitute>
    {

        [DynamicTextFunctionArgument(0, "ID of the stream.")]
        public class Arg0 : DynamicTextFunctionArgumentDatabaseItem<Stream>
        {
            public Arg0() : base(StreamDatabase.Instance)
            { }
        }

        [DynamicTextFunctionArgument(1, "Translation for state 'unknown'.")]
        public class Arg1 : DynamicTextFunctionArgumentString
        { }

        [DynamicTextFunctionArgument(2, "Translation for state 'not running'.")]
        public class Arg2 : DynamicTextFunctionArgumentString
        { }

        [DynamicTextFunctionArgument(3, "Translation for state 'not started'.")]
        public class Arg3 : DynamicTextFunctionArgumentString
        { }

        [DynamicTextFunctionArgument(4, "Translation for state 'running'.")]
        public class Arg4 : DynamicTextFunctionArgumentString
        { }

        [DynamicTextFunctionArgument(5, "Translation for state 'ended'.")]
        public class Arg5 : DynamicTextFunctionArgumentString
        { }

        public class Substitute : StreamState.Substitute
        {

            private string translationForUnknown;
            private string translationForNotRunning;
            private string translationForNotStarted;
            private string translationForRunning;
            private string translationForEnded;

            public override void Init(object[] argumentObjects)
            {
                translationForUnknown = (string)argumentObjects[1];
                translationForNotRunning = (string)argumentObjects[2];
                translationForNotStarted = (string)argumentObjects[3];
                translationForRunning = (string)argumentObjects[4];
                translationForEnded = (string)argumentObjects[5];
                base.Init(argumentObjects);
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
