using System.Collections.Generic;
using System.Linq;

namespace OpenSC.Library.BmdVideohub
{
    internal class InputLabelsRequest : LabelsRequest
    {
        protected override string Header { get; } = ProtocolStrings.BLOCK__INPUT_LABELS;
        public InputLabelsRequest(Label label) : base(label) { }
        public InputLabelsRequest(IEnumerable<Label> labels) : base(labels) { }
    }
}
