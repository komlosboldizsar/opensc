using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Streams.DynamicTextFunctions
{

    [DynamicTextFunction(nameof(StreamViewerCount), "Total count of viewers of a stream.")]
    public class StreamViewerCount : DynamicTextFunctionBase<StreamViewerCount.Substitute>
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
                stream.ViewerCountChanged += streamViewerCountChangedHandler;
                CurrentValue = stream.ViewerCount?.ToString() ?? "?";
            }

            private void streamViewerCountChangedHandler(Stream stream, int? oldCount, int? newCount) => CurrentValue = newCount?.ToString() ?? "?";

        }

    }

}
