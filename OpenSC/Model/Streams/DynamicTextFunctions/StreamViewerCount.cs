using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Streams.DynamicTextFunctions
{
    class StreamViewerCount : IDynamicTextFunction
    {
        public string FunctionName => nameof(StreamViewerCount);

        public string Description => "Total count of viewers of a stream.";

        public int ParameterCount => 1;

        public DynamicTextFunctionArgumentType[] ArgumentTypes => new DynamicTextFunctionArgumentType[]
        {
            DynamicTextFunctionArgumentType.Integer
        };

        public string[] ArgumentDescriptions => new string[]
        {
            "ID of the stream."
        };

        public IDynamicTextFunctionSubstitute GetSubstitute(object[] arguments)
        {
            Stream stream = StreamDatabase.Instance.GetTById((int)arguments[0]);
            return new Substitute(stream);
        }

        public class Substitute : DynamicTextFunctionSubstituteBase
        {

            private Stream stream;

            public Substitute(Stream stream)
            {
                if (stream == null) {
                    CurrentValue = "?";
                    return;
                }
                this.stream = stream;
                stream.ViewerCountChanged += streamViewerCountChangedHandler;
                CurrentValue = stream.ViewerCount.ToString();
            }

            private void streamViewerCountChangedHandler(Stream stream, int? oldCount, int? newCount)
            {
                CurrentValue = newCount.ToString();
            }

        }

    }

}
