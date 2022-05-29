using System.Collections.Generic;
using System.Linq;

namespace OpenSC.Library.BmdVideohub
{
    internal class OutputLabelsRequest : LabelsRequest
    {
        protected override string Header { get; } = ProtocolStrings.BLOCK__OUTPUT_LABELS;
        public OutputLabelsRequest(Label label) : base(label) { }
        public OutputLabelsRequest(IEnumerable<Label> labels) : base(labels) { }
    }
}
